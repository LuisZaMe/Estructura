using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Estructura.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.BLL.Services
{
    public class StudyFinalResultService : IStudyFinalResultService
    {
        private readonly IStudyFinalResultRepository _studyFinalResultRepository;
        private readonly IStudyRepository _studyRepository;
        public StudyFinalResultService(IStudyFinalResultRepository _studyFinalResultRepository, IStudyRepository _studyRepository)
        {
            this._studyFinalResultRepository = _studyFinalResultRepository;
            this._studyRepository = _studyRepository;
        }
        public async Task<GenericResponse<StudyFinalResult>> CreateStudyFinalResult(StudyFinalResult request)
        {
            var currentStudy = await _studyRepository.GetStudy(new List<long>() { request.StudyId }, 0, null, null);
            if (currentStudy == null || !currentStudy.Sucess || currentStudy.Response.Count==0)
                return new GenericResponse<StudyFinalResult>()
                {
                    ErrorMessage = "Study not found"
                };

            var currentStudyFinalResult = await GetStudyFinalResult(new List<long>() { request.StudyId }, 0, 10);
            if(currentStudyFinalResult !=null && currentStudyFinalResult.Sucess && currentStudyFinalResult.Response.Count>0) 
                return new GenericResponse<StudyFinalResult>()
                {
                    ErrorMessage = "Study final result already exist"
                };

            return await _studyFinalResultRepository.CreateStudyFinalResult(request);
        }

        public async Task<GenericResponse<StudyFinalResult>> DeleteStudyFinalResult(long id)
        {
            return await _studyFinalResultRepository.DeleteStudyFinalResult(id);
        }

        public async Task<GenericResponse<List<StudyFinalResult>>> GetStudyFinalResult(List<long> id, int currentPage, int offset, bool byStudy = false)
        {
            return await _studyFinalResultRepository.GetStudyFinalResult(id, currentPage, offset, byStudy);
        }

        public async Task<GenericResponse<StudyFinalResult>> UpdateStudyFinalResult(StudyFinalResult request)
        {
            var currentStudyFinalResultList = await _studyFinalResultRepository.GetStudyFinalResult(new List<long>() { request.Id }, 0, 10);
            if (currentStudyFinalResultList == null || !currentStudyFinalResultList.Sucess || currentStudyFinalResultList.Response.Count==0)
                return new GenericResponse<StudyFinalResult>()
                {
                    ErrorMessage = "CurrentStudyFinalResult not found"
                };

            var currentStudy = currentStudyFinalResultList.Response.FirstOrDefault();
            request.PositionSummary = string.IsNullOrWhiteSpace(request.PositionSummary) ? currentStudy.PositionSummary : request.PositionSummary;
            request.AttitudeSummary = string.IsNullOrWhiteSpace(request.AttitudeSummary) ? currentStudy.AttitudeSummary : request.AttitudeSummary;
            request.WorkHistorySummary = string.IsNullOrWhiteSpace(request.WorkHistorySummary) ? currentStudy.WorkHistorySummary : request.WorkHistorySummary;
            request.ArbitrationAndConciliationSummary = string.IsNullOrWhiteSpace(request.ArbitrationAndConciliationSummary) ? currentStudy.ArbitrationAndConciliationSummary : request.ArbitrationAndConciliationSummary;
            request.FinalResultsBy = string.IsNullOrWhiteSpace(request.FinalResultsBy) ? currentStudy.FinalResultsBy : request.FinalResultsBy;
            request.FinalResultsPositionBy = string.IsNullOrWhiteSpace(request.FinalResultsPositionBy) ? currentStudy.FinalResultsPositionBy : request.FinalResultsPositionBy;

            return await _studyFinalResultRepository.UpdateStudyFinalResult(request);
        }
    }
}
