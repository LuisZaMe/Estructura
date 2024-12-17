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
    public class UtilitiesService : IUtilitiesService
    {
        private readonly IUtilitiesRepository _utilitiesRepository;

        public UtilitiesService(IUtilitiesRepository utilitiesRepository)
        {
            this._utilitiesRepository = utilitiesRepository;
        }

        public async Task<GenericResponse<string>> GetCurrentVersion()
        {
            return new GenericResponse<string>()
            {
                Response = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                Sucess = true,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        public async Task<GenericResponse<bool>> IsAppCompatible(int appBuildVersion)
        {
            var response = new GenericResponse<bool>();
            var compatibility = await _utilitiesRepository.IsAppCompatible(appBuildVersion);
            if (compatibility == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ErrorMessage = "No compatible versions found";
                return response;
            }

            //response.Response = appBuildVersion >= compatibility.Response.AppVersion;

            response.Sucess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;

            return response;
        }

        public async Task<GenericResponse<bool>> VerifyEmailExist(string email)
        {
            var response = new GenericResponse<bool>();
            response.Sucess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            var exist = await _utilitiesRepository.VerifyEmailExist(email);
            if(exist!=null&&exist.Sucess)
                response.Response = exist.Response;
            else
                response.Response = false;

            return response;
        }

        public async Task<GenericResponse<List<Role>>> GetRoles()
        {
            return await _utilitiesRepository.GetRoles();
        }

        public async Task<GenericResponse<List<City>>> GetCities(int stateId)
        {
            return await _utilitiesRepository.GetCities(stateId);
        }

        public async Task<GenericResponse<List<State>>> GetStates()
        {
            return await _utilitiesRepository.GetStates();
        }

    }
}
