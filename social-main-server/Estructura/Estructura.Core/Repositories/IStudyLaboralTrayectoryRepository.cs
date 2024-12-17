using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Repositories
{
    public interface IStudyLaboralTrayectoryRepository
    {
        Task<GenericResponse<List<StudyLaboralTrayectory>>> CreateStudyLaboralTrayectory(List<StudyLaboralTrayectory> request);
        Task<GenericResponse<List<StudyLaboralTrayectory>>> GetStudyLaboralTrayectory(List<long> id, bool byStudy = false);
        Task<GenericResponse<StudyLaboralTrayectory>> UpdateStudyLaboralTrayectory(StudyLaboralTrayectory request);
        Task<GenericResponse<StudyLaboralTrayectory>> DeleteStudyLaboralTrayectory(long id);

    }
}
