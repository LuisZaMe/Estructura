using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IStudyPersonalReferenceService
    {
        Task<GenericResponse<List<StudyPersonalReference>>> CreateStudyPersonalReference(List<StudyPersonalReference> request);
        Task<GenericResponse<List<StudyPersonalReference>>> GetStudyPersonalReference(List<long> id, bool byStudy = false);
        Task<GenericResponse<StudyPersonalReference>> UpdateStudyPersonalReference(StudyPersonalReference request);
        Task<GenericResponse<StudyPersonalReference>> DeleteStudyPersonalReference(long id);
    }
}
