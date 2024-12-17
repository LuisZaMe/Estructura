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
    public class StudyIMSSValidationService: IStudyIMSSValidationService
    {
        private readonly HttpClient _client;
        public StudyIMSSValidationService(HttpClient _client)
        {
            this._client=_client;
        }


        //StudyIMSSValidation
        public async Task<GenericResponse<StudyIMSSValidation>> CreateStudyIMSSValidation(StudyIMSSValidation request)
        {
            var response = await _client.PostAsync("StudyIMSSValidation", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyIMSSValidation>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyIMSSValidation>(response);
        }

        public async Task<GenericResponse<StudyIMSSValidation>> DeleteStudyIMSSValidation(long id)
        {
            var response = await _client.DeleteAsync($@"StudyIMSSValidation?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyIMSSValidation>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyIMSSValidation>(response);
        }

        public async Task<GenericResponse<List<StudyIMSSValidation>>> GetStudyIMSSValidation(List<long> id)
        {
            string ids = string.Empty;
            id.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"id={e}";
            });
            var response = await _client.GetAsync($@"StudyIMSSValidation?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudyIMSSValidation>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudyIMSSValidation>>(response);

        }

        public async Task<GenericResponse<StudyIMSSValidation>> UpdateStudyIMSSValidation(StudyIMSSValidation request)
        {
            var response = await _client.PutAsync("StudyIMSSValidation", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyIMSSValidation>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyIMSSValidation>(response);

        }



        //IMSSValidation
        public async Task<GenericResponse<List<IMSSValidation>>> CreateIMSSValidation(List<IMSSValidation> request)
        {
            var response = await _client.PostAsync("StudyIMSSValidation/IMSSValidation", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<IMSSValidation>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<IMSSValidation>>(response);
        }

        public async Task<GenericResponse<IMSSValidation>> DeleteIMSSValidation(long id)
        {
            var response = await _client.DeleteAsync($@"StudyIMSSValidation/IMSSValidation?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<IMSSValidation>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<IMSSValidation>(response);
        }

        public async Task<GenericResponse<List<IMSSValidation>>> GetIMSSValidation(List<long> id)
        {
            string ids = string.Empty;
            id.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"id={e}";
            });
            var response = await _client.GetAsync($@"StudyIMSSValidation/IMSSValidation?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<IMSSValidation>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<IMSSValidation>>(response);
        }

        public async Task<GenericResponse<IMSSValidation>> UpdateIMSSValidation(IMSSValidation request)
        {
            var response = await _client.PutAsync("StudyIMSSValidation/IMSSValidation", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<IMSSValidation>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<IMSSValidation>(response);
        }
    }
}
