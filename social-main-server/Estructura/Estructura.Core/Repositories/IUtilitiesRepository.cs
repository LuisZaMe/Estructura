using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Repositories
{
    public interface IUtilitiesRepository
    {
        public string baseFilePath { get; }
        public string basePath { get; }
        public string GENERAL { get; }
        public string EVIDENCE { get; }
        public string PROFILE { get; }
        Task<Common.Response.GenericResponse<Compatibility>> IsAppCompatible(int appBuildVersion);
        Task<Common.Response.GenericResponse<bool>> VerifyEmailExist(string email);
        Task<Common.Response.GenericResponse<List<City>>> GetCities(int stateId);
        Task<Common.Response.GenericResponse<List<State>>> GetStates();
        ConfigurationReflection.APIConfig GetConfig();
        Task<GenericResponse<List<Role>>> GetRoles();
        Task<GenericResponse<bool>> SendMail(string to, string body, string subject);
        string ImageURLFormatter(Media media);
        string FileURLFormatter(Doccument file);
    }
}
