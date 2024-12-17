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
    public class StudySchoolarityService : IStudySchoolarityService
    {
        private readonly IStudySchoolarityRepository _studySchoolarityRepository;
        private readonly IMediaService _mediaService;
        private readonly IFileService _fileService;

        public StudySchoolarityService(IFileService _fileService, IMediaService _mediaService, IStudySchoolarityRepository _studySchoolarityRepository)
        {
            this._studySchoolarityRepository=_studySchoolarityRepository;
            this._mediaService=_mediaService;
            this._fileService=_fileService;
        }

        //Study schoolarity
        public async Task<GenericResponse<StudySchoolarity>> CreateStudySchoolarity(StudySchoolarity request)
        {
            string requirementsError = string.Empty;
            string globalError = string.Empty;

            if (request.StudyId == 0)
                requirementsError+="StudyId required";

            if (!string.IsNullOrWhiteSpace(requirementsError))
                return new GenericResponse<StudySchoolarity>()
                {
                    ErrorMessage = requirementsError,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            //Create study scholarity media
            if (request.ScholarVerificationMedia!=null&&!string.IsNullOrWhiteSpace(request.ScholarVerificationMedia.Base64Image))
            {
                request.ScholarVerificationMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                var mediaResponse = await _mediaService.CreateMedia(request.ScholarVerificationMedia);
                if (mediaResponse == null || !mediaResponse.Sucess||mediaResponse.Response.Id==0)
                    return new GenericResponse<StudySchoolarity>()
                    {
                        ErrorMessage = "Error on create media, verify media source",
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    };
                request.ScholarVerificationMedia = mediaResponse.Response;
            }

            //Create study schoolarity
            var studySchoolarityResult = await _studySchoolarityRepository.CreateStudySchoolarity(request);
            if (!studySchoolarityResult.Sucess)
                return studySchoolarityResult;

            //Create schoolarity list
            if (request.ScholarityList!=null &&request.ScholarityList.Count>0)
            {
                request.ScholarityList.ForEach(s =>
                {
                    s.StudySchoolarityId = studySchoolarityResult.Response.Id;
                    s.Doccument.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                });

                var scholarityResponse = await CreateSchoolarity(request.ScholarityList);
                if (scholarityResponse==null||!scholarityResponse.Sucess)
                {
                    await DeleteStudySchoolarity(studySchoolarityResult.Response.Id);
                    return new GenericResponse<StudySchoolarity>()
                    {
                        ErrorMessage = "Error creating schoolarity recotds",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }
                request.ScholarityList = scholarityResponse.Response;
            }

            // Create Extracurricular Activity
            if (request.ExtracurricularActivitiesList!=null && request.ExtracurricularActivitiesList.Count>0)
            {
                request.ExtracurricularActivitiesList.ForEach(s => s.StudySchoolarityId = studySchoolarityResult.Response.Id);

                var ExtracurricularActivitiesResponse = await CreateExtracurricularActivities(request.ExtracurricularActivitiesList);
                if (ExtracurricularActivitiesResponse==null||!ExtracurricularActivitiesResponse.Sucess)
                {
                    await DeleteStudySchoolarity(studySchoolarityResult.Response.Id);
                    return new GenericResponse<StudySchoolarity>()
                    {
                        ErrorMessage = "Error creating Extracurricular Activities",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }
                request.ExtracurricularActivitiesList= ExtracurricularActivitiesResponse.Response;
            }

            var studies = await GetStudySchoolarity(new List<long>() { studySchoolarityResult.Response.Id });

            if (!studies.Sucess)
                return new GenericResponse<StudySchoolarity>()
                {
                    StatusCode = studies.StatusCode,
                    ErrorMessage = studies.ErrorMessage
                };

            studySchoolarityResult.Response = studies.Response.FirstOrDefault();
            return studySchoolarityResult;
        }

        public async Task<GenericResponse<StudySchoolarity>> DeleteStudySchoolarity(long id)
        {
            return await _studySchoolarityRepository.DeleteStudySchoolarity(id);
        }

        public async Task<GenericResponse<List<StudySchoolarity>>> GetStudySchoolarity(List<long> id, bool byStudy = false)
        {
            var response = await _studySchoolarityRepository.GetStudySchoolarity(id, byStudy);

            if (!response.Sucess)
                return response;

            var mediaIdList = new List<long>();
            response.Response.ForEach(e=> mediaIdList.Add(e.ScholarVerificationMedia.Id));
            if(mediaIdList.Count > 0)
            {
                var mediaItems = await _mediaService.GetMedia(mediaIdList);
                response.Response.ForEach(e =>
                {
                    if (e.ScholarVerificationMedia!=null&&e.ScholarVerificationMedia.Id!=0)
                        e.ScholarVerificationMedia = mediaItems.Response?.FirstOrDefault(f => f.Id == e.ScholarVerificationMedia.Id);
                });
            }

            var doccumentIdList = new List<long>();
            response.Response.ForEach(e=> e.ScholarityList?.ForEach(f=> doccumentIdList.Add(f.DoccumentId)));
            if(doccumentIdList.Count > 0)
            {
                var doccumentItems = await _fileService.GetFile(doccumentIdList);
                response.Response.ForEach(e => e.ScholarityList?.ForEach(f => f.Doccument = doccumentItems.Response?.FirstOrDefault(g => f.DoccumentId == g.Id)));
            }

            return response;
        }

        public async Task<GenericResponse<StudySchoolarity>> UpdateStudySchoolarity(StudySchoolarity request)
        {
            var currentList = await GetStudySchoolarity(new List<long>() { request.Id });
            if (!currentList.Sucess || currentList.Response.Count==0)
                return new GenericResponse<StudySchoolarity>()
                {
                    ErrorMessage = "Study schoolarity not found",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var current = currentList.Response.FirstOrDefault();

            // add media if new
            if(request.ScholarVerificationMedia!=null&& !string.IsNullOrWhiteSpace(request.ScholarVerificationMedia.Base64Image)&&request.ScholarVerificationMedia.Id == 0)
            {
                request.ScholarVerificationMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                var mediaCreated = await _mediaService.CreateMedia(request.ScholarVerificationMedia);
                if (!mediaCreated.Sucess)
                    return new GenericResponse<StudySchoolarity>()
                    {
                        ErrorMessage = "Error creating media",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                request.ScholarVerificationMedia = mediaCreated.Response;
            }

            request.ScholarVerificationMedia = request.ScholarVerificationMedia==null||request.ScholarVerificationMedia.Id==0?current.ScholarVerificationMedia:request.ScholarVerificationMedia;
            request.ScholarVerificationSummary = String.IsNullOrWhiteSpace(request.ScholarVerificationSummary)? current.ScholarVerificationSummary:request.ScholarVerificationSummary;

            var response = await _studySchoolarityRepository.UpdateStudySchoolarity(request);
            if (response.Sucess)
            {
                currentList = await GetStudySchoolarity(new List<long>() { request.Id });
                response.Response = currentList.Response.FirstOrDefault();
            }
            return response;
        }


        // Extracurricular Activities
        public async Task<GenericResponse<List<ExtracurricularActivities>>> CreateExtracurricularActivities(List<ExtracurricularActivities> request)
        {
            return await _studySchoolarityRepository.CreateExtracurricularActivities(request);
        }

        public async Task<GenericResponse<ExtracurricularActivities>> DeleteExtracurricularActivities(long id)
        {
            return await _studySchoolarityRepository.DeleteExtracurricularActivities(id);
        }

        public async Task<GenericResponse<List<ExtracurricularActivities>>> GetExtracurricularActivities(List<long> id)
        {
            return await _studySchoolarityRepository.GetExtracurricularActivities(id);
        }

        public async Task<GenericResponse<ExtracurricularActivities>> UpdateExtracurricularActivities(ExtracurricularActivities request)
        {
            var currentList = await GetExtracurricularActivities(new List<long>() { request.Id });
            if (!currentList.Sucess || currentList.Response.Count==0)
                return new GenericResponse<ExtracurricularActivities>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var current = currentList.Response.First();

            request.Name = String.IsNullOrWhiteSpace(request.Name)?current.Name : request.Name;
            request.Period = String.IsNullOrWhiteSpace(request.Period) ?current.Period : request.Period;
            request.KnowledgeLevel = request.KnowledgeLevel == Common.Enums.KnowldegeLevel.NONE? current.KnowledgeLevel : request.KnowledgeLevel;
            request.Instituution = String.IsNullOrWhiteSpace(request.Instituution) ?current.Instituution : request.Instituution;


            var response = await _studySchoolarityRepository.UpdateExtracurricularActivities(request);
            if (response.Sucess)
            {
                var resultList = await GetExtracurricularActivities(new List<long>() { request.Id });
                response.Response = resultList.Response?.FirstOrDefault();
            }
            return response;
        }


        //Schoolarity
        public async Task<GenericResponse<List<Scholarity>>> CreateSchoolarity(List<Scholarity> request)
        {
            // Create doccuments
            List<Task<GenericResponse<Doccument>>> ScholarityDoccumentTaskList = new List<Task<GenericResponse<Doccument>>>();
            request.ForEach(s =>
            {
                s.Doccument.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                ScholarityDoccumentTaskList.Add(_fileService.CreateFile(s.Doccument, true));
            });

            bool success = true;
            while (ScholarityDoccumentTaskList.Count>0)
            {
                var current = await Task.WhenAny(ScholarityDoccumentTaskList);
                if (current.IsFaulted)
                {
                    success = false;
                    break;
                }

                // Set doccument with desired id
                foreach(var s in request)
                {
                    if (string.Compare(current.Result.Response.Base64Doccument, s.Doccument.Base64Doccument) == 0 && s.DoccumentId==0)
                    {
                        s.Doccument =  current.Result.Response;
                        s.DoccumentId = s.Doccument.Id;
                        break;
                    }
                }



                // remove current doccument
                ScholarityDoccumentTaskList.Remove(current);
            }

            if (!success)
            {
                return new GenericResponse<List<Scholarity>>()
                {
                    ErrorMessage = "Error uploading doccuments",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
            
            return await _studySchoolarityRepository.CreateSchoolarity(request);
        }

        public async Task<GenericResponse<Scholarity>> UpdateSchoolarity(Scholarity request)
        {
            var currentList = await GetSchoolarity(new List<long>() { request.Id });
            if (!currentList.Sucess || currentList.Response.Count==0)
                return new GenericResponse<Scholarity>()
                {
                    ErrorMessage = "Study schoolarity not found",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var current = currentList.Response.FirstOrDefault();

            // add doccum if new
            if (request.Doccument != null&& !string.IsNullOrWhiteSpace(request.Doccument.Base64Doccument)&&request.Doccument.Id == 0)
            {
                request.Doccument.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                var doccumentCreated = await _fileService.CreateFile(request.Doccument);
                if (!doccumentCreated.Sucess)
                    return new GenericResponse<Scholarity>()
                    {
                        ErrorMessage = "Error creating doccument",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                request.Doccument = doccumentCreated.Response;
            }

            request.Doccument = request.Doccument==null||request.Doccument.Id==0 ? current.Doccument : request.Doccument;
            request.SchoolarLevel = request.SchoolarLevel == Common.Enums.SchoolarLevel.NONE ? current.SchoolarLevel : request.SchoolarLevel;
            request.Career = String.IsNullOrWhiteSpace(request.Career) ? current.Career : request.Career;
            request.Period = String.IsNullOrWhiteSpace(request.Period) ? current.Period : request.Period;
            request.TimeOnCareer = String.IsNullOrWhiteSpace(request.TimeOnCareer) ? current.TimeOnCareer : request.TimeOnCareer;
            request.Institution = String.IsNullOrWhiteSpace(request.Institution) ? current.Institution : request.Institution;

            var response =  await _studySchoolarityRepository.UpdateSchoolarity(request);
            if(response.Sucess)
            {
                var updatedList = await GetSchoolarity(new List<long>() { request.Id });
                response.Response = updatedList.Response.FirstOrDefault();
            }
            return response;
        }

        public async Task<GenericResponse<List<Scholarity>>> GetSchoolarity(List<long> id)
        {
            var response = await _studySchoolarityRepository.GetSchoolarity(id);

            if (!response.Sucess)
                return response;

            var doccumentsIdList = new List<long>();
            response.Response.ForEach(e => doccumentsIdList.Add(e.DoccumentId));

            if (doccumentsIdList.Count>0)
            {
                var doccumentsList = await _fileService.GetFile(doccumentsIdList);
                response.Response.ForEach(e => e.Doccument = doccumentsList.Response.FirstOrDefault(f => f.Id == e.DoccumentId));
            }

            return response;
        }

        public async Task<GenericResponse<Scholarity>> DeleteSchoolarity(long id)
        {
            return await _studySchoolarityRepository.DeleteSchoolarity(id); 
        }
    }
}
