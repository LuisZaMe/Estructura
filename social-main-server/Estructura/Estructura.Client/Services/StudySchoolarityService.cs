using Estructura.Client.Interfaces;
using Estructura.Client.Utilities;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Services
{
    public class StudySchoolarityService : IStudySchoolarityService
    {
        private readonly HttpClient _client;
        public StudySchoolarityService(HttpClient _client)
        {
            this._client=_client;
        }



        //StudySchoolarity
        public async Task<GenericResponse<StudySchoolarity>> CreateStudySchoolarity(StudySchoolarity request)
        {
            var response = await _client.PostAsync($"StudySchoolarity", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudySchoolarity>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudySchoolarity>(response);
        }

        public async Task<GenericResponse<StudySchoolarity>> DeleteStudySchoolarity(long id)
        {
            var response = await _client.DeleteAsync($"StudySchoolarity?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudySchoolarity>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudySchoolarity>(response);
        }

        public async Task<GenericResponse<List<StudySchoolarity>>> GetStudySchoolarity(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($"StudySchoolarity?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudySchoolarity>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudySchoolarity>>(response);
        }

        public async Task<GenericResponse<StudySchoolarity>> UpdateStudySchoolarity(StudySchoolarity request)
        {
            var response = await _client.PutAsync($"StudySchoolarity", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudySchoolarity>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudySchoolarity>(response);
        }




        //Schoolarity
        public async Task<GenericResponse<List<Scholarity>>> CreateSchoolarity(List<Scholarity> request)
        {
            var response = await _client.PostAsync($"StudySchoolarity/Schoolarity", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Scholarity>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Scholarity>>(response);
        }

        public async Task<GenericResponse<Scholarity>> UpdateSchoolarity(Scholarity request)
        {
            var response = await _client.PutAsync($"StudySchoolarity/Schoolarity", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Scholarity>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Scholarity>(response);
        }

        public async Task<GenericResponse<List<Scholarity>>> GetSchoolarity(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($"StudySchoolarity/Schoolarity?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Scholarity>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Scholarity>>(response);
        }

        public async Task<GenericResponse<Scholarity>> DeleteSchoolarity(long id)
        {
            var response = await _client.DeleteAsync($"StudySchoolarity/Schoolarity?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Scholarity>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Scholarity>(response);
        }




        // Extracurricular Activities
        public async Task<GenericResponse<ExtracurricularActivities>> DeleteExtracurricularActivities(long id)
        {
            var response = await _client.DeleteAsync($"StudySchoolarity/ExtracurricularActivities?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<ExtracurricularActivities>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<ExtracurricularActivities>(response);
        }

        public async Task<GenericResponse<List<ExtracurricularActivities>>> GetExtracurricularActivities(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($"StudySchoolarity/ExtracurricularActivities?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<ExtracurricularActivities>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<ExtracurricularActivities>>(response);
        }

        public async Task<GenericResponse<ExtracurricularActivities>> UpdateExtracurricularActivities(ExtracurricularActivities request)
        {
            var response = await _client.PutAsync($"StudySchoolarity/ExtracurricularActivities", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<ExtracurricularActivities>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<ExtracurricularActivities>(response);
        }

        public async Task<GenericResponse<List<ExtracurricularActivities>>> CreateExtracurricularActivities(List<ExtracurricularActivities> request)
        {
            var response = await _client.PostAsync($"StudySchoolarity/ExtracurricularActivities", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<ExtracurricularActivities>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<ExtracurricularActivities>>(response);
        }
    }
}
