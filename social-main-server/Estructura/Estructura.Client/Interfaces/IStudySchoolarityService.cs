using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IStudySchoolarityService
    {
        // Study scholarity
        Task<GenericResponse<StudySchoolarity>> CreateStudySchoolarity(StudySchoolarity request);
        Task<GenericResponse<StudySchoolarity>> UpdateStudySchoolarity(StudySchoolarity request);
        Task<GenericResponse<StudySchoolarity>> DeleteStudySchoolarity(long id);
        Task<GenericResponse<List<StudySchoolarity>>> GetStudySchoolarity(List<long> id);

        // Scholarity
        Task<GenericResponse<List<Scholarity>>> CreateSchoolarity(List<Scholarity> request);
        Task<GenericResponse<Scholarity>> UpdateSchoolarity(Scholarity request);
        Task<GenericResponse<Scholarity>> DeleteSchoolarity(long id);
        Task<GenericResponse<List<Scholarity>>> GetSchoolarity(List<long> id);

        // Extracurricular Activities
        Task<GenericResponse<List<ExtracurricularActivities>>> CreateExtracurricularActivities(List<ExtracurricularActivities> request);
        Task<GenericResponse<ExtracurricularActivities>> UpdateExtracurricularActivities(ExtracurricularActivities request);
        Task<GenericResponse<ExtracurricularActivities>> DeleteExtracurricularActivities(long id);
        Task<GenericResponse<List<ExtracurricularActivities>>> GetExtracurricularActivities(List<long> id);
    }
}
