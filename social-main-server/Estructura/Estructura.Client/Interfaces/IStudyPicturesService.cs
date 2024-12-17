using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IStudyPicturesService
    {
        Task<GenericResponse<StudyPictures>> CreateStudyPictures(StudyPictures request);
        Task<GenericResponse<List<StudyPictures>>> GetStudyPictures(List<long> id, bool byStudy = false);
        Task<GenericResponse<StudyPictures>> UpdateStudyPictures(StudyPictures request);
        Task<GenericResponse<StudyPictures>> DeleteStudyPictures(long id);
    }
}
