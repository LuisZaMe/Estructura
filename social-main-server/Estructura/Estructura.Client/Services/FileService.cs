using Estructura.Client.Interfaces;
using Estructura.Client.Utilities;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Services
{
    public class FileService : IFileService
    {
        private readonly HttpClient _client;

        public FileService(HttpClient _client)
        {
            this._client = _client;
        }

        public async Task<GenericResponse<Doccument>> CreateFile(Doccument request)
        {
            var response = await _client.PostAsync($"File", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<Doccument>>();
            return result;
        }
    }
}
