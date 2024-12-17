using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Services
{
    public interface ICandidateService
    {
        Task<GenericResponse<Candidate>> CreateCandidate(Candidate request);
        Task<GenericResponse<List<Candidate>>> GetCandidate(List<long> Id, int currentPage, int offset);
        Task<GenericResponse<Candidate>> UpdateCandidate(Candidate request);
        Task<GenericResponse<Candidate>> DeleteCandidate(long Id);
        Task<GenericResponse<int>> Pagination(string key = "", long clientId = 0, int splitBy = 10);
        Task<GenericResponse<List<Candidate>>> SearchCandidate(string key, long clientId = 0, int currentPage = 0, int offset = 10);

        //Notes
        Task<GenericResponse<CandidateNote>> CreateNote(CandidateNote request);
        Task<GenericResponse<CandidateNote>> UpdateNote(CandidateNote request);
        Task<GenericResponse<CandidateNote>> DeleteNote(long Id);
        Task<GenericResponse<List<CandidateNote>>> GetNotes(List<long> Id, string key = "", long candidateId = 0, int currentPage = 0, int offset = 10);
    }
}
