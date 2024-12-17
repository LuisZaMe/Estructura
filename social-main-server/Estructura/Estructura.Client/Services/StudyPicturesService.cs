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
    public class StudyPicturesService : IStudyPicturesService
    {
        private readonly HttpClient _client;
        public StudyPicturesService(HttpClient _client)
        {
            this._client=_client;
        }
        public async Task<GenericResponse<StudyPictures>> CreateStudyPictures(StudyPictures request)
        {
            var response = await _client.PostAsync("StudyPictures", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyPictures>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyPictures>(response);
        }

        public async Task<GenericResponse<StudyPictures>> DeleteStudyPictures(long id)
        {
            var response = await _client.DeleteAsync($@"StudyPictures?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyPictures>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyPictures>(response);
        }

        public async Task<GenericResponse<List<StudyPictures>>> GetStudyPictures(List<long> id, bool byStudy = false)
        {
            string request = string.Empty;
            id.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(request)) request+="&";
                request+=$@"id={e}";
            });

            var response = await _client.GetAsync($@"StudyPictures?{request}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudyPictures>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudyPictures>>(response);
        }

        public async Task<GenericResponse<StudyPictures>> UpdateStudyPictures(StudyPictures request)
        {
            var response = await _client.PutAsync("StudyPictures", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyPictures>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyPictures>(response);
        }
    }
}
