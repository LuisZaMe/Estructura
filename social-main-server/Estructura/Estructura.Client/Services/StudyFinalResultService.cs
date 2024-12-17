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
    public class StudyFinalResultService : IStudyFinalResultService
    {
        private readonly HttpClient _client;
        public StudyFinalResultService(HttpClient _client)
        {
            this._client=_client;
        }

        public async Task<GenericResponse<StudyFinalResult>> CreateStudyFinalResult(StudyFinalResult request)
        {
            var response = await _client.PostAsync($"StudyFinalResult", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyFinalResult>>();
            return result;
        }

        public async Task<GenericResponse<StudyFinalResult>> DeleteStudyFinalResult(long id)
        {
            var response = await _client.DeleteAsync($"StudyFinalResult?id={id}");
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyFinalResult>>();
            return result;
        }

        public async Task<GenericResponse<List<StudyFinalResult>>> GetStudyFinalResult(List<long> id, int currentPage, int offset)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($"StudyFinalResult?{ids}currentPage={currentPage}&offset={offset}");
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudyFinalResult>>>();
            return result;
        }

        public async Task<GenericResponse<StudyFinalResult>> UpdateStudyFinalResult(StudyFinalResult request)
        {
            var response = await _client.PutAsync($"StudyFinalResult", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyFinalResult>>();
            return result;
        }
    }
}
