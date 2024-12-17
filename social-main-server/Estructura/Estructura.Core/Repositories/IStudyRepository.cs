using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Repositories
{
    public interface IStudyRepository
    {
        Task<GenericResponse<Study>> CreateStudy(Study request);
        Task<GenericResponse<List<Study>>> GetStudy(
            List<long> Id,
            long AnalystId,
            DateTime? startDate,
            DateTime? endDate,
            Common.Enums.ServiceTypes serviceType = Common.Enums.ServiceTypes.NONE,
            long interviewerId = 0,
            Common.Enums.StudyStatus studyStatus = Common.Enums.StudyStatus.NONE,
            long clientId = 0,
            long candidateId = 0,
            StudyProgressStatus studyProgressStatus = StudyProgressStatus.NONE,
            int currentPage = 0,
            int offset = 10,
            bool bringStudiesOnlyOwn = true,
            int bringStudiesOnly = 0
        );
        Task<GenericResponse<int>> Pagination(List<long> Id, DateTime? startDate, DateTime? endDate, Common.Enums.ServiceTypes serviceType = Common.Enums.ServiceTypes.NONE, long interviewerId = 0, Common.Enums.StudyStatus studyStatus = Common.Enums.StudyStatus.NONE, long clientId = 0, long candidateId = 0, StudyProgressStatus studyProgressStatus = StudyProgressStatus.NONE, int splitBy = 10);
        Task<GenericResponse<Study>> UpdateStudy(Study request);
        Task<GenericResponse<Study>> DeleteStudy(long Id, bool hardDelete);
        Task<GenericResponse<WorkStudy>> CreateWorkStudy(WorkStudy request);
        Task<GenericResponse<SocioeconomicStudy>> CreateSocuoeconomicStudy(SocioeconomicStudy request);
        Task<GenericResponse<FieldsToFill>> CreateFieldsToFill(FieldsToFill request);


        //Notes
        Task<GenericResponse<StudyNote>> CreateNote(StudyNote request);
        Task<GenericResponse<StudyNote>> UpdateNote(StudyNote request);
        Task<GenericResponse<StudyNote>> DeleteNote(long Id);
        Task<GenericResponse<List<StudyNote>>> GetNotes(List<long> Id, string key = "", long studyId = 0, int currentPage = 0, int offset = 10);
    }
}
