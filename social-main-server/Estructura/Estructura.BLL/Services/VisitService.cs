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
    public class VisitService: IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IStudyService _studyService;
        private readonly IMediaService _mediaService;
        public VisitService(IMediaService _mediaService, IStudyService _studyService, IVisitRepository _visitRepository)
        {
            this._visitRepository=_visitRepository;
            this._studyService=_studyService;
            this._mediaService=_mediaService;
        }

        public async Task<GenericResponse<Visit>> CreateVisit(Visit request)
        {
            string errorCumulate = string.Empty;
            if (request.Study == null ||request.Study.Id == 0)
                errorCumulate += string.IsNullOrWhiteSpace(errorCumulate) ? "Study required" : ", Study required";
            if (request.VisitDate == null || request.VisitDate == DateTime.MinValue.ToString())
                errorCumulate += string.IsNullOrWhiteSpace(errorCumulate) ? "Visit date required" : ", Visit date required";
            if (request.City == null || request.City.Id == 0)
                errorCumulate += string.IsNullOrWhiteSpace(errorCumulate) ? "city required" : ", city required";
            if (request.State == null || request.State.Id == 0)
                errorCumulate += string.IsNullOrWhiteSpace(errorCumulate) ? "state required" : ", state required";
            if (string.IsNullOrWhiteSpace(request.Address))
                errorCumulate += string.IsNullOrWhiteSpace(errorCumulate) ? "address required" : ", address required";

            if (!string.IsNullOrWhiteSpace(errorCumulate))
            {
                return new GenericResponse<Visit>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = errorCumulate
                };
            }

            var response = await _visitRepository.CreateVisit(request);
            if(response!=null && response.Sucess)
            {
                var itemResponse = await GetVisit(new List<long>() { response.Response.Id }, null, null, null, null, null, null, null, null, VisitStatus.NONE, 0, 10);
                response.Response = itemResponse.Response.FirstOrDefault();
            }
            return response;
        }

        public async Task<GenericResponse<List<Visit>>> GetVisit(List<long> Id, long? interviewerId, long? candidateId, long? clientId, long? studyId, DateTime? startDate, DateTime? endDate, int? cityId, int? stateId, VisitStatus visitStatus = VisitStatus.NONE, int currentPage = 0, int offset = 10)
        {
            var visits = await _visitRepository.GetVisit(Id, interviewerId, candidateId, clientId, studyId, startDate, endDate, cityId, stateId, visitStatus, currentPage, offset);

            if(visits.Sucess && visits.Response!=null && visits.Response.Count>0)
            {
                List<long> studyIds = new List<long>();
                List<long> mediaIds = new List<long>();
                visits.Response.ForEach(e =>
                {
                    studyIds.Add(e.Study.Id);
                    if (e.Evidence!=null&&e.Evidence.Id!=0)
                        mediaIds.Add(e.Evidence.Id);
                });
                var studies = _studyService.GetStudy(studyIds, 0, null, null);
                var media = _mediaService.GetMedia(mediaIds);

                await Task.WhenAll(new Task[] { studies, media });

                if (studies!=null&& studies.IsCompletedSuccessfully && studies.Result.Sucess)
                    visits.Response.ForEach(e => e.Study = studies.Result.Response.FirstOrDefault(f => e.Study.Id == f.Id));

                if (media!=null&& media.IsCompletedSuccessfully && media.Result.Sucess)
                    visits.Response.ForEach(e => e.Evidence = media.Result.Response.FirstOrDefault(f => e.Evidence.Id == f.Id));
            }
            return visits;
        }

        public async Task<GenericResponse<Visit>> UpdateVisit(Visit request)
        {
            var visit = await GetVisit(new List<long>() { request.Id }, null, null, null, null, null, null, null, null, VisitStatus.NONE, 0, 10);
            if (visit==null||!visit.Sucess || visit.Response.Count==0)
                return new GenericResponse<Visit>() { StatusCode = System.Net.HttpStatusCode.BadRequest, ErrorMessage = "Visit not found" };
            
            //Create evidence image
            if(request.Evidence!=null && request.Evidence.Id == 0 && !string.IsNullOrWhiteSpace(request.Evidence.Base64Image))
            {
                request.Evidence.StoreMediaType = StoreMediaType.EVIDENCE;
                var mediaResult = await _mediaService.CreateMedia(request.Evidence);
                request.Evidence = mediaResult.Response;
            }



            var current = visit.Response.First();
            request.VisitDate = request.VisitDate == null || request.VisitDate == DateTime.MinValue.ToString() ? current.VisitDate : request.VisitDate;
            request.VisitStatus = request.VisitStatus == VisitStatus.NONE ? current.VisitStatus : request.VisitStatus;
            request.City = request.City == null||request.City.Id ==0 ? current.City : request.City;
            request.State = request.State == null||request.State.Id ==0 ? current.State : request.State;
            request.Address = string.IsNullOrWhiteSpace(request.Address) ? current.Address : request.Address;
            request.Observations = string.IsNullOrWhiteSpace(request.Observations) ? current.Observations : request.Observations;
            request.NotationColor = string.IsNullOrWhiteSpace(request.NotationColor) ? current.NotationColor : request.NotationColor;
            request.Evidence = request.Evidence == null || request.Evidence.Id == 0 ? current.Evidence : request.Evidence;

            var response = await _visitRepository.UpdateVisit(request);
            if (response!=null&&response.Sucess)
            {
                var itemResponse = await GetVisit(new List<long>() { response.Response.Id }, null, null, null, null, null, null, null, null, VisitStatus.NONE, 0, 10);
                response.Response = itemResponse.Response.FirstOrDefault();
            }
            return response;
        }
        
        public async Task<GenericResponse<Visit>> DeleteVisit(long Id)
        {
            return await _visitRepository.DeleteVisit(Id);
        }
        
        public async Task<GenericResponse<int>> Pagination(List<long> Id, long? interviewerId, long? candidateId, long? clientId, long? studyId, DateTime? startDate, DateTime? endDate, int? cityId, int? stateId, VisitStatus visitStatus = VisitStatus.NONE, int splitBy = 10)
        {
            return await _visitRepository.Pagination(Id, interviewerId, candidateId, clientId, studyId, startDate, endDate, cityId, stateId, visitStatus, splitBy);
        }
    }
}
