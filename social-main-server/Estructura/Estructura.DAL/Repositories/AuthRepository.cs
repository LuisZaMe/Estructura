using Estructura.Common.Models;
using Estructura.Common.Request;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Estructura.DAL.Repositories
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        public AuthRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) { }

        public async Task<LoginResponse> LoginAsync(LoginRequest input)
        {
            //Get identity
            Common.Models.Identity ident = null;
            string query = $@" 
                    BEGIN  
                    SET NOCOUNT ON  
                     DECLARE 
						@Password VARCHAR(255)='{input.Password}',
						@Email NVARCHAR(100) = '{input.Email}' , 
						@TestPassword VARBINARY(MAX),
						@PasswordHash VARBINARY(MAX), 
						@UserID INT, 
						@IsAuthenticated BIT = 0  
                     SELECT @PasswordHash = PasswordHash, @UserID= Id 
                     FROM [dbo].[User]  
                     WHERE Email=@Email
                     AND [dbo].[User].IsActive = 1
                     AND [dbo].[User].DeletedAt IS NULL
                     IF @PasswordHash IS NOT NULL BEGIN
					  SET @TestPassword = hashbytes('SHA1', '{Config.TokenizationCredentials.ApiHashKey}'+ @Password)
                      SET @IsAuthenticated = CASE WHEN @PasswordHash = @TestPassword THEN 1 ELSE 0 END  
                     END  
                     IF @IsAuthenticated=1 BEGIN  
                        SELECT *, u.RoleId
                        FROM [dbo].[User] as u
                    WHERE
                    u.Id = @UserID
                     END ELSE BEGIN  
                      SELECT* FROM [dbo].[User] WHERE 1=0  
                     END  
                    END";
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var response = await conn.QueryAsync<Common.Models.Identity, int?, Common.Models.Identity>(query, (userIdentity, roleId)=>
                    {
                        roleId = roleId!=null ? roleId : 0;
                        userIdentity.Role = (Common.Enums.Role)int.Parse(roleId.ToString());
                        return userIdentity;
                    }, splitOn: "Id, RoleId");
                    ident= response.FirstOrDefault();
                }
            }
            catch (Exception exc)
            {
                return new LoginResponse()
                {
                    ErrorMessage = exc.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }

            if (ident == null || ident.Id == 0)
                return new LoginResponse()
                {
                    ErrorMessage = "User not found or not allowed",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };

            return new LoginResponse()
            {
                Identity = ident,
                StatusCode = System.Net.HttpStatusCode.OK,
                Sucess = true,
            };
        }
        public async Task<RefreshToken> GetRefreshTokenFromDB(string token)
        {
            string query = $@"SELECT 
                           [Token]
                          ,[ChallengeHash]
                          ,[UserId]
                          ,[Email]
                          ,[IssuedServerDate]
                          ,[DaysToLive]
                          ,[IsValid]
                    FROM RefreshTokens
                    WHERE
                        Token = '{token}'";
            RefreshToken refreshToken = new RefreshToken();
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var result = await conn.ExecuteReaderAsync(query);
                    while (result.Read())
                    {
                        refreshToken = new RefreshToken()
                        {
                            UserID = result.GetInt32("UserId"),
                            ChallengeHash = result.GetString("ChallengeHash"),
                            DaysToLive = result.GetInt32("DaysToLive"),
                            Email = result.GetString("Email"),
                            IssuedServerDate = result.GetDateTime("IssuedServerDate"),
                            IsValid = result.GetBoolean("IsValid"),
                            Token = result.GetString("Token")
                        };
                        break;
                    }
                }
            }
            catch
            {
                return null;
            }
            return refreshToken;
        }
        public async Task<RefreshToken> GetRefreshTokenFromDB(long userId)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<RefreshToken>($@"
                    SELECT TOP 2 
                           [Token]
                          ,[ChallengeHash]
                          ,[UserId]
                          ,[Email]
                          ,[IssuedServerDate]
                          ,[DaysToLive]
                          ,[IsValid]
                    FROM [dbo].[RefreshTokens]
                    WHERE
                        UserId = @ui
                    ORDER BY IssuedServerDate DESC
                    ", new { ui = userId });
                    if (apiResponse == null || apiResponse.Count() == 0) return null;
                    if (apiResponse.Count() > 1)
                    {
                        //Delete all the old tokens except the newest one
                        var currentToken = apiResponse.FirstOrDefault();
                        var deletedItems = await DeleteAllOldTokens(currentToken);
                        if (deletedItems > 0)
                            return currentToken;
                        return null;
                    }
                    return apiResponse.FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }
        public async Task<int> DeleteAllOldTokens(RefreshToken tokenToMaintain)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    return 0;
                    /*var apiResponse = await conn.ExecuteAsync($@"
                       DELETE FROM 
	                        RefreshTokens 
                        WHERE UserId=@uid 
	                        AND Token <> '{tokenToMaintain.Token}'
                        ", new
                    {
                        uid = tokenToMaintain.UserID
                    });
                    return apiResponse;*/
                }
            }
            catch
            {
                return 0;
            }
        }
        public async Task<bool> DeleteRefreshToken(string token)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.ExecuteAsync($@"
                        DELETE FROM RefreshTokens
                        WHERE 
                            Token = @token
                        ", new { token = token });
                }
            }
            //get some help from moffit on this one to also delete records too old.
            catch
            {
                return false;
            }
            return true;
        }
        public async Task<bool> StoreRefreshToken(Dictionary<string, string> decryptedRefreshToken, string token, byte[] hash, Common.Models.Identity customer)
        {
            var ch = Encoding.ASCII.GetString(hash);
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.ExecuteAsync($@"
                        INSERT INTO [dbo].[RefreshTokens] (
	                        Token, 
	                        ChallengeHash, 
	                        UserId, 
	                        Email, 
	                        IssuedServerDate, 
	                        DaysToLive, 
	                        IsValid)
                        VALUES (
	                        @Token, 
	                        @ChallengeHash, 
	                        @UserId,
	                        @Email,
	                        @IssuedServerDate,
	                        @DaysToLive,
	                        @IsValid)
                        ", new
                    {
                        Token = token,
                        ChallengeHash = System.Text.Encoding.ASCII.GetString(hash),
                        UserId = customer.Id,
                        Email = customer.Email,
                        IssuedServerDate = decryptedRefreshToken["IssuedServerDate"],
                        DaysToLive = decryptedRefreshToken["DaysToLive"],
                        IsValid = true
                    });
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public async Task<Common.Models.Identity> GetIdentity(long userId)
        {
            string query = $@"SELECT [dbo].[User].Id, * FROM [dbo].[User] WHERE [dbo].[User].Id = {userId}";
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var result = await conn.QueryAsync<Common.Models.Identity>(query);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return null;
            }
        }
    }
}
