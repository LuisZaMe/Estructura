using Dapper;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Models;
using Estructura.Core.Repositories;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.DAL.Repositories
{

    public class UtilitiesRepository : BaseRepository, Core.Repositories.IUtilitiesRepository
    {
        public string baseFilePath => "C://Files";
        public string basePath => "C://Images";
        public string GENERAL => "General";
        public string EVIDENCE => "Evidence";
        public string PROFILE => "Profile";

        protected readonly Core.ConfigurationReflection.APIConfig Config;
        public readonly Core.Models.AppUser AppUser;
        public UtilitiesRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) {
            Config = options.Value;
            AppUser = new Core.Models.AppUser(httpAccesor.HttpContext.User);
        }

        public Core.ConfigurationReflection.APIConfig GetConfig()
        {
            return Config;
        }

        public async Task<GenericResponse<Compatibility>> IsAppCompatible(int appBuildVersion)
        {
            string query = $@"SELECT TOP 1 * FROM CompatibleVersions 
                    ORDER BY CompatibleVersions.AppVersion DESC;";

            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var result = await conn.QuerySingleAsync<Compatibility>(query);
                    return new GenericResponse<Compatibility>()
                    {
                        Response = result,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Sucess  = true
                    };
                }
            }
            catch (System.Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message, nameof(UtilitiesRepository));
                return new GenericResponse<Compatibility>()
                {
                    Sucess = false,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<bool>> VerifyEmailExist(string email)
        {
            string query = $"SELECT TOP 1 Email FROM User u WHERE u.Email = '{email}' AND u.Id != {AppUser.UserID}";
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var result = await conn.ExecuteReaderAsync(query);
                    return new GenericResponse<bool>()
                    {
                        Response = result.HasRows,
                        Sucess = true,
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (System.Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message, nameof(UtilitiesRepository));
                return new GenericResponse<bool>()
                {
                    Response = false,
                    Sucess = false,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<Role>>> GetRoles()
        {
            string query = $"SELECT* FROM [dbo].[Role]";
            List<Common.Models.Role> roles = new List<Common.Models.Role>();
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var result = await conn.ExecuteReaderAsync(query);
                    while (result.Read())
                    {
                        roles.Add(new Common.Models.Role()
                        {
                            RoleDescription = result.GetString("RoleDescription"),
                            Id = result.GetInt32("Id"),
                            ParentRole = (Common.Enums.Role)result.GetInt32("Id")
                        });
                    }
                    return new GenericResponse<List<Role>>()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Sucess = true,
                        Response =  roles
                    };
                }
            }
            catch (System.Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message, nameof(UtilitiesRepository));
                return new GenericResponse<List<Role>>()
                {
                    ErrorMessage = exc.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Sucess = false,
                };
            }
        }

        public async Task<GenericResponse<List<City>>> GetCities(int stateId)
        {
            var genericError = new GenericResponse<List<City>>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            string query = $@"SELECT* FROM [dbo].[Cities] WHERE [dbo].[Cities].StateId = {stateId}";

            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    List<City> cities = new List<City>();
                    var apiResponse = await conn.ExecuteReaderAsync(query);
                    while (apiResponse.Read())
                    {
                        cities.Add(new City()
                        {
                            Id = apiResponse.GetInt32("Id"),
                            CityNumber = apiResponse.GetInt32("Id"),
                            StateId = apiResponse.GetInt32("StateId"),
                            Name = apiResponse.IsDBNull("Name") ? "" : apiResponse.GetString("Name"),
                        });
                    }
                    return new GenericResponse<List<City>>()
                    {
                        Response = cities,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Sucess = true
                    };
                }
            }
            catch (Exception exc)
            {
                genericError.ErrorMessage = exc.Message;
                return genericError;
            }
        }

        public async Task<GenericResponse<List<State>>> GetStates()
        {

            var genericError = new GenericResponse<List<State>>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            string query = $@"SELECT* FROM [dbo].[States]";

            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    List<State> states = new List<State>();
                    var apiResponse = await conn.ExecuteReaderAsync(query);
                    while (apiResponse.Read())
                    {
                        states.Add(new State()
                        {
                            Id = apiResponse.GetInt32("Id"),
                            Name = apiResponse.IsDBNull("Name") ? "" : apiResponse.GetString("Name"),
                        });
                    }
                    return new GenericResponse<List<State>>()
                    {
                        Response = states,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Sucess = true
                    };
                }
            }
            catch (Exception exc)
            {
                genericError.ErrorMessage = exc.Message;
                return genericError;
            }
        }

        public async Task<GenericResponse<bool>> SendMail(string to, string body, string subject)
        {
            var unhandledError = new GenericResponse<bool>()
            {
                ErrorMessage = "Unhandled error",
                Response = false,
                StatusCode = System.Net.HttpStatusCode.OK,
                Sucess = false
            };

            try
            {
                var env = GetCurrentEnvironment();
                string from = env.EmailFrom;
                string host = env.EmailHost;
                string pass = env.EmailPass;
                int port = env.EmailPort;
                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse(from));
                mail.To.Add(MailboxAddress.Parse(to));
                mail.Subject = subject;
                mail.Body = new TextPart(TextFormat.Plain) { Text = body };

                using (var smtp = new SmtpClient())
                {
                    smtp.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                    smtp.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                    smtp.Authenticate(from, pass);
                    smtp.Send(mail);
                    smtp.Disconnect(true);
                }

                return new GenericResponse<bool>()
                {
                    Response = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Sucess = true
                };
            }
            catch (Exception exc)
            {
                unhandledError.ErrorMessage = exc.Message;
            }
            return unhandledError;
        }
        
        public string ImageURLFormatter(Media media)
        {
            string baseURL = Config.Environment.APIUrl;
            string path = string.Empty;
            switch (media.StoreMediaType)
            {
                case Common.Enums.StoreMediaType.EVIDENCE:
                    path = $@"{basePath}/{EVIDENCE}";
                    break;
                case Common.Enums.StoreMediaType.PROFILE:
                    path = $@"{basePath}/{PROFILE}";
                    break;
                case Common.Enums.StoreMediaType.GENERAL:
                default:
                    path = $@"{basePath}/{GENERAL}";
                    break;
            }

            return string.Format("{0}/{1}/{2}", baseURL, path.Replace("C://", ""), media.ImageName);
        }

        public string FileURLFormatter(Doccument file)
        {
            string baseURL = Config.Environment.APIUrl;
            string path = string.Empty;
            switch (file.StoreMediaType)
            {
                case Common.Enums.StoreMediaType.EVIDENCE:
                    path = $@"{baseFilePath}/{EVIDENCE}";
                    break;
                case Common.Enums.StoreMediaType.PROFILE:
                    path = $@"{baseFilePath}/{PROFILE}";
                    break;
                case Common.Enums.StoreMediaType.GENERAL:
                default:
                    path = $@"{baseFilePath}/{GENERAL}";
                    break;
            }

            return string.Format("{0}/{1}/{2}", baseURL, path.Replace("C://", ""), file.DoccumentName);
        }
    }
}
