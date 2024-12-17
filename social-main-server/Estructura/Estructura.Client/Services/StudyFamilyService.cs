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
    public class StudyFamilyService: IStudyFamilyService
    {
        private readonly HttpClient _client;
        public StudyFamilyService(HttpClient _client)
        {
            this._client=_client;
        }



        //StudyFamily
        public async Task<GenericResponse<StudyFamily>> CreateStudyFamily(StudyFamily request)
        {
            var response = await _client.PostAsync($"StudyFamily", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyFamily>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyFamily>(response);
        }

        public async Task<GenericResponse<StudyFamily>> DeleteStudyFamily(long id)
        {
            var response = await _client.DeleteAsync($"StudyFamily?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyFamily>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyFamily>(response);
        }

        public async Task<GenericResponse<List<StudyFamily>>> GetStudyFamily(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($"StudyFamily?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudyFamily>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudyFamily>>(response);
        }

        public async Task<GenericResponse<StudyFamily>> UpdateStudyFamily(StudyFamily request)
        {
            var response = await _client.PutAsync($"StudyFamily", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyFamily>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyFamily>(response);
        }



        // LivingFamily
        public async Task<GenericResponse<List<LivingFamily>>> CreateLivingFamily(List<LivingFamily> request)
        {
            var response = await _client.PostAsync($"StudyFamily/LivingFamily", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<LivingFamily>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<LivingFamily>>(response);
        }

        public async Task<GenericResponse<LivingFamily>> DeleteLivingFamily(long id)
        {
            var response = await _client.DeleteAsync($"StudyFamily/LivingFamily?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<LivingFamily>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<LivingFamily>(response);
        }

        public async Task<GenericResponse<List<LivingFamily>>> GetLivingFamily(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($"StudyFamily/LivingFamily?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<LivingFamily>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<LivingFamily>>(response);
        }

        public async Task<GenericResponse<LivingFamily>> UpdateLivingFamily(LivingFamily request)
        {
            var response = await _client.PutAsync($"StudyFamily/LivingFamily", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<LivingFamily>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<LivingFamily>(response);
        }



        //NoLivingFamily
        public async Task<GenericResponse<List<NoLivingFamily>>> CreateNoLivingFamily(List<NoLivingFamily> request)
        {
            var response = await _client.PostAsync($"StudyFamily/NoLivingFamily", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<NoLivingFamily>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<NoLivingFamily>>(response);
        }

        public async Task<GenericResponse<NoLivingFamily>> UpdateNoLivingFamily(NoLivingFamily request)
        {
            var response = await _client.PutAsync($"StudyFamily/NoLivingFamily", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<NoLivingFamily>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<NoLivingFamily>(response);
        }

        public async Task<GenericResponse<List<NoLivingFamily>>> GetNoLivingFamily(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($"StudyFamily/NoLivingFamily?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<NoLivingFamily>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<NoLivingFamily>>(response);
        }

        public async Task<GenericResponse<NoLivingFamily>> DeleteNoLivingFamily(long id)
        {
            var response = await _client.DeleteAsync($"StudyFamily/NoLivingFamily?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<NoLivingFamily>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<NoLivingFamily>(response);
        }
    }
}
