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
    public class StudyFamilyService: IStudyFamilyService
    {
        private readonly IStudyFamilyRepository _studyFamilyRepository;
        public StudyFamilyService(IStudyFamilyRepository _studyFamilyRepository)
        {
            this._studyFamilyRepository=_studyFamilyRepository;
        }



        //StudyFamily
        public async Task<GenericResponse<StudyFamily>> CreateStudyFamily(StudyFamily request)
        {
            if (request.StudyId == 0)
                return new GenericResponse<StudyFamily>()
                {
                    ErrorMessage = "StudyId required",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var response = await _studyFamilyRepository.CreateStudyFamily(request);
            if (!response.Sucess)
                return response;

            // LivingFamily
            if (request.LivingFamilyList?.Count>0)
            {
                request.LivingFamilyList.ForEach(e => e.StudyFamilyId = response.Response.Id);
                var familyListResponse = await CreateLivingFamily(request.LivingFamilyList);
                if (!familyListResponse.Sucess)
                {
                    await DeleteStudyFamily(response.Response.Id);
                    return new GenericResponse<StudyFamily>()
                    {
                        ErrorMessage = "Error creating living family items, verify information and try again",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }
                response.Response.LivingFamilyList = familyListResponse.Response;
            }

            // NoLivingFamily
            if (request.NoLivingFamilyList?.Count>0)
            {
                request.NoLivingFamilyList.ForEach(e => e.StudyFamilyId = response.Response.Id);
                var familyListResponse = await CreateNoLivingFamily(request.NoLivingFamilyList);
                if (!familyListResponse.Sucess)
                {
                    await DeleteStudyFamily(response.Response.Id);
                    return new GenericResponse<StudyFamily>()
                    {
                        ErrorMessage = "Error creating No living family items, verify information and try again",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }
                response.Response.NoLivingFamilyList = familyListResponse.Response;
            }

            var responseList = await GetStudyFamily(new List<long>() { response.Response.Id });
            if (responseList.Sucess && responseList.Response.Count>0)
                response.Response = responseList.Response.FirstOrDefault();
            return response;
        }

        public async Task<GenericResponse<StudyFamily>> DeleteStudyFamily(long id)
        {
            return await _studyFamilyRepository.DeleteStudyFamily(id);
        }

        public async Task<GenericResponse<List<StudyFamily>>> GetStudyFamily(List<long> id, bool byStudy = false)
        {
            return await _studyFamilyRepository.GetStudyFamily(id, byStudy);
        }

        public async Task<GenericResponse<StudyFamily>> UpdateStudyFamily(StudyFamily request)
        {
            var currentList = await GetStudyFamily(new List<long>() { request.Id });
            if (!currentList.Sucess || currentList.Response?.Count ==0)
                return new GenericResponse<StudyFamily>()
                {
                    ErrorMessage = "study family Not found",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var current = currentList.Response.FirstOrDefault();

            request.Notes = String.IsNullOrWhiteSpace(request.Notes) ? current.Notes : request.Notes;
            request.FamiliarArea = String.IsNullOrWhiteSpace(request.FamiliarArea) ? current.FamiliarArea : request.FamiliarArea;

            var response = await _studyFamilyRepository.UpdateStudyFamily(request);

            var responseList = await GetStudyFamily(new List<long>() { request.Id });
            if (responseList.Sucess && responseList.Response.Count>0)
                response.Response = responseList.Response.FirstOrDefault();
            return response;
        }



        // LivingFamily
        public async Task<GenericResponse<List<LivingFamily>>> CreateLivingFamily(List<LivingFamily> request)
        {
            if (request.Any(e => e.StudyFamilyId ==0))
                return new GenericResponse<List<LivingFamily>>()
                {
                    ErrorMessage = "Study family id required on all items",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            return await _studyFamilyRepository.CreateLivingFamily(request);
        }

        public async Task<GenericResponse<LivingFamily>> DeleteLivingFamily(long id)
        {
            return await _studyFamilyRepository.DeleteLivingFamily(id);
        }

        public async Task<GenericResponse<List<LivingFamily>>> GetLivingFamily(List<long> id)
        {
            return await _studyFamilyRepository.GetLivingFamily(id);
        }

        public async Task<GenericResponse<LivingFamily>> UpdateLivingFamily(LivingFamily request)
        {
            var currentList = await GetLivingFamily(new List<long>() { request.Id });
            if (!currentList.Sucess || currentList.Response?.Count ==0)
                return new GenericResponse<LivingFamily>()
                {
                    ErrorMessage = "Living family Not found",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var current = currentList.Response.FirstOrDefault();

            request.Name = String.IsNullOrWhiteSpace(request.Name) ? current.Name : request.Name;
            request.Relationship = String.IsNullOrWhiteSpace(request.Relationship) ? current.Relationship : request.Relationship;
            request.Age = String.IsNullOrWhiteSpace(request.Age) ? current.Age : request.Age;
            request.MaritalStatus = request.MaritalStatus == Common.Enums.MaritalStatus.NONE ? current.MaritalStatus : request.MaritalStatus;
            request.Schoolarity = String.IsNullOrWhiteSpace(request.Schoolarity) ? current.Schoolarity : request.Schoolarity;
            request.Address = String.IsNullOrWhiteSpace(request.Address) ? current.Address : request.Address;
            request.Phone = String.IsNullOrWhiteSpace(request.Phone) ? current.Phone : request.Phone;
            request.Occupation = String.IsNullOrWhiteSpace(request.Occupation) ? current.Occupation : request.Occupation;

            var response = await _studyFamilyRepository.UpdateLivingFamily(request);

            var responseList = await GetLivingFamily(new List<long>() { request.Id });
            if (responseList.Sucess && responseList.Response.Count>0)
                response.Response = responseList.Response.FirstOrDefault();
            return response;
        }



        //NoLivingFamily
        public async Task<GenericResponse<List<NoLivingFamily>>> CreateNoLivingFamily(List<NoLivingFamily> request)
        {
            if (request.Any(e => e.StudyFamilyId ==0))
                return new GenericResponse<List<NoLivingFamily>>()
                {
                    ErrorMessage = "Study family id required on all items",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            return await _studyFamilyRepository.CreateNoLivingFamily(request);
        }

        public async Task<GenericResponse<NoLivingFamily>> UpdateNoLivingFamily(NoLivingFamily request)
        {
            var currentList = await GetNoLivingFamily(new List<long>() { request.Id });
            if (!currentList.Sucess || currentList.Response?.Count ==0)
                return new GenericResponse<NoLivingFamily>()
                {
                    ErrorMessage = "Living family Not found",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var current = currentList.Response.FirstOrDefault();

            request.Name = String.IsNullOrWhiteSpace(request.Name) ? current.Name : request.Name;
            request.Relationship = String.IsNullOrWhiteSpace(request.Relationship) ? current.Relationship : request.Relationship;
            request.Age = String.IsNullOrWhiteSpace(request.Age) ? current.Age : request.Age;
            request.MaritalStatus = request.MaritalStatus == Common.Enums.MaritalStatus.NONE ? current.MaritalStatus : request.MaritalStatus;
            request.Schoolarity = String.IsNullOrWhiteSpace(request.Schoolarity) ? current.Schoolarity : request.Schoolarity;
            request.Address = String.IsNullOrWhiteSpace(request.Address) ? current.Address : request.Address;
            request.Phone = String.IsNullOrWhiteSpace(request.Phone) ? current.Phone : request.Phone;
            request.Occupation = String.IsNullOrWhiteSpace(request.Occupation) ? current.Occupation : request.Occupation;

            var response = await _studyFamilyRepository.UpdateNoLivingFamily(request);

            var responseList = await GetNoLivingFamily(new List<long>() { request.Id });
            if (responseList.Sucess && responseList.Response.Count>0)
                response.Response = responseList.Response.FirstOrDefault();
            return response;
        }

        public async Task<GenericResponse<List<NoLivingFamily>>> GetNoLivingFamily(List<long> id)
        {
            return await _studyFamilyRepository.GetNoLivingFamily(id);
        }

        public async Task<GenericResponse<NoLivingFamily>> DeleteNoLivingFamily(long id)
        {
            return await _studyFamilyRepository.DeleteNoLivingFamily(id);
        }
    }
}
