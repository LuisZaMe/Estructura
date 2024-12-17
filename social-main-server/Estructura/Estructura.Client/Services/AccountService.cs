using Estructura.Client.Interfaces;
using Estructura.Client.Utilities;
using Estructura.Common.Models;
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
    public class AccountService: IAccountService
    {
        private readonly HttpClient _client;
        public AccountService(HttpClient _client)
        {
            this._client = _client;
        }


        public async Task<GenericResponse<Identity>> CreateSuperadmin(Identity request)
        {
            var response = await _client.PostAsync("Account/Superadmin", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Identity>>();
                return result;
            }
            else
                return await HttpErrorParser.ErrorParser<Identity>(response);
        }

        public async Task<GenericResponse<Identity>> Approve(long Id)
        {
            var response = await _client.PutAsync($@"Account/{Id}/Approve", new StringContent(JsonConvert.SerializeObject(new { Id }), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<Identity>>();
            return result;
        }

        public async Task<GenericResponse<Identity>> Create(Identity request)
        {
            var response = await _client.PostAsync("Account", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<Identity>>();
            return result;
        }

        public async Task<GenericResponse<Identity>> Delete(long Id)
        {
            var response = await _client.DeleteAsync($@"Account?Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Identity>>();
                return result;
            }
            else
                return await HttpErrorParser.ErrorParser<Identity>(response);
        }

        public async Task<GenericResponse<List<Identity>>> Get(List<long> Id, int currentPage, int offset)
        {
            string extra = string.Empty;
            for(int x = 0; x<Id.Count; x++)
            {
                if (!string.IsNullOrWhiteSpace(extra)) extra+="&";
                extra += "Id="+Id[x];
            }
            var response = await _client.GetAsync($@"Account?{extra}&currentPage={currentPage}&offset={offset}");
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Identity>>>();
            return result;
        }

        public async Task<GenericResponse<List<Identity>>> Pending(int currentPage, int offset)
        {
            var response = await _client.GetAsync($@"Account/Pending?currentPage={currentPage}&offset={offset}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Identity>>>();
                return result;
            }
            return new GenericResponse<List<Identity>>() { StatusCode = response.StatusCode };
        }

        public async Task<GenericResponse<Identity>> Reject(long Id)
        {
            var response = await _client.PutAsync($@"Account/{Id}/Reject", new StringContent(JsonConvert.SerializeObject(new { Id }), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Identity>>();
                return result;
            }
            return new GenericResponse<Identity>() { StatusCode = response.StatusCode };
        }

        public async Task<GenericResponse<List<Identity>>> Search(string key, Common.Enums.Role role = Common.Enums.Role.NONE, int currentPage = 0, int offset = 10)
        {
            var response = await _client.GetAsync($@"Account/Search?key={key}&role={role}&currentPage={currentPage}&offset={offset}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Identity>>>();
                return result;
            }
            return new GenericResponse<List<Identity>>() { StatusCode = response.StatusCode };
        }

        public async Task<GenericResponse<List<Identity>>> Role(Common.Enums.Role role, int currentPage, int offset)
        {
            var response = await _client.GetAsync($@"Account/Role?role={role}&currentPage={currentPage}&offset={offset}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Identity>>>();
                return result;
            }
            return new GenericResponse<List<Identity>>() { StatusCode = response.StatusCode };
        }

        public async Task<GenericResponse<bool>> SendRecoverPasswordMail(RecoverPasswordRequest request)
        {
            var response = await _client.PostAsync($@"Account/SendRecoverPasswordMail", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<bool>>();
                return result;
            }
            return new GenericResponse<bool>() { StatusCode = response.StatusCode };
        }

        public async Task<GenericResponse<bool>> RecoverPasswordByMail(CompleteRecoverPasswordRequest request)
        {
            var response = await _client.PostAsync($@"Account/RecoverPasswordByMail", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<bool>>();
                return result;
            }
            return new GenericResponse<bool>() { StatusCode = response.StatusCode };
        }

        public async Task<GenericResponse<Identity>> UpdateUserInformation(Identity request)
        {
            var response = await _client.PutAsync("Account", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Identity>>();
                return result;
            }
            else
                return new GenericResponse<Identity>() { StatusCode = response.StatusCode };
        }

        public async Task<GenericResponse<Identity>> CompleteAccountRegistration(CompleteRegistration request)
        {
            var response = await _client.PutAsync($@"Account/CompleteRegistration", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Identity>>();
                return result;
            }
            return new GenericResponse<Identity>() { StatusCode = response.StatusCode };
        }

        public async Task<GenericResponse<int>> Pagination(int splitBy, string key, Common.Enums.Role role = Common.Enums.Role.NONE)
        {
            var response = await _client.GetAsync($@"Account/Pagination?splitBy={splitBy}&key={key}&role={(int)role}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<int>>();
                return result;
            }
            return new GenericResponse<int>() { StatusCode = response.StatusCode };
        }
    }
}
