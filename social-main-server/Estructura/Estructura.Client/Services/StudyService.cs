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
    public class StudyService : IStudyService
    {
        private readonly HttpClient _client;
        public StudyService(HttpClient _client)
        {
            this._client = _client;
        }

        public async Task<GenericResponse<Study>> CreateStudy(Study request)
        {
            var response = await _client.PostAsync("Study", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Study>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Study>(response);
        }

        public async Task<GenericResponse<Study>> DeleteStudy(long Id)
        {
            var response = await _client.DeleteAsync($@"Study?Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Study>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Study>(response);
        }

        public async Task<GenericResponse<List<Study>>> GetStudy(List<long> Id, long AnalystId, DateTime? startDate, DateTime? endDate, Common.Enums.ServiceTypes serviceType = Common.Enums.ServiceTypes.NONE, long interviewerId = 0, Common.Enums.StudyStatus studyStatus = Common.Enums.StudyStatus.NONE, long clientId = 0, long candidateId = 0, StudyProgressStatus studyProgressStatus = StudyProgressStatus.NONE, int currentPage = 0, int offset = 10)
        {
            string ids = string.Empty;
            Id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            Console.WriteLine($@"Study?{ids}startDate={startDate}&endDate={endDate}&serviceType={serviceType}&interviewerId={interviewerId}&studyStatus={studyStatus}&clientId={clientId}&candidateId={candidateId}&studyProgressStatus={studyProgressStatus}&currentPage={currentPage}&offset={offset}");
            var response = await _client.GetAsync($@"Study?{ids}startDate={startDate}&endDate={endDate}&serviceType={serviceType}&interviewerId={interviewerId}&studyStatus={studyStatus}&clientId={clientId}&candidateId={candidateId}&studyProgressStatus={studyProgressStatus}&currentPage={currentPage}&offset={offset}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<Study>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<Study>>(response);
        }

        public async Task<GenericResponse<int>> Pagination(List<long> Id, DateTime? startDate, DateTime? endDate, ServiceTypes serviceType = ServiceTypes.NONE, long interviewerId = 0, StudyStatus studyStatus = StudyStatus.NONE, long clientId = 0, long candidateId = 0, StudyProgressStatus studyProgressStatus = StudyProgressStatus.NONE, int splitBy = 10)
        {
            string ids = string.Empty;
            Id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";

            var response = await _client.GetAsync($@"Study/Pagination?{ids}startDate={startDate}&endDate={endDate}&serviceType={serviceType}&interviewerId={interviewerId}&studyStatus={studyStatus}&clientId={clientId}&candidateId={candidateId}&studyProgressStatus={studyProgressStatus}&splitBy={splitBy}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<int>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<int>(response);
        }

        public async Task<GenericResponse<Study>> UpdateStudy(Study request)
        {
            var response = await _client.PutAsync("Study", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<Study>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<Study>(response);
        }



        public async Task<GenericResponse<List<StudyNote>>> GetNotes(List<long> Id, string key = "", long studyId = 0, int currentPage = 0, int offset = 10)
        {
            string ids = string.Empty;
            Id?.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(ids)) ids+="&";
                ids+=$@"Id={e}";
            });
            if (!string.IsNullOrWhiteSpace(ids))
                ids+="&";
            var response = await _client.GetAsync($@"Study/Note?{ids}key={key}&studyId={studyId}&currentPage={currentPage}&offset={offset}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<List<StudyNote>>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<List<StudyNote>>(response);
        }

        public async Task<GenericResponse<StudyNote>> UpdateNote(StudyNote request)
        {
            var response = await _client.PutAsync("Study/Note", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyNote>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyNote>(response);
        }

        public async Task<GenericResponse<StudyNote>> DeleteNote(long Id)
        {
            var response = await _client.DeleteAsync($@"Study/Note?Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyNote>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyNote>(response);
        }

        public async Task<GenericResponse<StudyNote>> CreateNote(StudyNote request)
        {
            var response = await _client.PostAsync("Study/Note", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsObjectAsync<GenericResponse<StudyNote>>();
                return result;
            }
            return await HttpErrorParser.ErrorParser<StudyNote>(response);
        }
    }
}
