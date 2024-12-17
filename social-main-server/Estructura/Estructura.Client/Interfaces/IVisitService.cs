using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IVisitService
    {
        Task<GenericResponse<Visit>> CreateVisit(Visit request);
        Task<GenericResponse<List<Visit>>> GetVisit(List<long> Id, long? interviewerId, long? candidateId, long? clientId, long? studyId, DateTime? startDate, DateTime? endDate, int? cityId, int? stateId, VisitStatus visitStatus = VisitStatus.NONE, int currentPage = 0, int offset = 10);
        Task<GenericResponse<Visit>> UpdateVisit(Visit request);
        Task<GenericResponse<Visit>> DeleteVisit(long Id);
        Task<GenericResponse<int>> Pagination(List<long> Id, long? interviewerId, long? candidateId, long? clientId, long? studyId, DateTime? startDate, DateTime? endDate, int? cityId, int? stateId, VisitStatus visitStatus = VisitStatus.NONE, int splitBy = 10);
    }
}
