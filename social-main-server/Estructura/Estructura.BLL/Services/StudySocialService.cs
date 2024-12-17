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
    public class StudySocialService : IStudySocialService
    {
        private readonly IStudySocialRepository _studySocialRepository;
        public StudySocialService(IStudySocialRepository _studySocialRepository)
        {
            this._studySocialRepository=_studySocialRepository;
        }

        // Study Social
        public async Task<GenericResponse<StudySocial>> CreateStudySocial(StudySocial request)
        {
            if (request.StudyId ==0)
                return new GenericResponse<StudySocial>()
                {
                    ErrorMessage = "Study id required",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var response = await _studySocialRepository.CreateStudySocial(request);
            if(!response.Sucess)
                return new GenericResponse<StudySocial>()
                {
                    ErrorMessage = "Error creating studySocial, verify information",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            if (request.SocialGoalsList!=null&&request.SocialGoalsList.Count>0)
            {
                request.SocialGoalsList.ForEach(e => e.StudySocialId = response.Response.Id);
                var items = await CreateSocialGoals(request.SocialGoalsList);
                if (!items.Sucess)
                {
                    await DeleteStudySocial(response.Response.Id);
                    return new GenericResponse<StudySocial>()
                    {
                        ErrorMessage = "error creating goals, verify information",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }
                response.Response.SocialGoalsList = items.Response;
            }
            return response;
        }

        public async Task<GenericResponse<StudySocial>> DeleteStudySocial(long id)
        {
            return await _studySocialRepository.DeleteStudySocial(id);
        }

        public async Task<GenericResponse<List<StudySocial>>> GetStudySocial(List<long> id, bool byStudy = false)
        {
            return await _studySocialRepository.GetStudySocial(id, byStudy);
        }

        public async Task<GenericResponse<StudySocial>> UpdateStudySocial(StudySocial request)
        {
            var currentList = await GetStudySocial(new List<long>() { request.Id });
            if (!currentList.Sucess || currentList.Response.Count ==0)
                return new GenericResponse<StudySocial>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

            var current = currentList.Response.FirstOrDefault();

            request.SocialArea = String.IsNullOrWhiteSpace(request.SocialArea) ? current.SocialArea : request.SocialArea;
            request.Hobbies = String.IsNullOrWhiteSpace(request.Hobbies) ? current.Hobbies : request.Hobbies;
            request.HealthInformation = String.IsNullOrWhiteSpace(request.HealthInformation) ? current.HealthInformation : request.HealthInformation;

            var response = await _studySocialRepository.UpdateStudySocial(request);
            var responseList = await GetStudySocial(new List<long>() { request.Id });

            if (responseList.Sucess)
                response.Response = responseList.Response.FirstOrDefault();

            return response;
        }



        // Social Goals
        public async Task<GenericResponse<List<SocialGoals>>> CreateSocialGoals(List<SocialGoals> request)
        {
            if (request.Any(e=> e.StudySocialId == 0))
                return new GenericResponse<List<SocialGoals>>()
                {
                    ErrorMessage = "Study Social id required",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            return await _studySocialRepository.CreateSocialGoals(request);
        }

        public async Task<GenericResponse<SocialGoals>> DeleteSocialGoals(long id)
        {
            return await _studySocialRepository.DeleteSocialGoals(id);
        }

        public async Task<GenericResponse<List<SocialGoals>>> GetSocialGoals(List<long> id)
        {
            return await _studySocialRepository.GetSocialGoals(id);
        }

        public async Task<GenericResponse<SocialGoals>> UpdateSocialGoals(SocialGoals request)
        {
            var currentList = await GetSocialGoals(new List<long>() { request.Id });
            if (!currentList.Sucess || currentList.Response.Count ==0)
                return new GenericResponse<SocialGoals>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

            var current = currentList.Response.FirstOrDefault();

            request.CoreValue = String.IsNullOrWhiteSpace(request.CoreValue) ? current.CoreValue : request.CoreValue;
            request.LifeGoal = String.IsNullOrWhiteSpace(request.LifeGoal) ? current.LifeGoal : request.LifeGoal;
            request.NextGoal = String.IsNullOrWhiteSpace(request.NextGoal) ? current.NextGoal : request.NextGoal;

            var response = await _studySocialRepository.UpdateSocialGoals(request);
            var responseList = await GetSocialGoals(new List<long>() { request.Id });

            if (responseList.Sucess)
                response.Response = responseList.Response.FirstOrDefault();

            return response;
        }
    }
}
