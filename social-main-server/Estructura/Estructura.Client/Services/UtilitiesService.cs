using Estructura.Client.Interfaces;
using Estructura.Client.Utilities;
using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Services
{
    public class UtilitiesService : IUtilitiesService
    {
        private readonly HttpClient _client;
        public UtilitiesService(HttpClient _client)
        {
            this._client = _client;
        }

        public async Task<GenericResponse<string>> GetCurrentVersion()
        {
            var response = await _client.GetAsync("Utilities/CurrentVersion");
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<string>>();
            return result;
        }

        public async Task<GenericResponse<bool>> IsAppCompatible(int appBuildVersion)
        {
            var response = await _client.GetAsync($"Utilities/IsAppCompatible?appBuildVersion={appBuildVersion}");
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<bool>>();
            return result;
        }

        public async Task<GenericResponse<bool>> VerifyEmailExist(string email)
        {
            var response = await _client.GetAsync($"Utilities/VerifyEmailExist?email={email}");
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<bool>>();
            return result;
        }

        public async Task<GenericResponse<List<City>>> GetCities(int stateId)
        {
            var response = await _client.GetAsync($"Utilities/GetCities?stateId={stateId}");
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<City>>>();
            return result;
        }

        public async Task<GenericResponse<List<State>>> GetStates()
        {
            var response = await _client.GetAsync($"Utilities/GetStates");
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<State>>>();
            return result;
        }
    }
}
