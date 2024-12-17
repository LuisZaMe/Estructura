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
    public class CandidateService : ICandidateService
    {
        private readonly HttpClient _client;
        public CandidateService(HttpClient _client)
        {
            this._client=_client;
        }
        public async Task<GenericResponse<Candidate>> CreateCandidate(Candidate request)
        {
            var response = await _client.PostAsync("Candidate", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Candidate>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Candidate>(response);
        }


        public async Task<GenericResponse<Candidate>> DeleteCandidate(long Id)
        {
            var response = await _client.DeleteAsync($@"Candidate?Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Candidate>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Candidate>(response);
        }


        public async Task<GenericResponse<int>> Pagination(string key = "", long clientId = 0, int splitBy = 10)
        {
            var response = await _client.GetAsync($@"Candidate/Pagination?key={key}&clientId={clientId}&splitBy={splitBy}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<int>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<int>(response);
        }

        public async Task<GenericResponse<List<Candidate>>> SearchCandidate(string key, long clientId = 0, int currentPage = 0, int offset = 10)
        {
            var response = await _client.GetAsync($@"Candidate/Search?key={key}&clientId={clientId}&currentPage={currentPage}&offset={offset}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Candidate>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Candidate>>(response);
        }

        public async Task<GenericResponse<Candidate>> UpdateCandidate(Candidate request)
        {
            var response = await _client.PutAsync("Candidate", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Candidate>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Candidate>(response);
        }


        public async Task<GenericResponse<List<Candidate>>> GetCandidate(List<long> Id, int currentPage, int offset)
        {
            string ids = string.Empty;
            Id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($@"Candidate?{ids}currentPage={currentPage}&offset={offset}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Candidate>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Candidate>>(response);
        }


        //Note
        public async Task<GenericResponse<List<CandidateNote>>> GetNotes(List<long> Id, string key = "", long candidateId = 0, int currentPage = 0, int offset = 10)
        {

            string ids = string.Empty;
            Id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($@"Candidate/Note?{ids}key={key}&candidateId={candidateId}&currentPage={currentPage}&offset={offset}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<CandidateNote>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<CandidateNote>>(response);
        }

        public async Task<GenericResponse<CandidateNote>> UpdateNote(CandidateNote request)
        {
            var response = await _client.PutAsync("Candidate/Note", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<CandidateNote>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<CandidateNote>(response);
        }

        public async Task<GenericResponse<CandidateNote>> DeleteNote(long Id)
        {
            var response = await _client.DeleteAsync($@"Candidate/Note?Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<CandidateNote>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<CandidateNote>(response);
        }

        public async Task<GenericResponse<CandidateNote>> CreateNote(CandidateNote request)
        {
            var response = await _client.PostAsync("Candidate/Note", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<CandidateNote>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<CandidateNote>(response);
        }
    }
}
