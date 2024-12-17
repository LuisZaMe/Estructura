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
    public class StudyPersonalReferenceService : IStudyPersonalReferenceService
    {
        private readonly HttpClient _client;
        public StudyPersonalReferenceService(HttpClient _client)
        {
            this._client=_client;
        }
        public async Task<GenericResponse<List<StudyPersonalReference>>> CreateStudyPersonalReference(List<StudyPersonalReference> request)
        {
            var response = await _client.PostAsync("StudyPersonalReference", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudyPersonalReference>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudyPersonalReference>>(response);

        }

        public async Task<GenericResponse<StudyPersonalReference>> DeleteStudyPersonalReference(long id)
        {
            var response = await _client.DeleteAsync($@"StudyPersonalReference?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyPersonalReference>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyPersonalReference>(response);

        }

        public async Task<GenericResponse<List<StudyPersonalReference>>> GetStudyPersonalReference(List<long> id, bool byStudy = false)
        {
            string request = string.Empty;
            id.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(request)) request+="&";
                request+=$@"id={e}";
            });

            var response = await _client.GetAsync($@"StudyPersonalReference?{request}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudyPersonalReference>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudyPersonalReference>>(response);

        }

        public async Task<GenericResponse<StudyPersonalReference>> UpdateStudyPersonalReference(StudyPersonalReference request)
        {
            var response = await _client.PutAsync("StudyPersonalReference", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyPersonalReference>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyPersonalReference>(response);

        }
    }
}
