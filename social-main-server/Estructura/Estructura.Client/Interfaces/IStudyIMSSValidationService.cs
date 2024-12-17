using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IStudyIMSSValidationService
    {
        // IMSSValidation
        Task<GenericResponse<StudyIMSSValidation>> CreateStudyIMSSValidation(StudyIMSSValidation request);
        Task<GenericResponse<StudyIMSSValidation>> UpdateStudyIMSSValidation(StudyIMSSValidation request);
        Task<GenericResponse<List<StudyIMSSValidation>>> GetStudyIMSSValidation(List<long> id);
        Task<GenericResponse<StudyIMSSValidation>> DeleteStudyIMSSValidation(long id);



        // IMSSValidation
        Task<GenericResponse<List<IMSSValidation>>> CreateIMSSValidation(List<IMSSValidation> request);
        Task<GenericResponse<IMSSValidation>> UpdateIMSSValidation(IMSSValidation request);
        Task<GenericResponse<List<IMSSValidation>>> GetIMSSValidation(List<long> id);
        Task<GenericResponse<IMSSValidation>> DeleteIMSSValidation(long id);
    }
}
