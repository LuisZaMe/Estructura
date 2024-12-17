using Dapper;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Models;
using Estructura.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.DAL.Repositories
{

    public class AccountRepository : BaseRepository, IAccountRepository
    {
        private readonly IUtilitiesRepository _utilitiesRepository;
        public AccountRepository(IUtilitiesRepository _utilitiesRepository, IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor)
        {
            this._utilitiesRepository=_utilitiesRepository;
        }

        public GenericResponse<AppUser> GetCurrentUser()
        {
            return new GenericResponse<AppUser>()
            {
                Response= AppUser,
                StatusCode = System.Net.HttpStatusCode.OK,
                Sucess = AppUser!=null&& AppUser.IsAuthenticated
            };
        }

        public GenericResponse<Core.ConfigurationReflection.Environment> GetEnvironment()
        {
            return new GenericResponse<Core.ConfigurationReflection.Environment>()
            {
                Response = base.GetCurrentEnvironment(),
                StatusCode = System.Net.HttpStatusCode.OK,
                Sucess = true
            };
        }

        public async Task<GenericResponse<Identity>> CreateAccount(Identity request)
        {
            var commonError = new GenericResponse<Identity>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            int userID = 0;
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<int>($@"
	                    {(request.Role== Common.Enums.Role.CLIENTES ? $@"declare @passwordHash varbinary(max) = hashbytes('SHA1', '{Config.TokenizationCredentials.ApiHashKey}'+'{request.Password}')" : "")}				        

                        INSERT INTO [dbo].[user] (
	                        [Name], 
	                        [Lastname], 
	                        [Phone], 
	                        [Email],
	                        [RoleID],
                            [CreatedAt],
                            [UpdatedAt],
                            [IsActive],
                            [StateId],
                            [CityId]
	                        {(GetCurrentUser().Sucess && request.Role!= Common.Enums.Role.CLIENTES ? ",[UnderAdminUserId]" : "")}
	                        {(request.Role== Common.Enums.Role.CLIENTES? ",[PasswordHash]":"")}
                        )                
                        OUTPUT INSERTED.*
                        VALUES (
	                        @Name, 
	                        @Lastname, 
	                        @Phone, 
	                        @Email, 
	                        @RoleID,
                            @CreatedAt,
                            @UpdatedAt,
                            @IsActive,
                            @StateId,
                            @CityId
	                        {(GetCurrentUser().Sucess && request.Role!= Common.Enums.Role.CLIENTES ? $@", {AppUser.UserID}" : "")}
	                        {(request.Role== Common.Enums.Role.CLIENTES ? ",@passwordHash" : "")}
	                        )
                    ", new
                    {
                        Name = request.Name,
                        Lastname = request.Lastname,
                        Phone = request.Phone,
                        Email = request.Email,
                        RoleID = (int)request.Role,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = request.IsActive,
                        StateId = request.StateId, 
                        CityId = request.CityId
                    });
                    userID = apiResponse;
                }
            }
            catch (Exception exc)
            {
                commonError.ErrorMessage = exc.Message;
                return commonError;
            }

            if (userID == 0)
                return commonError;
            request.Id = userID;
            request.Password = "";
            return new GenericResponse<Identity>()
            {
                Response = request,
                StatusCode = System.Net.HttpStatusCode.Created,
                Sucess = true
            };
        }

        public async Task<GenericResponse<Identity>> DeleteUserAccount(long UserID, bool hardDelete = false)
        {
            string query = string.Empty;
            if (hardDelete)
                query = $@"                        
                        DELETE FROM CompanyInformation WHERE UserId = {UserID}
                        DELETE FROM [dbo].[User] WHERE [dbo].[User].Id = {UserID};";
            else
                query = $@"UPDATE [dbo].[User] SET IsActive = 0 output inserted.*, inserted.RoleId as Role WHERE [dbo].[User].Id = {UserID};";

            try
            {
                using (var conn = Connection)
                {
                    var apiResponse = await conn.QueryAsync<Identity>(query);
                    return new GenericResponse<Identity>()
                    {
                        Response = apiResponse.FirstOrDefault(),
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Sucess = true
                    };
                }

            }
            catch (Exception exc)
            {
                return new GenericResponse<Identity>()
                {
                    ErrorMessage = exc.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucess = false
                };
            }
        }

        public async Task<GenericResponse<List<Identity>>> GetPendingApprovalUsers(int currentPage, int offset)
        {
            var genericError = new GenericResponse<List<Identity>>()
            {
                Response = new List<Identity>(),
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Sucess = false
            };


            var filter = await GetAdminForCurrentUser();
            if (!filter.Sucess)
            {
                genericError.ErrorMessage = filter.ErrorMessage;
                return genericError;
            }

            string query = $@"
                            SELECT
                                *,
                                RoleId as Role
                            FROM [dbo].[User] u
                            WHERE 
                                u.IsActive = 0 
                            AND 
                                u.DeletedAt IS NULL
                            {filter.Response.QueryFilter}
                            ORDER BY u.Id
							OFFSET {currentPage * offset} ROWS 
                            FETCH NEXT {offset} ROWS ONLY";

            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var result = await conn.QueryAsync<Identity>(query);
                    return new GenericResponse<List<Identity>>()
                    {
                        Response = result.ToList(),
                        Sucess = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                    };
                }
            }
            catch (Exception exc)
            {
                genericError.ErrorMessage = exc.Message;
                return genericError;
            }
        }

        public async Task<GenericResponse<Identity>> ApproveAccount(Identity user)
        {
            try
            {
                string query = $@"UPDATE [dbo].[User] SET [dbo].[User].IsActive = 1
                                  OUTPUT 
                                    INSERTED.*,
                                    INSERTED.RoleId as Role
                                  WHERE [dbo].[User].Id = {user.Id}";
                var apiResponse = await Connection.QuerySingleAsync<Identity>(query);
                return new GenericResponse<Identity>()
                {
                    Response = apiResponse,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Sucess = true
                };
            }
            catch (Exception exc)
            {
                return new GenericResponse<Identity>()
                {
                    ErrorMessage = exc.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucess = false
                };
            }
        }

        public async Task<GenericResponse<Identity>> UnapproveAccount(Identity user)
        {
            try
            {
                return await DeleteUserAccount(user.Id, false);
            }
            catch (Exception exc)
            {
                return new GenericResponse<Identity>()
                {
                    ErrorMessage = exc.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucess = false
                };
            }
        }

        public async Task<GenericResponse<Identity>> UpdatePassword(SilentData request, string password)
        {
            var commonError = new GenericResponse<Identity>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    string query = $@"
				        declare @passwordHash varbinary(max) = hashbytes('SHA1', '{Config.TokenizationCredentials.ApiHashKey}'+'{password}')
                        UPDATE [dbo].[user]
                        SET [dbo].[user].[PasswordHash] = @passwordHash
                        OUTPUT INSERTED.*
					    WHERE [dbo].[user].Email = '{request.Email}'
                        AND [dbo].[user].Id = {request.Id}
                    ";
                    var apiResponse = await conn.QueryAsync<Identity>(query);

                    return new GenericResponse<Identity>()
                    {
                        Response = apiResponse.FirstOrDefault(),
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Sucess = true
                    };
                }
            }
            catch (Exception exc)
            {
                commonError.ErrorMessage = exc.Message;
                return commonError;
            }
        }

        public async Task<GenericResponse<bool>> VerifyEmailExists(string email)
        {
            var commonError = new GenericResponse<bool>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            try
            {
                var response = new GenericResponse<bool>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Sucess = true
                };
                using (var conn = Connection)
                {
                    conn.Open();
                    string query = $@"IF (SELECT TOP 1 COUNT(*) FROM [dbo].[User] WHERE LOWER([dbo].[User].Email) = '{email.ToLower()}') > 0
                                      SELECT 1;
                                      ELSE
                                      SELECT 0;";
                    var apiResponse = await conn.QueryAsync<bool>(query);


                    if (apiResponse.AsList().FirstOrDefault())
                        response.Response = true;
                    else
                        response.Response = false;
                    return response;
                }
            }
            catch (Exception exc)
            {
                commonError.ErrorMessage = exc.Message;
                return commonError;
            }

        }

        public async Task<GenericResponse<List<Identity>>> GetActiveUsers(int currentPage, int offset)
        {
            var commonError = new GenericResponse<List<Identity>>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            try
            {
                var filter = await GetAdminForCurrentUser();
                if (!filter.Sucess)
                {
                    commonError.ErrorMessage = filter.ErrorMessage;
                    return commonError;
                }


                string query = $@"SELECT 
                                  u.*,
                                  u.RoleId as Role
                                  FROM [dbo].[User] u
                                  WHERE u.DeletedAt IS NULL
                                  {filter.Response.QueryFilter}
                                  ORDER BY u.Id
								  OFFSET {currentPage * offset} ROWS 
                                  FETCH NEXT {offset} ROWS ONLY";

                using (var conn = Connection)
                {
                    conn.Open();
                    var users = await conn.QueryAsync<Identity>(query);
                    return new GenericResponse<List<Identity>>()
                    {
                        Sucess = true,
                        Response = users.ToList(),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                commonError.ErrorMessage = exc.Message;
                return commonError;
            }
        }

        public async Task<GenericResponse<List<Identity>>> SearchUsers(string key, bool showSuperAdmin = false, Common.Enums.Role role = Common.Enums.Role.NONE, int currentPage = 0, int offset = 10)
        {

            var commonError = new GenericResponse<List<Identity>>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };
            try
            {
                string roleSearch = string.Empty;

                if (showSuperAdmin && role != Common.Enums.Role.NONE)
                {
                    roleSearch = $@" AND ( u.RoleId = {(int)role} OR u.RoleId =  {(int)Common.Enums.Role.SUPER_ADMINISTRADOR} ) ";
                }
                else if (role!= Common.Enums.Role.NONE)
                    roleSearch = $@" AND u.RoleId = {(int)role}";


                var filter = await GetAdminForCurrentUser();
                if (!filter.Sucess)
                {
                    commonError.ErrorMessage = filter.ErrorMessage;
                    return commonError;
                }

                string query = $@"
                                DECLARE @KEY varchar(255) = UPPER('%{key}%');
                                SELECT 
                                u.*
                                FROM [dbo].[User] u
                                WHERE 
                                    u.DeletedAt IS NULL
                                    {roleSearch}
                                AND u.IsActive = 1
                                AND
                                    (
                                     UPPER(u.Name) LIKE @KEY
                                     OR UPPER(u.Lastname) LIKE @KEY
                                    )
                                -- {filter.Response.QueryFilter}
                                ORDER BY u.Id
							    OFFSET {currentPage * offset} ROWS 
                                FETCH NEXT {offset} ROWS ONLY";

                using (var conn = Connection)
                {
                    conn.Open();
                    var response = await conn.QueryAsync<Identity>(query);
                    return new GenericResponse<List<Identity>>()
                    {
                        Sucess = true,
                        Response = response.ToList(),
                        StatusCode = System.Net.HttpStatusCode.OK,
                    };
                }
            }
            catch (Exception exc)
            {
                commonError.ErrorMessage = exc.Message;
                return commonError;
            }
        }

        public async Task<GenericResponse<List<Identity>>> GetUsersById(List<long> users, bool applySuperadminFilter = true)
        {
            var commonError = new GenericResponse<List<Identity>>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            if (users == null || users.Count == 0)
            {
                commonError.ErrorMessage = "No users id provided for identity";
                commonError.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return commonError;
            }

            string extra = string.Empty;
            foreach (int userId in users)
            {
                if (!string.IsNullOrWhiteSpace(extra)) extra += ", ";
                extra += userId;
            }

            try
            {
                //string advancedFilter = string.Empty;
                //if (applySuperadminFilter)
                //{
                //    var adminFilter = await GetAdminForCurrentUser();
                //    if(!adminFilter.Sucess)
                //    {
                //        commonError.ErrorMessage = adminFilter.ErrorMessage;
                //        return commonError;
                //    }
                //    advancedFilter = adminFilter.Response.QueryFilter;
                //}

                string query = $@"SELECT 
                                  u.*,
                                  u.RoleId as Role
                                  FROM [dbo].[User] u
								  WHERE u.Id IN ({extra})
                                  ORDER BY u.Id";

                using (var conn = Connection)
                {
                    conn.Open();
                    var response =await conn.QueryAsync<Identity>(query);
                    return new GenericResponse<List<Identity>>()
                    {
                        Sucess = true,
                        Response = response.ToList(),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                commonError.ErrorMessage = exc.Message;
                return commonError;
            }
        }

        public async Task<GenericResponse<List<Identity>>> GetUsersByRole(Common.Enums.Role id, int currentPage, int offset)
        {
            var commonError = new GenericResponse<List<Identity>>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };
            try
            {

                var filter = await GetAdminForCurrentUser();
                if (!filter.Sucess)
                {
                    commonError.ErrorMessage = filter.ErrorMessage;
                    return commonError;
                }

                string query = $@"
                                SELECT 
                                u.*,
                                u.RoleId as Role
                                FROM [dbo].[User] u
                                WHERE u.RoleId = {(int)id}
                                AND u.DeletedAt IS NULL
                                {filter.Response.QueryFilter}
                                ORDER BY u.Id
							    OFFSET {currentPage * offset} ROWS 
                                FETCH NEXT {offset} ROWS ONLY";

                using (var conn = Connection)
                {
                    conn.Open();
                    var response = await conn.QueryAsync<Identity>(query);
                    return new GenericResponse<List<Identity>>()
                    {
                        Sucess = true,
                        Response = response.ToList(),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                commonError.ErrorMessage = exc.Message;
                return commonError;
            }
        }

        public async Task<GenericResponse<List<Identity>>> GetUsersByEmail(string email, int currentPage, int offset)
        {
            var commonError = new GenericResponse<List<Identity>>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };
            try
            {
                string query = $@"
                                SELECT 
                                u.*,
                                u.RoleId as Role
                                FROM [dbo].[User] u
                                WHERE u.Email = '{email}'
                                AND u.DeletedAt IS NULL
                                ORDER BY u.Id
							    OFFSET {currentPage * offset} ROWS 
                                FETCH NEXT {offset} ROWS ONLY";

                using (var conn = Connection)
                {
                    conn.Open();
                    var response = await conn.QueryAsync<Identity>(query);
                    return new GenericResponse<List<Identity>>()
                    {
                        Sucess = true,
                        Response = response.ToList(),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                commonError.ErrorMessage = exc.Message;
                return commonError;
            }
        }

        public async Task<GenericResponse<Identity>> UpdateUserInformation(Identity request)
        {
            string extra = "";
            if (request.CityId != null && request.StateId != null)
            {
                extra = $@"
                        ,StateId = {request.StateId},
                        CityId = {request.CityId}
                        ";
            }

            try
            {
                string query = $@"
                                UPDATE [User] 
                                SET
	                                Name = '{request.Name}',
	                                Lastname = '{request.Lastname}',
	                                RoleId = {(int)request.Role},
                                    Phone = '{request.Phone}'
                                    {extra}
                                OUTPUT INSERTED.*
                                WHERE  
	                                Id = '{request.Id}'";
                var apiResponse = await Connection.QuerySingleAsync<Identity>(query);
                return new GenericResponse<Identity>()
                {
                    Response = apiResponse,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Sucess = true
                };
            }
            catch (Exception exc)
            {
                return new GenericResponse<Identity>()
                {
                    ErrorMessage = exc.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucess = false
                };
            }
        }
    
        public async Task<GenericResponse<int>> Pagination(int splitBy, string key, bool showSuperAdmin = false, Common.Enums.Role role = Common.Enums.Role.NONE)
        {
            var commonError = new GenericResponse<int>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            try
            {
                using (var conn = Connection)
                {
                    string roleSearch = string.Empty;

                    if (showSuperAdmin && role != Common.Enums.Role.NONE)
                    {
                        roleSearch = $@" AND ( u.RoleId = {(int)role} OR u.RoleId =  {(int)Common.Enums.Role.SUPER_ADMINISTRADOR} ) ";
                    }
                    else if (role!= Common.Enums.Role.NONE)
                        roleSearch = $@" AND u.RoleId = {(int)role}";

                    var filter = await GetAdminForCurrentUser();
                    if (!filter.Sucess)
                    {
                        commonError.ErrorMessage = filter.ErrorMessage;
                        return commonError;
                    }
                    string query = $@"
                                DECLARE @KEY varchar(255) = UPPER('%{key}%');
                                SELECT                                 
                                COUNT(u.Id) AS TotalRows
                                FROM [dbo].[User] u
                                WHERE 
                                    u.DeletedAt IS NULL
                                    {roleSearch}
                                AND u.IsActive = 1
                                AND
                                    (
                                     UPPER(u.Name) LIKE @KEY
                                     OR UPPER(u.Lastname) LIKE @KEY
                                    )                            
                                {filter.Response.QueryFilter}
                                ORDER BY TotalRows";
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<int>(query);

                    var extra = (float)apiResponse%(float)splitBy;
                    return new GenericResponse<int>()
                    {
                        Sucess = true,
                        Response = (apiResponse/splitBy) + (extra!=0 ? 1 : 0),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return new GenericResponse<int>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<UserIdentitySearchPermission>> GetAdminForCurrentUser()
        {
            var error = new GenericResponse<UserIdentitySearchPermission>()
            {
                Response = new UserIdentitySearchPermission()
                {
                    FullSearch = false
                },
                Sucess = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            GenericResponse<UserIdentitySearchPermission> sucess;

            if (AppUser.UserRole == Common.Enums.Role.ADMINISTRADOR|| AppUser.UserRole == Common.Enums.Role.SUPER_ADMINISTRADOR)
            {
                var admin = await GetUsersById(new List<long>() { AppUser.UserID }, false);
                if (admin==null||!admin.Sucess || admin.Response == null || admin.Response.Count==0)
                    return error;

                sucess= new GenericResponse<UserIdentitySearchPermission>()
                {
                    Sucess = admin.Sucess,
                    ErrorMessage = admin.ErrorMessage,
                    Response = new UserIdentitySearchPermission()
                    {
                        Admin = admin.Response.FirstOrDefault(),
                        FullSearch = admin.Response.FirstOrDefault().Role == Common.Enums.Role.SUPER_ADMINISTRADOR
                    },
                    StatusCode = admin.StatusCode
                };
            }
            else if (AppUser.UserRole == Common.Enums.Role.INTERNO_ANALISTA || AppUser.UserRole == Common.Enums.Role.INTERNO_ENTREVISTADOR)
            {
                var internalUser = await GetUsersById(new List<long>() { AppUser.UserID }, false);
                if (internalUser==null||!internalUser.Sucess || internalUser.Response == null || internalUser.Response.Count==0)
                    return error;

                var admin = await GetUsersById(new List<long>() { (long)internalUser.Response.FirstOrDefault().UnderAdminUserId }, false);
                if (admin==null||!admin.Sucess || admin.Response == null || admin.Response.Count==0)
                    return error;

                sucess = new GenericResponse<UserIdentitySearchPermission>()
                {
                    Sucess = admin.Sucess,
                    ErrorMessage = admin.ErrorMessage,
                    Response = new UserIdentitySearchPermission()
                    {
                        Admin = admin.Response.FirstOrDefault(),
                        FullSearch = admin.Response.FirstOrDefault().Role == Common.Enums.Role.SUPER_ADMINISTRADOR
                    },
                    StatusCode = admin.StatusCode
                };

            }
            else if (AppUser.UserRole == Common.Enums.Role.CLIENTES)
            {
                sucess= new GenericResponse<UserIdentitySearchPermission>()
                {
                    Response = new UserIdentitySearchPermission()
                    {
                        FullSearch = true
                    },
                    Sucess = true,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            else
                return error;

            if (!sucess.Sucess||sucess.Response == null)
            {
                error.ErrorMessage = "Admin user not valid for this user";
                return error;
            }
            if (!sucess.Response.FullSearch)
            {
                sucess.Response.QueryFilter = $@" AND (UnderAdminUserId = {sucess.Response.Admin.Id} OR UnderAdminUserId IS null)";
                sucess.Response.AdminId = sucess.Response.Admin ==null ? 0 : sucess.Response.Admin.Id;
            }
            else
            {
                sucess.Response.QueryFilter = $@"";
                sucess.Response.AdminId = sucess.Response.Admin ==null? 0: sucess.Response.Admin.Id;
            }

            return sucess;
        }
    }
}
