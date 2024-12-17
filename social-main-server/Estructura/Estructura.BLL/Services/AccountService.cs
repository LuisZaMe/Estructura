using Estructura.Common.Models;
using Estructura.Common.Request;
using Estructura.Common.Response;
using Estructura.Core.Models;
using Estructura.Core.Repositories;
using Estructura.Core.Services;
using Estructura.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenUtil _token;
        private readonly ICompanyRepository _companies;
        private readonly ISendMail _mailUtil;
        private readonly Core.ConfigurationReflection.APIConfig Config;
        IUtilitiesRepository _utilities;

        public AccountService(IUtilitiesRepository _utilities, ICompanyRepository _companies, ISendMail _mailUtil, ITokenUtil _token, IAccountRepository accountRepository, Microsoft.Extensions.Options.IOptions<Core.ConfigurationReflection.APIConfig> _options)
        {
            this._token = _token;
            this._accountRepository = accountRepository;
            this._utilities = _utilities;
            this._companies=_companies;
            this._utilities = _utilities;
            this.Config = _options.Value;
            this._mailUtil=_mailUtil;
        }

        public async Task<GenericResponse<Identity>> CreateSuperAdmin(Identity request)
        {
            var unhandledError = new GenericResponse<Identity>()
            {
                ErrorMessage = "Unable to create user, Verify the data and retry",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            if (request == null || string.IsNullOrWhiteSpace(request.Email) ||
               string.IsNullOrWhiteSpace(request.Phone) || request.Role == Common.Enums.Role.NONE)
            {
                return new GenericResponse<Identity>()
                {
                    ErrorMessage = "All fields required",
                    StatusCode = System.Net.HttpStatusCode.Unauthorized,
                    Sucess = false
                };
            }

            try
            {
                // 26-nov
                request.Email = request.Email;//Common.Utilities.EncodingHelper.DecodeFrom64(request.Email);
                request.Password = request.Password;//Common.Utilities.EncodingHelper.DecodeFrom64(request.Password);
                //Encrypt password before save on the DB
            }
            catch
            {
                return new GenericResponse<Identity>()
                {
                    ErrorMessage = "Not correct data encoded",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucess = false
                };
            }

            request.IsActive = true;

            try
            {
                if (request.Role != Common.Enums.Role.SUPER_ADMINISTRADOR)
                {
                    unhandledError.ErrorMessage = "Exclusive to create Superadministrador";
                    unhandledError.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return unhandledError;
                }

                var user = await _accountRepository.CreateAccount(request);
                if (!user.Sucess || user.Response.Id == 0)
                {
                    unhandledError.ErrorMessage = user?.ErrorMessage;
                    return unhandledError;
                }
                var emailRegistration = await SendCompleteRegistrationMail(user.Response);
                if (emailRegistration==null||!emailRegistration.Response)
                {
                    var accountDeleted = await _accountRepository.DeleteUserAccount(user.Response.Id, true);
                    unhandledError.ErrorMessage = "Error sending Registration Email";
                    return unhandledError;
                }

                return user;
            }
            catch (Exception exc)
            {
                unhandledError.ErrorMessage = exc.Message;
                return unhandledError;
            }
        }

        public async Task<GenericResponse<Identity>> CreateAccount(Identity request)
        {
            var unhandledError = new GenericResponse<Identity>()
            {
                ErrorMessage = "Unable to create user, Verify the data and retry",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            if (request == null ||
               string.IsNullOrWhiteSpace(request.Email) ||
               string.IsNullOrWhiteSpace(request.Phone) ||
               request.Role == Common.Enums.Role.NONE)
            {
                return new GenericResponse<Identity>()
                {
                    ErrorMessage = "All fields required",
                    StatusCode = System.Net.HttpStatusCode.Unauthorized,
                    Sucess = false
                };
            }

            try
            {
                request.Email = Common.Utilities.EncodingHelper.DecodeFrom64(request.Email);
                request.Password = Common.Utilities.EncodingHelper.DecodeFrom64(request.Password);
                //Encrypt password before save on the DB
            }
            catch
            {
                return new GenericResponse<Identity>()
                {
                    ErrorMessage = "Not correct data encoded",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucess = false
                };
            }

            switch (request.Role)
            {
                case Common.Enums.Role.ADMINISTRADOR:
                case Common.Enums.Role.INTERNO_ANALISTA:
                case Common.Enums.Role.INTERNO_ENTREVISTADOR:
                case Common.Enums.Role.SUPER_ADMINISTRADOR:
                    request.IsActive = true;
                    break;
                case Common.Enums.Role.CLIENTES:
                    request.IsActive = true;
                    break;
                default:
                    request.IsActive = true;
                    break;
            }



            try
            {
                //Not allowed to create superAdmin
                //if(request.Role == Common.Enums.Role.SUPER_ADMINISTRADOR)
                //{
                //    unhandledError.ErrorMessage = "Cannot create a Super Administrador, Request an update to TI";
                //    unhandledError.StatusCode = System.Net.HttpStatusCode.BadRequest;
                //    return unhandledError;
                //}

                if (request.Role == Common.Enums.Role.ADMINISTRADOR || request.Role == Common.Enums.Role.INTERNO_ANALISTA || request.Role == Common.Enums.Role.INTERNO_ENTREVISTADOR)
                {
                    var currentUser = _accountRepository.GetCurrentUser();
                    if (!currentUser.Sucess)
                    {
                        unhandledError.ErrorMessage = "User must be created by a platform user";
                        unhandledError.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                        return unhandledError;
                    }
                }

                if (request.Role == Common.Enums.Role.CLIENTES && 
                   (request.CompanyInformation == null ||
                   string.IsNullOrWhiteSpace(request.CompanyInformation.RFC)||
                   string.IsNullOrWhiteSpace(request.CompanyInformation.CompanyName)||
                   string.IsNullOrWhiteSpace(request.CompanyInformation.CompanyPhone)||
                   string.IsNullOrWhiteSpace(request.CompanyInformation.RazonSocial)||
                   request.CompanyInformation.Payment == null ||
                   request.CompanyInformation.Payment.Id == 0
                   ))
                {
                    unhandledError.ErrorMessage = "Incomplete company data";
                    unhandledError.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return unhandledError;
                }



                var user = await _accountRepository.CreateAccount(request);
                if (!user.Sucess || user.Response.Id == 0)
                {
                    unhandledError.ErrorMessage = user?.ErrorMessage;
                    return unhandledError;
                }
                if(request.Role == Common.Enums.Role.CLIENTES)
                {
                    request.CompanyInformation.UserId = user.Response.Id;
                    var companyResponse = await _companies.AddCompanyInformation(request.CompanyInformation);
                    user.Response.CompanyInformation = companyResponse.Response;
                }
                else
                {
                    var emailRegistration = await SendCompleteRegistrationMail(user.Response);
                    if (emailRegistration==null||!emailRegistration.Response)
                    {
                        var accountDeleted = await _accountRepository.DeleteUserAccount(user.Response.Id, true);
                        unhandledError.ErrorMessage = "Error sending Registration Email";
                        return unhandledError;
                    }
                }

                return user;
            }
            catch (Exception exc)
            {
                unhandledError.ErrorMessage = exc.Message;
                return unhandledError;
            }
        }

        public async Task<GenericResponse<Identity>> DeleteUserAccount(long UserID)
        {
            return await _accountRepository.DeleteUserAccount(UserID);
        }

        public async Task<GenericResponse<Identity>> UnapproveAccount(Identity user)
        {
            return await _accountRepository.UnapproveAccount(user);
        }

        public async Task<GenericResponse<Identity>> ApproveAccount(Identity user)
        {
            var unhandledError = new GenericResponse<Identity>()
            {
                ErrorMessage = "Missing data",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            if (user.Id == 0)
            {
                unhandledError.ErrorMessage = "User id or User role not found";
                return unhandledError;
            }

            var response = await _accountRepository.ApproveAccount(user);
            return response;
        }

        public Task<GenericResponse<List<Identity>>> GetPendingApprovalUsers(int currentPage, int offset)
        {
            return _accountRepository.GetPendingApprovalUsers(currentPage, offset);
        }

        public async Task<GenericResponse<bool>> SendRecoverPasswordMail(RecoverPasswordRequest request)
        {
            var emailExists = await _accountRepository.VerifyEmailExists(request.Email);
            if (emailExists == null || !emailExists.Sucess || !emailExists.Response)
                return new GenericResponse<bool>()
                {
                    ErrorMessage = "User not found",
                    Response = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Sucess = false
                };
            string message = "Ingresa aqui para recuperar tu contraseña:";
            var user = await _accountRepository.GetUsersByEmail(request.Email, 0, 10);
            var tokenBody = new SilentData()
            {
                Id = user.Response[0].Id,
                Email = request.Email,
                date = DateTime.UtcNow
            };
            var environment = _accountRepository.GetEnvironment();
            var token = _token.GenericEncode(tokenBody);
            string url = environment.Response.APIUrl;
            string body = string.Format("{0} {1}/password/reset?token={2}", message, url, token);

            return await _utilities.SendMail(request.Email, body, "Recuperar contrasena");
        }

        public async Task<GenericResponse<Identity>> UpdateAccountPassword(CompleteRegistration request)
        {
            var error = new GenericResponse<Identity>()
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = "Unhandled error"
            };

            try
            {
                request.Password = Common.Utilities.EncodingHelper.DecodeFrom64(request.Password);
            }
            catch
            {
                return new GenericResponse<Identity>()
                {
                    ErrorMessage = "Not correct data encoded",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucess = false
                };
            }
            try
            {
                var data = _token.GenericDecode<SilentData>(request.Token);
                if (data == null || data.date > DateTime.UtcNow.AddMinutes(10))
                {
                    error.ErrorMessage = "Invalid or expired token, contact support";
                    error.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return error;
                }

                var passwordUpdated = _accountRepository.UpdatePassword(data, request.Password);
                await Task.WhenAll(passwordUpdated);

                var user = await _accountRepository.GetUsersByEmail(data.Email, 0, 10);
                return new GenericResponse<Identity>()
                {
                    Response = user.Response.FirstOrDefault(),
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Sucess = true
                };
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                error.ErrorMessage = exc.Message;
                return error;
            }
        }

        public async Task<GenericResponse<Identity>> RecoverPasswordByMail(CompleteRecoverPasswordRequest request)
        {
            var unhandledError = new GenericResponse<Identity>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            try
            {
                var data = _token.GenericDecode<SilentData>(request.Token);
                if (data.date> DateTime.UtcNow.AddMinutes(10))
                {
                    unhandledError.ErrorMessage = "Expired token!";
                    return unhandledError;
                }

                string password = "";
                try
                {
                    password = Common.Utilities.EncodingHelper.DecodeFrom64(request.Password);
                }
                catch
                {
                    return new GenericResponse<Identity>()
                    {
                        ErrorMessage = "Not correct data encoded",
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Sucess = false
                    };
                }

                return await _accountRepository.UpdatePassword(data, password);
            }
            catch (Exception exc)
            {
                unhandledError.ErrorMessage = exc.Message;
                return unhandledError;
            }
        }

        public async Task<GenericResponse<List<Identity>>> GetActiveUsers(List<long> Id, int currentPage, int offset)
        {
            GenericResponse<List<Identity>> users = new GenericResponse<List<Identity>>();
            if (Id!=null&&Id.Count>0)
                users = await _accountRepository.GetUsersById(Id);
            else
                users = await _accountRepository.GetActiveUsers(currentPage, offset);

            if (users.Sucess&&users.Response!=null&&users.Response.Count>0)
            {
                List<long> userIds = new List<long>();
                users.Response.ForEach(e => userIds.Add(e.Id));
                var companyInformationList = await _companies.GetCompanyInformation(userIds);
                if (companyInformationList!=null&&companyInformationList.Sucess&&companyInformationList.Response!=null)
                {
                    users.Response.ForEach(e =>
                    {
                        e.CompanyInformation = companyInformationList?.Response?.FirstOrDefault(f => f.UserId == e.Id);
                    });
                }
            }
            return users;
        }

        public async Task<GenericResponse<List<Identity>>> SearchUsers(string key, bool showSuperAdmin = false, Common.Enums.Role role = Common.Enums.Role.NONE, int currentPage = 0, int offset = 10)
        {
            GenericResponse<List<Identity>> users = new GenericResponse<List<Identity>>();
            users= await _accountRepository.SearchUsers(key, showSuperAdmin, role, currentPage, offset);
            if (users.Sucess&&users.Response!=null&&users.Response.Count>0)
            {
                List<long> userIds = new List<long>();
                users.Response.ForEach(e => userIds.Add(e.Id));
                var companyInformationList = await _companies.GetCompanyInformation(userIds);
                if (companyInformationList!=null&&companyInformationList.Sucess&&companyInformationList.Response!=null)
                {
                    users.Response.ForEach(e =>
                    {
                        e.CompanyInformation = companyInformationList?.Response?.FirstOrDefault(f => f.UserId == e.Id);
                    });
                }
            }
            return users;
        }

        public async Task<GenericResponse<List<Identity>>> GetUsersByRole(Common.Enums.Role request, int currentPage, int offset)
        {
            var users = await _accountRepository.GetUsersByRole(request,currentPage, offset);
            if (users.Sucess&&users.Response!=null&&users.Response.Count>0)
            {
                List<long> userIds = new List<long>();
                users.Response.ForEach(e => userIds.Add(e.Id));
                var companyInformationList = await _companies.GetCompanyInformation(userIds);
                if (companyInformationList!=null&&companyInformationList.Sucess&&companyInformationList.Response!=null)
                {
                    users.Response.ForEach(e =>
                    {
                        e.CompanyInformation = companyInformationList?.Response?.FirstOrDefault(f => f.UserId == e.Id);
                    });
                }
            }
            return users;
        }

        public async Task<GenericResponse<Identity>> UpdateUserInformation(Identity request)
        {
            var requestedUsers = await GetActiveUsers(new List<long>() { request.Id }, 0, 10);
            if (requestedUsers==null||!requestedUsers.Sucess || requestedUsers.Response.Count==0)
            {
                return new GenericResponse<Identity>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ErrorMessage = "User not found" };
            }

            var requestedUser = requestedUsers.Response.First();
            request.Name = string.IsNullOrWhiteSpace(request.Name) ? requestedUser.Name : request.Name;
            request.Lastname = string.IsNullOrWhiteSpace(request.Lastname) ? requestedUser.Lastname : request.Lastname;
            request.Phone = string.IsNullOrWhiteSpace(request.Phone) ? requestedUser.Phone : request.Phone;
            request.Role = request.Role == Common.Enums.Role.NONE? requestedUser.Role : request.Role;
            return await _accountRepository.UpdateUserInformation(request);
        }
    
        private async Task<GenericResponse<bool>> SendCompleteRegistrationMail(Identity target)
        {
            var token = _token.GenericEncode(new SilentData()
            {
                Email = target.Email,
                date = DateTime.UtcNow.AddDays(1),
                Id = target.Id
            });
            string emailInvitation = "Para completar tu registro accede a";
            string emailSubject = "Completar registro de cuenta";
            string emailBody = string.Format("{0}: {1}/validar-cuenta?token={2}", emailInvitation, Config.Environment.WebUrl, token);
            return await _mailUtil.SendSingle(target.Email, emailBody, emailSubject);
        }

        public async Task<GenericResponse<Identity>> CompleteAccountRegistration(CompleteRegistration request)
        {
            var error = new GenericResponse<Identity>()
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = "Unhandled error"
            };

            try
            {
                request.Password = Common.Utilities.EncodingHelper.DecodeFrom64(request.Password);
            }
            catch
            {
                return new GenericResponse<Identity>()
                {
                    ErrorMessage = "Not correct data encoded",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucess = false
                };
            }

            try
            {
                var data = _token.GenericDecode<SilentData>(request.Token);
                if (data == null || data.date< DateTime.UtcNow)
                {
                    error.ErrorMessage = "Invalid or expired token, contact support";
                    error.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return error;
                }

                var accountApproved = _accountRepository.ApproveAccount(new Identity()
                {
                    Id = data.Id,
                    Email = data.Email
                });
                var passwordUpdated = _accountRepository.UpdatePassword(data, request.Password);
                await Task.WhenAll(accountApproved, passwordUpdated);

                if (accountApproved.Result==null||accountApproved.Result.Response==null||!accountApproved.Result.Sucess ||
                    passwordUpdated.Result==null||passwordUpdated.Result.Response==null||!passwordUpdated.Result.Sucess)
                {
                    error.ErrorMessage = "Account cannot be approved, verify password";
                    error.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return error;
                }

                var user =  await GetActiveUsers(new List<long>() { data.Id }, 0, 10);
                return new GenericResponse<Identity>()
                {
                    Response = user.Response.FirstOrDefault(),
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Sucess = true
                };
            }
            catch(Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                error.ErrorMessage = exc.Message;
                return error;
            }
        }

        public async Task<GenericResponse<int>> Pagination(int splitBy, string key, bool showSuperAdmin = false, Common.Enums.Role role = Common.Enums.Role.NONE)
        {
            return await _accountRepository.Pagination(splitBy, key, showSuperAdmin, role);
        }
    }
}
