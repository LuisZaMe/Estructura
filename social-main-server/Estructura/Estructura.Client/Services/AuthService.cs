using Estructura.Client.Interfaces;
using Estructura.Common.Request;
using Estructura.Common.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;
        public AuthService(HttpClient _client)
        {
            this._client = _client;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            LoginResponse result;
            try
            {
                request.Email = Common.Utilities.EncodingHelper.EncodeTo64(request.Email);
                request.Password = Common.Utilities.EncodingHelper.EncodeTo64("A1B2C3D4e5"); // request.Password);
                var postContent = JsonConvert.SerializeObject(request);
                var requestContent = new StringContent(postContent, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("Auth/Login", requestContent);
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<LoginResponse>(content);
            }
            catch (Exception exc)
            {
                result = new LoginResponse()
                {
                    ErrorMessage = "Error encoding credentials " + exc.ToString(),
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucess = false
                };
            }
            return result;
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            RefreshTokenResponse result;
            try
            {
                var postContent = JsonConvert.SerializeObject(request);
                var requestContent = new StringContent(postContent, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("Auth/RefreshToken", requestContent);
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<RefreshTokenResponse>(content);
            }
            catch (Exception exc)
            {
                result = new RefreshTokenResponse()
                {
                    ErrorMessage = "Error on refresh token " + exc.ToString(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Sucess = false
                };
            }
            return result;
        }
    }
}
