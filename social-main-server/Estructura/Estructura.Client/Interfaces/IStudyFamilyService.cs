using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IStudyFamilyService
    {
        //StudyFamily
        Task<GenericResponse<StudyFamily>> CreateStudyFamily(StudyFamily request);
        Task<GenericResponse<StudyFamily>> UpdateStudyFamily(StudyFamily request);
        Task<GenericResponse<List<StudyFamily>>> GetStudyFamily(List<long> id);
        Task<GenericResponse<StudyFamily>> DeleteStudyFamily(long id);


        //LivingFamily
        Task<GenericResponse<List<LivingFamily>>> CreateLivingFamily(List<LivingFamily> request);
        Task<GenericResponse<LivingFamily>> UpdateLivingFamily(LivingFamily request);
        Task<GenericResponse<List<LivingFamily>>> GetLivingFamily(List<long> id);
        Task<GenericResponse<LivingFamily>> DeleteLivingFamily(long id);


        //NoLivingFamily
        Task<GenericResponse<List<NoLivingFamily>>> CreateNoLivingFamily(List<NoLivingFamily> request);
        Task<GenericResponse<NoLivingFamily>> UpdateNoLivingFamily(NoLivingFamily request);
        Task<GenericResponse<List<NoLivingFamily>>> GetNoLivingFamily(List<long> id);
        Task<GenericResponse<NoLivingFamily>> DeleteNoLivingFamily(long id);
    }
}
