using Estructura.Common.Enums;
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
    public class StudyService: IStudyService
    {
        private readonly IStudyRepository _studyRepository;
        private readonly ICandidateService _candidateService;
        private readonly IStudyEconomicSituationService _studyEconomicSituationService;
        private readonly IStudyFamilyService _studyFamilyService;
        private readonly IStudyFinalResultService _studyFinalResultService;
        private readonly IStudyGeneralInformationService _studyGeneralInformationService;
        private readonly IStudySchoolarityService _studySchoolarityService;
        private readonly IStudySocialService _studySocialService;
        private readonly IStudyLaboralTrayectoryService _studyLaboralTrayectoryService;
        private readonly IStudyIMSSValidationService _studyIMSSValidationService;
        private readonly IStudyPersonalReferenceService _studyPersonalReferenceService;
        private readonly IStudyPicturesService _studyPicturesService;

        public StudyService(
            ICandidateService _candidateService, 
            IStudyRepository _studyRepository,
            IStudyEconomicSituationService _studyEconomicSituationService,
            IStudyFamilyService _studyFamilyService,
            IStudyFinalResultService _studyFinalResultService,
            IStudyGeneralInformationService _studyGeneralInformationService,
            IStudySchoolarityService _studySchoolarityService,
            IStudySocialService _studySocialService,
            IStudyLaboralTrayectoryService _studyLaboralTrayectoryService,
            IStudyIMSSValidationService _studyIMSSValidationService,
            IStudyPersonalReferenceService studyPersonalReferenceService,
            IStudyPicturesService _studyPicturesService)
        {
            this._studyRepository=_studyRepository;
            this._candidateService=_candidateService;
            this._studyEconomicSituationService=_studyEconomicSituationService;
            this._studyFamilyService=_studyFamilyService;
            this._studyFinalResultService=_studyFinalResultService;
            this._studyGeneralInformationService=_studyGeneralInformationService;
            this._studySchoolarityService=_studySchoolarityService;
            this._studySocialService=_studySocialService;
            this._studyLaboralTrayectoryService=_studyLaboralTrayectoryService;
            this._studyIMSSValidationService=_studyIMSSValidationService;
            this._studyPersonalReferenceService=studyPersonalReferenceService;
            this._studyPicturesService=_studyPicturesService;
        }

        public async Task<GenericResponse<FieldsToFill>> CreateFieldsToFill(FieldsToFill request)
        {
            return await _studyRepository.CreateFieldsToFill(request);
        }

        public async Task<GenericResponse<SocioeconomicStudy>> CreateSocuoeconomicStudy(SocioeconomicStudy request)
        {
            return await _studyRepository.CreateSocuoeconomicStudy(request);
        }

        public async Task<GenericResponse<Study>> CreateStudy(Study request)
        {
            var error = new GenericResponse<Study>()
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };

            if(request.Candidate == null || request.Candidate.Id == 0||
               //request.Interviewer == null || request.Interviewer.Id == 0||
               request.ServiceType == Common.Enums.ServiceTypes.NONE)
            {
                error.StatusCode = System.Net.HttpStatusCode.BadRequest;
                error.ErrorMessage = "Candidate, interviewer and ServiceType are required";
                return error;
            }
            request.StudyStatus = Common.Enums.StudyStatus.NOT_STARTED;

            if (request.ServiceType == Common.Enums.ServiceTypes.ESTUDIO_LABORAL)
                request.StudyProgressStatus = StudyProgressStatus.UNDER_ANALYST;
            else if (request.ServiceType == Common.Enums.ServiceTypes.ESTUDIO_SOCIOECONOMICO)
                request.StudyProgressStatus = StudyProgressStatus.UNDER_INTERVIEWER;

            var study = await _studyRepository.CreateStudy(request);

            if(!study.Sucess|| study.Response==null||study.Response.Id==0)
            {
                error.ErrorMessage = study.ErrorMessage;
                return error;
            }


            bool isSucces = true;
            if (request.ServiceType == Common.Enums.ServiceTypes.ESTUDIO_LABORAL)
            {
                request.WorkStudy.StudyId = study.Response.Id;
                var r = await CreateWorkStudy(request.WorkStudy);
                isSucces = r.Sucess;
            }
            else if(request.ServiceType == Common.Enums.ServiceTypes.ESTUDIO_SOCIOECONOMICO)
            {
                request.SocioeconomicStudy.StudyId = study.Response.Id;
                var r = await CreateSocuoeconomicStudy(request.SocioeconomicStudy);
                isSucces = r.Sucess;
            }

            if (isSucces)
            {
                request.FieldsToFill.StudyId = study.Response.Id;
                var r = await CreateFieldsToFill(request.FieldsToFill);
                isSucces = r.Sucess;
            }

            if (!isSucces)
            {
                await _studyRepository.DeleteStudy(study.Response.Id, true);
                error.ErrorMessage = "Unable to create study, verify information";
                return error;
            }

            var currentStudy = await GetStudy(
                new List<long>() { study.Response.Id },
                0,
                null,
                null,
                Common.Enums.ServiceTypes.NONE,
                0,
                Common.Enums.StudyStatus.NONE,
                0,
                0,
                0,
                0,
                10,
                false
            );
            return new GenericResponse<Study>()
            {
                Sucess = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Response = currentStudy.Response?.FirstOrDefault()
            };
        }

        public async Task<GenericResponse<WorkStudy>> CreateWorkStudy(WorkStudy request)
        {
            return await _studyRepository.CreateWorkStudy(request);
        }

        public async Task<GenericResponse<Study>> DeleteStudy(long Id, bool hardDelete = false)
        {
            return await _studyRepository.DeleteStudy(Id, hardDelete);
        }

        public async Task<GenericResponse<List<Study>>> GetStudy(
            List<long> Id,
            long AnalystId,
            DateTime? startDate,
            DateTime? endDate,
            Common.Enums.ServiceTypes serviceType = Common.Enums.ServiceTypes.NONE,
            long interviewerId = 0,
            Common.Enums.StudyStatus studyStatus = Common.Enums.StudyStatus.NONE,
            long clientId = 0,
            long candidateId = 0,
            StudyProgressStatus studyProgressStatus = StudyProgressStatus.NONE,
            int currentPage = 0,
            int offset = 10,
            bool bringStudiesOnlyOwn = true,
            int bringStudiesOnly = 0
        )
        {
            var studies= await _studyRepository.GetStudy(
                Id,
                AnalystId,
                startDate,
                endDate,
                serviceType,
                interviewerId,
                studyStatus,
                clientId,
                candidateId,
                studyProgressStatus,
                currentPage,
                offset,
                bringStudiesOnlyOwn,
                bringStudiesOnly
             );
            if (studies==null||!studies.Sucess||studies.Response.Count==0)
                return studies;

            List<long> candidateIds = new List<long>();
            studies.Response.ForEach(e => candidateIds.Add(e.Candidate.Id));
            var candidates = await _candidateService.GetCandidate(candidateIds, 0, int.MaxValue);

            var studiesList = new List<long>();
            studies.Response.ForEach(e=> studiesList.Add(e.Id));


            List<Task> tasks = new List<Task>();
            Task<GenericResponse<List<StudyEconomicSituation>>> studyEconomicServiceTask = _studyEconomicSituationService.GetStudyEconomicSituation(studiesList, true);
            Task<GenericResponse<List<StudyFamily>>> studyFamilyServiceTask = _studyFamilyService.GetStudyFamily(studiesList, true);
            Task<GenericResponse<List<StudyFinalResult>>> studyFinalResultTask = _studyFinalResultService.GetStudyFinalResult(studiesList, 0, 1000, true);
            Task<GenericResponse<List<StudyGeneralInformation>>> studyGeneralInformationTask = _studyGeneralInformationService.GetStudyGeneralInformation(studiesList, true);
            Task<GenericResponse<List<StudySchoolarity>>> studySchoolarityTask = _studySchoolarityService.GetStudySchoolarity(studiesList, true);
            Task<GenericResponse<List<StudySocial>>> studySocialServiceTask = _studySocialService.GetStudySocial(studiesList, true);
            Task<GenericResponse<List<StudyLaboralTrayectory>>> studyLaboralTrayectoryTask = _studyLaboralTrayectoryService.GetStudyLaboralTrayectory(studiesList, true);
            Task<GenericResponse<List<StudyIMSSValidation>>> studyIMSSValidationTask = _studyIMSSValidationService.GetStudyIMSSValidation(studiesList, true);
            Task<GenericResponse<List<StudyPersonalReference>>> studyPersonalReferenceTask = _studyPersonalReferenceService.GetStudyPersonalReference(studiesList, true);
            Task<GenericResponse<List<StudyPictures>>> studyPicturesTask = _studyPicturesService.GetStudyPictures(studiesList, true);


            tasks.Add(studyEconomicServiceTask);
            tasks.Add(studyFamilyServiceTask);
            tasks.Add(studyFinalResultTask);
            tasks.Add(studyGeneralInformationTask);
            tasks.Add(studySchoolarityTask);
            tasks.Add(studySocialServiceTask);
            tasks.Add(studyLaboralTrayectoryTask);
            tasks.Add(studyIMSSValidationTask);
            tasks.Add(studyPersonalReferenceTask);
            tasks.Add(studyPicturesTask);


            /*while(tasks.Count > 0)
            {
                var current = await Task.WhenAny(tasks);
                if (current.IsFaulted)
                {
                    return new GenericResponse<List<Study>>()
                    {
                        ErrorMessage = "Error on Task"+ current.Exception.Message,
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }

                if(current == studyEconomicServiceTask)
                    studies.Response.ForEach(s => s.StudyEconomicSituation = studyEconomicServiceTask.Result.Response.FirstOrDefault(t => t.StudyId==s.Id));
                else if(current == studyFamilyServiceTask)
                    studies.Response.ForEach(s => s.StudyFamily = studyFamilyServiceTask.Result.Response.FirstOrDefault(t => t.StudyId==s.Id));
                else if(current == studyFinalResultTask)
                    studies.Response.ForEach(s => s.StudyFinalResult = studyFinalResultTask.Result.Response.FirstOrDefault(t => t.StudyId==s.Id));
                else if(current == studyGeneralInformationTask)
                    studies.Response.ForEach(s => s.StudyGeneralInformation = studyGeneralInformationTask.Result.Response.FirstOrDefault(t => t.StudyId==s.Id));
                else if(current == studySchoolarityTask)
                    studies.Response.ForEach(s => s.StudySchoolarity = studySchoolarityTask.Result.Response.FirstOrDefault(t => t.StudyId==s.Id));
                else if(current == studySocialServiceTask)
                    studies.Response.ForEach(s => s.StudySocial = studySocialServiceTask.Result.Response.FirstOrDefault(t => t.StudyId==s.Id));
                else if(current == studyLaboralTrayectoryTask)
                    studies.Response.ForEach(s => s.StudyLaboralTrayectoryList = studyLaboralTrayectoryTask.Result.Response.Where(t => t.StudyId==s.Id)?.ToList());
                else if(current == studyIMSSValidationTask)
                    studies.Response.ForEach(s => s.StudyIMSSValidation = studyIMSSValidationTask.Result.Response.FirstOrDefault(t => t.StudyId==s.Id));
                else if(current == studyPersonalReferenceTask)
                    studies.Response.ForEach(s => s.StudyPersonalReferenceList = studyPersonalReferenceTask.Result.Response.Where(t => t.StudyId==s.Id)?.ToList());
                else if (current == studyPicturesTask)
                    studies.Response.ForEach(s => s.StudyPictures = studyPicturesTask.Result.Response.FirstOrDefault(t => t.StudyId==s.Id));

                tasks.Remove(current);
            }*/

            if (candidates!=null&&candidates.Sucess&&candidates.Response?.Count!=null)
                studies.Response.ForEach(e =>
                {
                    var CAN = candidates.Response.FirstOrDefault(f => e.Candidate.Id == f.Id);
                    e.Candidate = CAN;
                });
            return studies;
        }

        public async Task<GenericResponse<int>> Pagination(List<long> Id, DateTime? startDate, DateTime? endDate, ServiceTypes serviceType = ServiceTypes.NONE, long interviewerId = 0, StudyStatus studyStatus = StudyStatus.NONE, long clientId = 0, long candidateId = 0, StudyProgressStatus studyProgressStatus = StudyProgressStatus.NONE, int splitBy = 10)
        {
            return await _studyRepository.Pagination(Id, startDate, endDate, serviceType, interviewerId, studyStatus, clientId, candidateId, studyProgressStatus, splitBy);
        }

        public async Task<GenericResponse<Study>> UpdateStudy(Study request)
        {
            var study = await GetStudy(new List<long>() { request.Id }, 0, null, null,  Common.Enums.ServiceTypes.NONE, 0,  Common.Enums.StudyStatus.NONE, 0, 0, 0, 0, 10);
            if (study==null||!study.Sucess || study.Response.Count==0)
            {
                return new GenericResponse<Study>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ErrorMessage = "Study not found" };
            }

            request.StudyStatus = request.StudyStatus == Common.Enums.StudyStatus.NONE ? study.Response.FirstOrDefault().StudyStatus : request.StudyStatus;
            request.Analyst= request.Analyst!=null&&request.Analyst.Id>0 ? request.Analyst : study.Response.FirstOrDefault().Analyst;
            request.StudyProgressStatus = request.StudyProgressStatus != StudyProgressStatus.NONE? request.StudyProgressStatus : study.Response.FirstOrDefault().StudyProgressStatus;

            var updated = await _studyRepository.UpdateStudy(request);
            if (updated==null||!updated.Sucess || updated.Response==null)
            { 
                return new GenericResponse<Study>() { 
                    StatusCode = System.Net.HttpStatusCode.BadRequest, 
                    ErrorMessage = updated!=null&&!string.IsNullOrWhiteSpace(updated.ErrorMessage)? updated.ErrorMessage: "Error updating study"
                };
            }
            var result = await GetStudy(new List<long>() { updated.Response.Id }, 0, null, null, Common.Enums.ServiceTypes.NONE, 0, Common.Enums.StudyStatus.NONE, 0, 0, StudyProgressStatus.NONE, 0, 10);
            return new GenericResponse<Study>()
            {
                StatusCode = result.StatusCode,
                ErrorMessage = result.ErrorMessage,
                Sucess = result.Sucess,
                Response = result.Response?.FirstOrDefault()
            };
        }


        public async Task<GenericResponse<StudyNote>> DeleteNote(long Id)
        {
            return await _studyRepository.DeleteNote(Id);
        }

        public async Task<GenericResponse<List<StudyNote>>> GetNotes(List<long> Id, string key = "", long studyId = 0, int currentPage = 0, int offset = 10)
        {
            return await _studyRepository.GetNotes(Id, key, studyId, currentPage, offset);
        }

        public async Task<GenericResponse<StudyNote>> CreateNote(StudyNote request)
        {
            return await _studyRepository.CreateNote(request);
        }

        public async Task<GenericResponse<StudyNote>> UpdateNote(StudyNote request)
        {
            return await _studyRepository.UpdateNote(request);
        }
    }
}
