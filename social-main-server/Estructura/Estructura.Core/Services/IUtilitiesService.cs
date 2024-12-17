using Estructura.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Services
{
    public interface IUtilitiesService
    {
        Task<Common.Response.GenericResponse<string>> GetCurrentVersion();
        Task<Common.Response.GenericResponse<bool>> IsAppCompatible(int appBuildVersion);
        Task<Common.Response.GenericResponse<bool>> VerifyEmailExist(string email);
        Task<Common.Response.GenericResponse<List<City>>> GetCities(int stateId);
        Task<Common.Response.GenericResponse<List<State>>> GetStates();
    }
}
