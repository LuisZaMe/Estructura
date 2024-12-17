using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IStudySocialService
    {
        // Study Social
        Task<GenericResponse<StudySocial>> CreateStudySocial(StudySocial request);
        Task<GenericResponse<StudySocial>> UpdateStudySocial(StudySocial request);
        Task<GenericResponse<List<StudySocial>>> GetStudySocial(List<long> id);
        Task<GenericResponse<StudySocial>> DeleteStudySocial(long id);



        // Social Goals
        Task<GenericResponse<List<SocialGoals>>> CreateSocialGoals(List<SocialGoals> request);
        Task<GenericResponse<SocialGoals>> UpdateSocialGoals(SocialGoals request);
        Task<GenericResponse<List<SocialGoals>>> GetSocialGoals(List<long> id);
        Task<GenericResponse<SocialGoals>> DeleteSocialGoals(long id);
    }
}
