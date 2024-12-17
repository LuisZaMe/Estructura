using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IStudyLaboralTrayectoryService
    {
        Task<GenericResponse<List<StudyLaboralTrayectory>>> CreateStudyLaboralTrayectory(List<StudyLaboralTrayectory> request);
        Task<GenericResponse<List<StudyLaboralTrayectory>>> GetStudyLaboralTrayectory(List<long> id);
        Task<GenericResponse<StudyLaboralTrayectory>> UpdateStudyLaboralTrayectory(StudyLaboralTrayectory request);
        Task<GenericResponse<StudyLaboralTrayectory>> DeleteStudyLaboralTrayectory(long id);
    }
}
