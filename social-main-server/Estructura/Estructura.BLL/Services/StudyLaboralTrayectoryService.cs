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
    public class StudyLaboralTrayectoryService : IStudyLaboralTrayectoryService
    {
        private readonly IStudyLaboralTrayectoryRepository _studyLaboralTrayectoryRepository;
        private readonly IMediaService _mediaService;
        public StudyLaboralTrayectoryService(IMediaService _mediaService, IStudyLaboralTrayectoryRepository _studyLaboralTrayectoryRepository)
        {
            this._studyLaboralTrayectoryRepository=_studyLaboralTrayectoryRepository;
            this._mediaService=_mediaService;
        }
        public async Task<GenericResponse<List<StudyLaboralTrayectory>>> CreateStudyLaboralTrayectory(List<StudyLaboralTrayectory> request)
        {
            if (request.Any(e => e.StudyId ==0))
                return new GenericResponse<List<StudyLaboralTrayectory>>()
                {
                    ErrorMessage = "All items require StudyId",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var mediaTaskList = new List<Task<GenericResponse<Media>>>();
            request.ForEach(e =>
            {
                if (e.Media1!=null&&!string.IsNullOrWhiteSpace(e.Media1.Base64Image))
                {
                    e.Media1.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                    var media1 = _mediaService.CreateMedia(e.Media1);
                    e.Media1.Id = media1.Id;
                    mediaTaskList.Add(media1);
                }
                if (e.Media2!=null&&!string.IsNullOrWhiteSpace(e.Media2.Base64Image))
                {
                    e.Media2.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                    var media2 = _mediaService.CreateMedia(e.Media2);
                    e.Media2.Id = media2.Id;
                    mediaTaskList.Add(media2);
                }
            });

            while(mediaTaskList.Count > 0)
            {
                var current = await Task.WhenAny(mediaTaskList);
                if (current.IsFaulted)
                {
                    return new GenericResponse<List<StudyLaboralTrayectory>>()
                    {
                        ErrorMessage = "Unable to create media images, verify information",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }
                request.ForEach(e =>
                {
                    if (e.Media1 !=null && e.Media1.Id == current.Id)
                        e.Media1 = current.Result.Response;
                    else if (e.Media2 !=null &&e.Media2.Id == current.Id)
                        e.Media2 = current.Result.Response;
                });
                mediaTaskList.Remove(current);
            }

            var response = await _studyLaboralTrayectoryRepository.CreateStudyLaboralTrayectory(request);
            if (response.Sucess)
            {
                var listIds = new List<long>();
                response.Response.ForEach(e => listIds.Add(e.Id));
                return await GetStudyLaboralTrayectory(listIds);
            }
            return response;
        }

        public async Task<GenericResponse<StudyLaboralTrayectory>> DeleteStudyLaboralTrayectory(long id)
        {
            return await _studyLaboralTrayectoryRepository.DeleteStudyLaboralTrayectory(id);
        }

        public async Task<GenericResponse<List<StudyLaboralTrayectory>>> GetStudyLaboralTrayectory(List<long> id, bool byStudy=false)
        {
            var response = await _studyLaboralTrayectoryRepository.GetStudyLaboralTrayectory(id, byStudy);
            if (!response.Sucess)
                return response;

            List<long> mediaIds = new List<long>();
            response.Response.ForEach(e =>
            {
                mediaIds.Add(e.Media1.Id);
                mediaIds.Add(e.Media2.Id);
            });

            var mediaItems = await _mediaService.GetMedia(mediaIds);
            if (!mediaItems.Sucess)
                return response;

            response.Response.ForEach(e =>
            {
                e.Media1 = mediaItems.Response.FirstOrDefault(f => f.Id == e.Media1.Id);
                e.Media2 = mediaItems.Response.FirstOrDefault(f => f.Id == e.Media2.Id);
            });
            return response;
        }

        public async Task<GenericResponse<StudyLaboralTrayectory>> UpdateStudyLaboralTrayectory(StudyLaboralTrayectory request)
        {
            var currentList = await GetStudyLaboralTrayectory(new List<long>() { request.Id });
            if (!currentList.Sucess)
                return new GenericResponse<StudyLaboralTrayectory>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            var current = currentList.Response.FirstOrDefault();


            if (request.Media1 !=null && !string.IsNullOrWhiteSpace(request.Media1.Base64Image) && request.Media1.Id==0)
            {
                request.Media1.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                var image = await _mediaService.CreateMedia(request.Media1);
                if (!image.Sucess)
                    return new GenericResponse<StudyLaboralTrayectory>()
                    {
                        ErrorMessage = "Error updating media1 source, verify data",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                request.Media1 = image.Response;
            }
            if (request.Media2 != null && !string.IsNullOrWhiteSpace(request.Media2.Base64Image) && request.Media2.Id==0)
            {
                request.Media2.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                var image = await _mediaService.CreateMedia(request.Media2);
                if (!image.Sucess)
                    return new GenericResponse<StudyLaboralTrayectory>()
                    {
                        ErrorMessage = "Error updating media2 source, verify data",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                request.Media2 = image.Response;
            }


            request.TrayectoryName  = String.IsNullOrWhiteSpace(request.TrayectoryName) ? current.TrayectoryName : request.TrayectoryName;
            request.CompanyName  = String.IsNullOrWhiteSpace(request.CompanyName) ? current.CompanyName : request.CompanyName;
            request.CandidateBusinessName  = String.IsNullOrWhiteSpace(request.CandidateBusinessName) ? current.CandidateBusinessName : request.CandidateBusinessName;
            request.CompanyBusinessName  = String.IsNullOrWhiteSpace(request.CompanyBusinessName) ? current.CompanyBusinessName : request.CompanyBusinessName;
            request.CandidateRole  = String.IsNullOrWhiteSpace(request.CandidateRole) ? current.CandidateRole : request.CandidateRole;
            request.CompanyRole  = String.IsNullOrWhiteSpace(request.CompanyRole) ? current.CompanyRole : request.CompanyRole;
            request.CandidatePhone  = String.IsNullOrWhiteSpace(request.CandidatePhone) ? current.CandidatePhone : request.CandidatePhone;
            request.CompanyPhone  = String.IsNullOrWhiteSpace(request.CompanyPhone) ? current.CompanyPhone : request.CompanyPhone;
            request.CandidateAddress  = String.IsNullOrWhiteSpace(request.CandidateAddress) ? current.CandidateAddress : request.CandidateAddress;
            request.CompanyAddress  = String.IsNullOrWhiteSpace(request.CompanyAddress) ? current.CompanyAddress : request.CompanyAddress;
            request.CandidateStartDate  = request.CandidateStartDate == DateTime.MinValue ? current.CandidateStartDate : request.CandidateStartDate;
            request.CompanyStartDate = request.CompanyStartDate == DateTime.MinValue ? current.CompanyStartDate : request.CompanyStartDate;
            request.CandidateEndDate  = request.CandidateEndDate == DateTime.MinValue ? current.CandidateEndDate : request.CandidateEndDate;
            request.CompanyEndDate  = request.CompanyEndDate == DateTime.MinValue ? current.CompanyEndDate : request.CompanyEndDate;
            request.CandidateInitialRole  = String.IsNullOrWhiteSpace(request.CandidateInitialRole) ? current.CandidateInitialRole : request.CandidateInitialRole;
            request.CompanyInitialRole  = String.IsNullOrWhiteSpace(request.CompanyInitialRole) ? current.CompanyInitialRole : request.CompanyInitialRole;
            request.CandidateFinalRole  = String.IsNullOrWhiteSpace(request.CandidateFinalRole) ? current.CandidateFinalRole : request.CandidateFinalRole;
            request.CompanyFinalRole  = String.IsNullOrWhiteSpace(request.CompanyFinalRole) ? current.CompanyFinalRole : request.CompanyFinalRole;
            request.CandidateStartSalary  = request.CandidateStartSalary == 0 ? current.CandidateStartSalary : request.CandidateStartSalary;
            request.CompanyStartSalary  = request.CompanyStartSalary == 0 ? current.CompanyStartSalary : request.CompanyStartSalary;
            request.CandidateEndSalary  = request.CandidateEndSalary == 0 ? current.CandidateEndSalary : request.CandidateEndSalary;
            request.CompanyEndSalary  = request.CompanyEndSalary == 0 ? current.CompanyEndSalary : request.CompanyEndSalary;
            request.CandidateBenefits  = String.IsNullOrWhiteSpace(request.CandidateBenefits) ? current.CandidateBenefits : request.CandidateBenefits;
            request.CompanyBenefits  = String.IsNullOrWhiteSpace(request.CompanyBenefits) ? current.CompanyBenefits : request.CompanyBenefits;
            request.CandidateResignationReason  = String.IsNullOrWhiteSpace(request.CandidateResignationReason) ? current.CandidateResignationReason : request.CandidateResignationReason;
            request.CompanyResignationReason  = String.IsNullOrWhiteSpace(request.CompanyResignationReason) ? current.CompanyResignationReason : request.CompanyResignationReason;
            request.CandidateDirectBoss  = String.IsNullOrWhiteSpace(request.CandidateDirectBoss) ? current.CandidateDirectBoss : request.CandidateDirectBoss;
            request.CompanyDirectBoss  = String.IsNullOrWhiteSpace(request.CompanyDirectBoss) ? current.CompanyDirectBoss : request.CompanyDirectBoss;
            request.CandidateLaborUnion  = String.IsNullOrWhiteSpace(request.CandidateLaborUnion) ? current.CandidateLaborUnion : request.CandidateLaborUnion;
            request.CompanyLaborUnion  = String.IsNullOrWhiteSpace(request.CompanyLaborUnion) ? current.CompanyLaborUnion : request.CompanyLaborUnion;
            request.Recommended  = String.IsNullOrWhiteSpace(request.Recommended) ? current.Recommended : request.Recommended;
            request.RecommendedReasonWhy  = String.IsNullOrWhiteSpace(request.RecommendedReasonWhy) ? current.RecommendedReasonWhy : request.RecommendedReasonWhy;
            request.Rehire  = String.IsNullOrWhiteSpace(request.Rehire) ? current.Rehire : request.Rehire;
            request.RehireReason  = String.IsNullOrWhiteSpace(request.RehireReason) ? current.RehireReason : request.RehireReason;
            request.Observations  = String.IsNullOrWhiteSpace(request.Observations) ? current.Observations : request.Observations;
            request.Notes  = String.IsNullOrWhiteSpace(request.Notes) ? current.Notes : request.Notes;
            request.Media1  = request.Media1 == null || request.Media1.Id == 0 ? current.Media1 : request.Media1;
            request.Media2  = request.Media2 == null || request.Media2.Id == 0 ? current.Media2 : request.Media2;

            var response = await _studyLaboralTrayectoryRepository.UpdateStudyLaboralTrayectory(request);
            if (response.Sucess)
            {
                var responseList = await GetStudyLaboralTrayectory(new List<long>() { request.Id });
                response.Response = responseList.Response?.FirstOrDefault();
            }
            return response;
        }
    }
}
