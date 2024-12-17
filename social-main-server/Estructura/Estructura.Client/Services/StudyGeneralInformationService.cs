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
    public class StudyGeneralInformationService : IStudyGeneralInformationService
    {
        HttpClient _client;
        public StudyGeneralInformationService(HttpClient _client)
        {
            this._client=_client;
        }


        // StudyGeneralInformation
        public async Task<GenericResponse<StudyGeneralInformation>> CreateStudyGeneralInformation(StudyGeneralInformation request)
        {
            var response = await _client.PostAsync("StudyGeneralInformation", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyGeneralInformation>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyGeneralInformation>(response);
        }

        public async Task<GenericResponse<StudyGeneralInformation>> DeleteStudyGeneralInformation(long id)
        {
            var response = await _client.DeleteAsync($@"StudyGeneralInformation?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyGeneralInformation>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyGeneralInformation>(response);
        }

        public async Task<GenericResponse<List<StudyGeneralInformation>>> GetStudyGeneralInformation(List<long> id)
        {
            string ids = string.Empty;
            id.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"id={e}";
            });
            var response = await _client.GetAsync($@"StudyGeneralInformation?{ids}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudyGeneralInformation>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudyGeneralInformation>>(response);
        }

        public async Task<GenericResponse<StudyGeneralInformation>> UpdateStudyGeneralInformation(StudyGeneralInformation request)
        {
            var response = await _client.PutAsync("StudyGeneralInformation", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyGeneralInformation>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyGeneralInformation>(response);
        }


        // Recommendation card
        public async Task<GenericResponse<List<RecommendationCard>>> CreateRecommendationCard(List<RecommendationCard> request)
        {
            var response = await _client.PostAsync("StudyGeneralInformation/RecommendationCard", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<RecommendationCard>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<RecommendationCard>>(response);
        }

        public async Task<GenericResponse<RecommendationCard>> UpdateRecommendationCard(RecommendationCard request)
        {
            var response = await _client.PutAsync("StudyGeneralInformation/RecommendationCard", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<RecommendationCard>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<RecommendationCard>(response);
        }

        public async Task<GenericResponse<List<RecommendationCard>>> GetRecommendationCard(List<long> id)
        {
            string request = string.Empty;
            id.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(request)) request+="&";
                request+=$@"id={e}";
            });

            var response = await _client.GetAsync($@"StudyGeneralInformation/RecommendationCard?{request}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<RecommendationCard>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<RecommendationCard>>(response);
        }

        public async Task<GenericResponse<RecommendationCard>> DeleteRecommendationCard(long id)
        {
            var response = await _client.DeleteAsync($@"StudyGeneralInformation/RecommendationCard?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<RecommendationCard>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<RecommendationCard>(response);
        }
    }
}
