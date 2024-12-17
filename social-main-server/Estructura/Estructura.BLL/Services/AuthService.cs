using Estructura.Common.Models;
using Estructura.Common.Request;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Estructura.Core.Services;
using Estructura.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.BLL.Services
{
    public class AuthService : IAuthService
    {
        IUtilitiesService utilities;
        IUtilitiesRepository utilitiesRepository;
        ITokenUtil tokenUtil;
        IAuthRepository authRepository;
        public AuthService(IUtilitiesService utilities, IUtilitiesRepository utilitiesRepository, ITokenUtil tokenUtil, IAuthRepository authRepository)
        {
            this.utilities = utilities;
            this.tokenUtil = tokenUtil;
            this.authRepository = authRepository;
            this.utilitiesRepository = utilitiesRepository;
        }


        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            //Verify credentials integrity
            if (string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Email))
                return new LoginResponse() { StatusCode = System.Net.HttpStatusCode.Unauthorized };

            //Review app compatibility (Killerswitch)
            //if (!(await utilities.IsAppCompatible(request.AppBuildVersion)).Response)
            //    return new LoginResponse() { StatusCode = System.Net.HttpStatusCode.NotAcceptable };

            //Decode user credentials
            try
            {
                //cmljYXJkby5iZWxtb250MjFAZ21haWwuY29t
                //MTIzNDU=
                request.Email = Common.Utilities.EncodingHelper.DecodeFrom64(request.Email);
                request.Password = Common.Utilities.EncodingHelper.DecodeFrom64(request.Password);
            }
            catch (Exception)
            {
                return new LoginResponse() { StatusCode = System.Net.HttpStatusCode.Unauthorized };
            }

            //Get identity
            var login = await authRepository.LoginAsync(request);

            //Verify identity integrity
            if (login == null || login.Identity == null)
                return new LoginResponse() { StatusCode = System.Net.HttpStatusCode.Unauthorized };

            //Setup token (rehuse, refresh or create)
            var refreshToken = await authRepository.GetRefreshTokenFromDB(login.Identity.Id);
            if (refreshToken != null && refreshToken.IssuedServerDate.AddDays(refreshToken.DaysToLive) <= DateTime.UtcNow)
            {
                await authRepository.DeleteRefreshToken(refreshToken.Token);
                refreshToken = null;
            }

            //Refresh token is for all environments, slide tokens can work on sandbox, production etc but it will be created on each database environment
            //Create slide/refresh token and store if necessary (if expired or not exist)
            var claims = tokenUtil.GetClaims(login.Identity);
            var tokenExp = tokenUtil.GetAndStoreJWTToken(claims);//here all ok, we have the jwt token
            login.Token = tokenExp.Token;
            login.SlidingExpiration = tokenExp.SlidingExpiration;
            //uncomment after all expiration tokens get expired, old hash need to be removed
            //login.RefreshToken = refreshToken == null ? await BuildAndStoreRefreshToken(login.Identity) : refreshToken.Token;
            login.RefreshToken = await BuildAndStoreRefreshToken(login.Identity);
            var tokenFromDB = await authRepository.GetRefreshTokenFromDB(login.Identity.Id);
            await authRepository.DeleteAllOldTokens(tokenFromDB);//delete all old refresh tokens, including not matching hash tokens
            login.StatusCode = System.Net.HttpStatusCode.OK;

            //Finish
            return login;
        }

        public async Task<RefreshTokenResponse> RefreshAsync(RefreshTokenRequest request)
        {
            var commonBadTokenResponse = new RefreshTokenResponse()
            {
                ErrorMessage = "Unauthorized",
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Sucess = false,
            };
            if (!(await utilities.IsAppCompatible(request.AppBuildVersion)).Response)
                return new RefreshTokenResponse() { StatusCode = System.Net.HttpStatusCode.NotAcceptable };

            Dictionary<string, string> decryptedToken;
            RefreshToken storedRefreshToken = null;
            try
            {
                decryptedToken = tokenUtil.Decode(request.Token);
                storedRefreshToken = await authRepository.GetRefreshTokenFromDB(request.Token);
            }
            catch (Exception exc)
            {
                commonBadTokenResponse.ErrorMessage = exc.Message;
                return commonBadTokenResponse;
            }
            //check stored token exists
            if (storedRefreshToken == null || decryptedToken == null)
            {
                commonBadTokenResponse.ErrorMessage = "Not valid token";
                return commonBadTokenResponse;//Log some token not found stuff
            }

            if (storedRefreshToken.IssuedServerDate.AddDays(storedRefreshToken.DaysToLive) < DateTime.UtcNow)
            {
                //Log some stuff about expired token
                await authRepository.DeleteRefreshToken(storedRefreshToken.Token);
                commonBadTokenResponse.ErrorMessage = "Expired credentials, login again";
                return commonBadTokenResponse;
            }
            if (!storedRefreshToken.IsValid)
            {
                //Log some user token login capabilities revoked
                await authRepository.DeleteRefreshToken(storedRefreshToken.Token);
                commonBadTokenResponse.ErrorMessage = "Not Valid Token";
                return commonBadTokenResponse;
            }
            //if (storedRefreshToken.ChallengeHash !=  System.Text.Encoding.UTF8.GetString(tokenUtil.Hash(decryptedToken["Challenge"])))
            var fromdb = System.Text.Encoding.ASCII.GetString(tokenUtil.Hash(decryptedToken["Challenge"]));
            var fromclient = tokenUtil.Hash(decryptedToken["Challenge"]);
            if (storedRefreshToken.ChallengeHash != System.Text.Encoding.ASCII.GetString(tokenUtil.Hash(decryptedToken["Challenge"])))
            {
                //Log some tricky/modified token
                commonBadTokenResponse.ErrorMessage = "Not valid token";
                return commonBadTokenResponse;
            }
            var ident = await authRepository.GetIdentity(storedRefreshToken.UserID);

            var claims = tokenUtil.GetClaims(ident);
            var tokenExp = tokenUtil.GetAndStoreJWTToken(claims);
            return new RefreshTokenResponse
            {
                Identity = ident,
                Token = tokenExp.Token,
                RefreshToken = storedRefreshToken.Token,
                SlidingExpiration = tokenExp.SlidingExpiration,
                Sucess = true,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        private async Task<string> BuildAndStoreRefreshToken(Estructura.Common.Models.Identity input)
        {
            //build encrypted token
            var encryptedRefreshToken = tokenUtil.CreateRefreshToken(input);
            //decrypt token to get challenge
            var decryptedRefreshToken = tokenUtil.Decode(encryptedRefreshToken);
            //hash the challenge
            var hashedChallenge = tokenUtil.Hash(decryptedRefreshToken["Challenge"]);
            //save token, hashed challenge, CustomerID, and Email to DB
            bool result = await authRepository.StoreRefreshToken(tokenUtil.Decode(encryptedRefreshToken), encryptedRefreshToken, hashedChallenge, input);

            return encryptedRefreshToken;
        }


    }
}
