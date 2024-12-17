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
    public class StudyIMSSValidationService : IStudyIMSSValidationService
    {
        private readonly IStudyIMSSValidationRepository _studyIMSSValidationRepository;
        public StudyIMSSValidationService(IStudyIMSSValidationRepository _studyIMSSValidationRepository)
        {
            this._studyIMSSValidationRepository=_studyIMSSValidationRepository;
        }


        public async Task<GenericResponse<StudyIMSSValidation>> CreateStudyIMSSValidation(StudyIMSSValidation request)
        {
            if (request.StudyId == 0)
                return new GenericResponse<StudyIMSSValidation>()
                {
                    ErrorMessage = "StudyId is required",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var response = await _studyIMSSValidationRepository.CreateStudyIMSSValidation(request);
            if (!response.Sucess)
                return response;

            if (request.IMSSValidationList?.Count>0)
            {
                request.IMSSValidationList?.ForEach(e => e.StudyIMSSValidationId = response.Response.Id);
                var imssvalidationResponse = await CreateIMSSValidation(request.IMSSValidationList);
                if (!imssvalidationResponse.Sucess)
                {
                    await DeleteStudyIMSSValidation(response.Response.Id);
                    return new GenericResponse<StudyIMSSValidation>()
                    {
                        ErrorMessage = "Error creating IMSS Validation items, verify information",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }
                response.Response.IMSSValidationList = imssvalidationResponse.Response;
            }
            return response;
        }

        public async Task<GenericResponse<StudyIMSSValidation>> UpdateStudyIMSSValidation(StudyIMSSValidation request)
        {
            var currentList = await GetStudyIMSSValidation(new List<long>() { request.Id });
            if (!currentList.Sucess)
                return new GenericResponse<StudyIMSSValidation>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var current = currentList.Response.FirstOrDefault();

            request.CreditNumber = String.IsNullOrWhiteSpace(request.CreditNumber) ? current.CreditNumber : request.CreditNumber;
            request.CreditStatus = String.IsNullOrWhiteSpace(request.CreditStatus) ? current.CreditStatus : request.CreditStatus;
            request.GrantDate = request.GrantDate == DateTime.MinValue ? current.GrantDate : request.GrantDate;
            request.ConciliationClaimsSummary = String.IsNullOrWhiteSpace(request.ConciliationClaimsSummary) ? current.ConciliationClaimsSummary : request.ConciliationClaimsSummary;

            var response = await _studyIMSSValidationRepository.UpdateStudyIMSSValidation(request);
            if (response.Sucess)
            {
                currentList = await GetStudyIMSSValidation(new List<long>() { request.Id });
                if (currentList.Sucess)
                    response.Response = currentList.Response.FirstOrDefault();
            }
            return response;
        }

        public async Task<GenericResponse<List<StudyIMSSValidation>>> GetStudyIMSSValidation(List<long> id, bool byStudy = false)
        {
            return await _studyIMSSValidationRepository.GetStudyIMSSValidation(id, byStudy);
        }

        public async Task<GenericResponse<StudyIMSSValidation>> DeleteStudyIMSSValidation(long id)
        {
            return await _studyIMSSValidationRepository.DeleteStudyIMSSValidation(id);
        }




        public async Task<GenericResponse<List<IMSSValidation>>> GetIMSSValidation(List<long> id)
        {
            return await _studyIMSSValidationRepository.GetIMSSValidation(id);
        }

        public async Task<GenericResponse<IMSSValidation>> UpdateIMSSValidation(IMSSValidation request)
        {
            var currentList = await GetIMSSValidation(new List<long>() { request.Id });
            if (!currentList.Sucess)
                return new GenericResponse<IMSSValidation>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var current = currentList.Response.FirstOrDefault();

            request.CompanyBusinessName = String.IsNullOrWhiteSpace(request.CompanyBusinessName) ? current.CompanyBusinessName : request.CompanyBusinessName;

            request.StartDate = request.StartDate == DateTime.MinValue ? current.StartDate : request.StartDate;
            request.EndDate = request.EndDate == DateTime.MinValue ? current.EndDate : request.EndDate;
            request.Result = String.IsNullOrWhiteSpace(request.Result) ? current.Result : request.Result;

            var response = await _studyIMSSValidationRepository.UpdateIMSSValidation(request);
            if (response.Sucess)
            {
                currentList = await GetIMSSValidation(new List<long>() { request.Id });
                if (currentList.Sucess)
                    response.Response = currentList.Response.FirstOrDefault();
            }
            return response;
        }
        
        public async Task<GenericResponse<List<IMSSValidation>>> CreateIMSSValidation(List<IMSSValidation> request)
        {
            if (request.Any(e => e.StudyIMSSValidationId == 0))
                return new GenericResponse<List<IMSSValidation>>()
                {
                    ErrorMessage = "All items need a StudyIMSSValidationId",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            return await _studyIMSSValidationRepository.CreateIMSSValidation(request);
        }

        public async Task<GenericResponse<IMSSValidation>> DeleteIMSSValidation(long id)
        {
            return await _studyIMSSValidationRepository.DeleteIMSSValidation(id);
        }
    }
}
