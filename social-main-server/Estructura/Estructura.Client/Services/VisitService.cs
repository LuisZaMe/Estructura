using Estructura.Client.Interfaces;
using Estructura.Client.Utilities;
using Estructura.Common.Enums;
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
    public class VisitService : IVisitService
    {
        private readonly HttpClient _client;
        public VisitService(HttpClient _client)
        {
            this._client=_client;
        }

        public async Task<GenericResponse<Visit>> CreateVisit(Visit request)
        {
            var response = await _client.PostAsync($@"Visit", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Visit>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Visit>(response);
        }

        public async Task<GenericResponse<List<Visit>>> GetVisit(List<long> Id, long? interviewerId, long? candidateId, long? clientId, long? studyId, DateTime? startDate, DateTime? endDate, int? cityId, int? stateId, VisitStatus visitStatus = VisitStatus.NONE, int currentPage = 0, int offset = 10)
        {
            string ids = string.Empty;
            Id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            string request = $@"Visit?{ids}interviewerId={interviewerId}&candidateId={candidateId}&clientId={clientId}&studyId={studyId}&startDate={startDate}&endDate={endDate}&cityId={cityId}&stateId={stateId}&visitStatus={visitStatus}&currentPage={currentPage}&offset={offset}";
            var response = await _client.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Visit>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Visit>>(response);
        }

        public async Task<GenericResponse<Visit>> UpdateVisit(Visit request)
        {
            var response = await _client.PutAsync($@"Visit", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Visit>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Visit>(response);
        }

        public async Task<GenericResponse<Visit>> DeleteVisit(long Id)
        {
            var response = await _client.DeleteAsync($@"Visit?Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Visit>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Visit>(response);
        }

        public async Task<GenericResponse<int>> Pagination(List<long> Id, long? interviewerId, long? candidateId, long? clientId, long? studyId, DateTime? startDate, DateTime? endDate, int? cityId, int? stateId, VisitStatus visitStatus = VisitStatus.NONE, int splitBy = 10)
        {
            string ids = string.Empty;
            Id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($@"Visit/Pagination?{ids}interviewerId={interviewerId}&candidateId={candidateId}&clientId={clientId}&studyId={studyId}&startDate={startDate}&endDate={endDate}&cityId={cityId}&stateId={stateId}&visitStatus={visitStatus}&splitBy={splitBy}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<int>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<int>(response);
        }
    }
}
