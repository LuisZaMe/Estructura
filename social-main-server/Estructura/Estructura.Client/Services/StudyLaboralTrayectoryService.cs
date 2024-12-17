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
    public class StudyLaboralTrayectoryService : IStudyLaboralTrayectoryService
    {
        private readonly HttpClient _client;
        public StudyLaboralTrayectoryService(HttpClient _client)
        {
            this._client=_client;
        }

        public async Task<GenericResponse<List<StudyLaboralTrayectory>>> CreateStudyLaboralTrayectory(List<StudyLaboralTrayectory> request)
        {
            var response = await _client.PostAsync("StudyLaboralTrayectory", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudyLaboralTrayectory>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudyLaboralTrayectory>>(response);
        }

        public async Task<GenericResponse<StudyLaboralTrayectory>> DeleteStudyLaboralTrayectory(long id)
        {
            var response = await _client.DeleteAsync($@"StudyLaboralTrayectory?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyLaboralTrayectory>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyLaboralTrayectory>(response);
        }

        public async Task<GenericResponse<List<StudyLaboralTrayectory>>> GetStudyLaboralTrayectory(List<long> id)
        {
            string request = string.Empty;
            id.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(request)) request+="&";
                request+=$@"id={e}";
            });

            var response = await _client.GetAsync($@"StudyLaboralTrayectory?{request}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudyLaboralTrayectory>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudyLaboralTrayectory>>(response);
        }

        public async Task<GenericResponse<StudyLaboralTrayectory>> UpdateStudyLaboralTrayectory(StudyLaboralTrayectory request)
        {
            var response = await _client.PutAsync("StudyLaboralTrayectory", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyLaboralTrayectory>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyLaboralTrayectory>(response);
        }
    }
}
