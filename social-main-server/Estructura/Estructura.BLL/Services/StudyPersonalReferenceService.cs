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
    public class StudyPersonalReferenceService: IStudyPersonalReferenceService
    {
        private readonly IStudyPersonalReferenceRepository _studyPersonalReferenceRepository;
        public StudyPersonalReferenceService(IStudyPersonalReferenceRepository _studyPersonalReferenceRepository)
        {
            this._studyPersonalReferenceRepository=_studyPersonalReferenceRepository;
        }

        public async Task<GenericResponse<List<StudyPersonalReference>>> CreateStudyPersonalReference(List<StudyPersonalReference> request)
        {
            if (request.Any(e => e.StudyId == 0))
                return new GenericResponse<List<StudyPersonalReference>>()
                {
                    ErrorMessage = "All items require a StudyId",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            return await _studyPersonalReferenceRepository.CreateStudyPersonalReference(request);
        }

        public async Task<GenericResponse<StudyPersonalReference>> DeleteStudyPersonalReference(long id)
        {
            return await _studyPersonalReferenceRepository.DeleteStudyPersonalReference(id);
        }

        public async Task<GenericResponse<List<StudyPersonalReference>>> GetStudyPersonalReference(List<long> id, bool byStudy = false)
        {
            return await _studyPersonalReferenceRepository.GetStudyPersonalReference(id, byStudy);
        }

        public async Task<GenericResponse<StudyPersonalReference>> UpdateStudyPersonalReference(StudyPersonalReference request)
        {
            var currentList = await GetStudyPersonalReference(new List<long>() { request.Id });
            if (!currentList.Sucess)
                return new GenericResponse<StudyPersonalReference>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = "Item not found"
                };

            var current = currentList.Response.FirstOrDefault();

            request.ReferenceTitle = String.IsNullOrWhiteSpace(request.ReferenceTitle) ? current.ReferenceTitle : request.ReferenceTitle;
            request.Name = String.IsNullOrWhiteSpace(request.Name) ? current.Name : request.Name;
            request.CurrentJob = String.IsNullOrWhiteSpace(request.CurrentJob) ? current.CurrentJob : request.CurrentJob;
            request.Address = String.IsNullOrWhiteSpace(request.Address) ? current.Address : request.Address;
            request.Phone = String.IsNullOrWhiteSpace(request.Phone) ? current.Phone : request.Phone;
            request.YearsKnowingEachOther = String.IsNullOrWhiteSpace(request.YearsKnowingEachOther) ? current.YearsKnowingEachOther : request.YearsKnowingEachOther;
            request.KnowAddress = String.IsNullOrWhiteSpace(request.KnowAddress) ? current.KnowAddress : request.KnowAddress;
            request.YearsOnCurrentResidence = String.IsNullOrWhiteSpace(request.YearsOnCurrentResidence) ? current.YearsOnCurrentResidence : request.YearsOnCurrentResidence;
            request.KnowledgeAboutPreviousJobs = String.IsNullOrWhiteSpace(request.KnowledgeAboutPreviousJobs) ? current.KnowledgeAboutPreviousJobs : request.KnowledgeAboutPreviousJobs;
            request.OpinionAboutTheCandidate = String.IsNullOrWhiteSpace(request.OpinionAboutTheCandidate) ? current.OpinionAboutTheCandidate : request.OpinionAboutTheCandidate;
            request.PoliticalActivity = String.IsNullOrWhiteSpace(request.PoliticalActivity) ? current.PoliticalActivity : request.PoliticalActivity;
            request.WouldYouRecommendIt = String.IsNullOrWhiteSpace(request.WouldYouRecommendIt) ? current.WouldYouRecommendIt : request.WouldYouRecommendIt;

            return await _studyPersonalReferenceRepository.UpdateStudyPersonalReference(request);

        }
    }
}
