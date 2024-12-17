using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Services
{
    public interface IStudyFinalResultService
    {
        Task<GenericResponse<StudyFinalResult>> CreateStudyFinalResult(StudyFinalResult request);
        Task<GenericResponse<StudyFinalResult>> UpdateStudyFinalResult(StudyFinalResult request);
        Task<GenericResponse<List<StudyFinalResult>>> GetStudyFinalResult(List<long> id, int currentPage, int offset, bool byStudy = false);
        Task<GenericResponse<StudyFinalResult>> DeleteStudyFinalResult(long id);
    }
}
