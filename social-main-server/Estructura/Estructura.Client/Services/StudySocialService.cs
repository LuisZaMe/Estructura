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
    public class StudySocialService: IStudySocialService
    {
        private readonly HttpClient _client;
        public StudySocialService(HttpClient _client)
        {
            this._client=_client;
        }

        // Study Social
        public async Task<GenericResponse<StudySocial>> CreateStudySocial(StudySocial request)
        {
            var response = await _client.PostAsync($"StudySocial", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudySocial>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudySocial>(response);
        }

        public async Task<GenericResponse<StudySocial>> DeleteStudySocial(long id)
        {
            var response = await _client.DeleteAsync($"StudySocial?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudySocial>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudySocial>(response);
        }

        public async Task<GenericResponse<List<StudySocial>>> GetStudySocial(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($"StudySocial?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudySocial>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudySocial>>(response);
        }

        public async Task<GenericResponse<StudySocial>> UpdateStudySocial(StudySocial request)
        {
            var response = await _client.PutAsync($"StudySocial", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudySocial>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudySocial>(response);
        }



        // Social Goals
        public async Task<GenericResponse<List<SocialGoals>>> CreateSocialGoals(List<SocialGoals> request)
        {
            var response = await _client.PostAsync($"StudySocial/SocialGoals", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<SocialGoals>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<SocialGoals>>(response);
        }

        public async Task<GenericResponse<SocialGoals>> DeleteSocialGoals(long id)
        {
            var response = await _client.DeleteAsync($"StudySocial/SocialGoals?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<SocialGoals>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<SocialGoals>(response);
        }

        public async Task<GenericResponse<List<SocialGoals>>> GetSocialGoals(List<long> id)
        {
            string ids = string.Empty;
            id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });

            var response = await _client.GetAsync($"StudySocial/SocialGoals?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<SocialGoals>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<SocialGoals>>(response);
        }

        public async Task<GenericResponse<SocialGoals>> UpdateSocialGoals(SocialGoals request)
        {
            var response = await _client.PutAsync($"StudySocial/SocialGoals", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<SocialGoals>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<SocialGoals>(response);
        }
    }
}
