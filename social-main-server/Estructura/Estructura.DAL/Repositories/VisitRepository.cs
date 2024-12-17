using Dapper;
using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.DAL.Repositories
{
    public class VisitRepository:BaseRepository, IVisitRepository
    {
        private readonly IAccountRepository _accountRepository;
        public VisitRepository(IAccountRepository _accountRepository, IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor)
        {
            this._accountRepository=_accountRepository;
        }

        public async Task<GenericResponse<Visit>> CreateVisit(Visit request)
        {
            try
            {
                var admin = await _accountRepository.GetAdminForCurrentUser();
                if (!admin.Sucess)
                {
                    return new GenericResponse<Visit>()
                    {
                        ErrorMessage = admin.ErrorMessage,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                    };
                }
                using (var conn = Connection)
                {
                    conn.Open();
                    string query = $@"
                        INSERT INTO ScheduledVisits (
	                        StudyId,
	                        VisitDate,
	                        ConfirmAssistance,
	                        VisitStatusId,
	                        CityId,
	                        StateId,
	                        Address,
	                        Observations,
	                        CreatedAt,
	                        UpdatedAt,
                            UnderAdminUserId,
                            NotationColor)
                        OUTPUT INSERTED.*
                        VALUES (
	                        {request.Study.Id},
	                        '{request.VisitDate.ToString()}',
	                        0,
	                        {(int)Common.Enums.VisitStatus.VISIT_NOT_STARTED},
	                        {request.City.Id},
	                        {request.State.Id},
	                        '{request.Address}',
	                        '{request.Observations}',
	                        '{DateTime.UtcNow}',
	                        '{DateTime.UtcNow}',
                            {admin.Response.AdminId},
                            '{request.NotationColor}'
                        )";
                    var apiResponse = await conn.QuerySingleAsync<Visit>(query);

                    return new GenericResponse<Visit>()
                    {
                        Sucess = true,
                        Response = apiResponse,
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return new GenericResponse<Visit>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<Visit>>>GetVisit(List<long>Id, long? interviewerId, long? candidateId, long? clientId, long? studyId, DateTime? startDate, DateTime? endDate, int? cityId, int? stateId, VisitStatus visitStatus = VisitStatus.NONE, int currentPage = 0, int offset = 10)
        {
            string idFilter = string.Empty;
            if (Id!=null && Id.Count>0)
            {
                Id.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(idFilter)) idFilter+=", ";
                    idFilter+=e;
                });
                idFilter=string.Format("sv.StudyId IN ({0})", idFilter);
            }
            else
                idFilter = "1 = 1";

            string interviewerFilter = string.Empty;
            if (interviewerId !=null&& interviewerId !=0)
                interviewerFilter = $@"
                    INNER JOIN Study s
                    ON (
	                    s.Id = sv.StudyId 
                        AND s.InterviewerId = {interviewerId}
                    )";

            string candidateFilter = string.Empty;
            if (candidateId!=null&&candidateId!=0)
                candidateFilter = $@"
                    INNER JOIN Study s2
                    ON (
	                    s2.Id = sv.StudyId
	                    AND s2.CandidateId = {candidateId}
                    )";

            string clientFilter = string.Empty;
            if (clientId!=null&&clientId!=0)
                clientFilter = $@"
                    INNER JOIN Study s3
                    ON 
	                    s3.Id = sv.StudyId
                    INNER JOIN [Candidate] can
                    ON (
	                    can.Id = s3.CandidateId
	                    AND can.ClientId = {clientId}
                    )";

            string studyFilter = string.Empty;
            if (studyId!=null&&studyId!=0)
                studyFilter = $@" AND sv.StudyId = {studyId} ";

            string dateFilter = string.Empty;
            if(startDate!=null&&endDate!=null&&startDate!=DateTime.MinValue&&endDate!=DateTime.MinValue)
                dateFilter = $@" AND (sv.CreatedAt > '{startDate}' AND sv.CreatedAt < '{endDate}')";

            string cityFilter = string.Empty;
            if (cityId!=null&&cityId!=0)
                cityFilter = $@" AND sv.CityId = {cityId}";

            string stateFilter = string.Empty;
            if (stateId!=null&&stateId!=0)
                stateFilter = $@" AND sv.StateId = {stateId}";

            string visitStatusFilter = string.Empty;
            if (visitStatus != VisitStatus.NONE )
                visitStatusFilter = $@" AND sv.VisitStatusId = {(int)visitStatus}";

            try
            {
                var admin = await _accountRepository.GetAdminForCurrentUser();
                if (!admin.Sucess)
                {
                    return new GenericResponse<List<Visit>>()
                    {
                        ErrorMessage = admin.ErrorMessage,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                    };
                }
                
                string query = $@"
                        SELECT 
                            sv.*,
                            sv.StudyId as stid,
                            sv.VisitStatusId as VisitStatus,
                            ci.*,
                            sta.*,
                            m.Id,
                            sv.id as ScheduledVisitsId
                        FROM ScheduledVisits sv
                        LEFT JOIN Cities ci
                        ON 
                            ci.Id = sv.CityId
                        LEFT JOIN States sta
                        ON  
                            sta.Id = sv.StateId
                        LEFT JOIN Media m 
                        ON
                            m.Id = sv.MediaId
                        {interviewerFilter}
                        {candidateFilter}
                        {clientFilter}
                        WHERE 
                        {idFilter}
                        AND sv.DeletedAt IS NULL 
                        {studyFilter}
                        {dateFilter}
                        {cityFilter}
                        {stateFilter}
                        {visitStatusFilter}
                        ORDER BY sv.id DESC
						OFFSET {currentPage * offset} ROWS 
                        FETCH NEXT {offset} ROWS ONLY
                    ";

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<Visit>(query, 
                    new[] { 
                        typeof(Visit),
                        typeof(long),
                        typeof(int),
                        typeof(City),
                        typeof(State),
                        typeof(long?),
                        typeof(long?),
                    }, (objects)=>
                    {
                        var current = objects[0] as Visit;
                        if (current == null) return null;
                        int intDeseado = int.Parse(objects[1].ToString());
                        long longDeseado = long.Parse(objects[1].ToString());
                        current.Id = intDeseado;
                        current.Study = new Study()
                        {
                            Id = longDeseado
                        };

                        current.VisitStatus = (VisitStatus)objects[2];

                        if (objects[3] as City != null)
                            current.City = objects[3] as City;

                        if (objects[4] as State != null)
                            current.State = objects[4] as State;

                        long mediaId = 0;
                        if (objects[5] != null && long.TryParse(objects[5].ToString(), out mediaId))
                            current.Evidence = new Media() { Id = mediaId };
                        else
                            current.Evidence = new Media();

                        current.Id = long.Parse(objects[6].ToString());

                        return current;
                    }, splitOn: "Id, stid, VisitStatus, Id, Id, Id, ScheduledVisitsId");

                    return new GenericResponse<List<Visit>>()
                    {
                        Sucess = true,
                        Response = apiResponse.ToList(),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return new GenericResponse<List<Visit>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }

        }

        public async Task<GenericResponse<Visit>> UpdateVisit(Visit request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var query = $@"
                        UPDATE ScheduledVisits 
                        SET
	                        VisitDate = '{request.VisitDate}',
	                        ConfirmAssistance = {(request.ConfirmAssistance ? 1 : 0)},
	                        VisitStatusId = {(int)request.VisitStatus},
	                        CityId = {request.City.Id},
	                        StateId = {request.State.Id},
	                        Address = '{request.Address}',
	                        Observations = '{request.Observations}',
	                        UpdatedAt = '{DateTime.UtcNow}',
	                        NotationColor = '{request.NotationColor}'
                            {(request.Evidence != null && request.Evidence.Id != 0 ? $@", MediaId = {request.Evidence.Id}" : "")}
                        WHERE
	                        StudyId = {request.Id}
                    ";
                    var apiResponse = await conn.QueryAsync<Visit>(query);

                    return new GenericResponse<Visit>()
                    {
                        Sucess = true,
                        Response = apiResponse.First(),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return new GenericResponse<Visit>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    
        public async Task<GenericResponse<Visit>> DeleteVisit(long Id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var query = $@"
                        UPDATE ScheduledVisits 
                        SET
	                        DeletedAt = '{DateTime.UtcNow}'
                        WHERE
	                        StudyId = {Id}
                    ";
                    var apiResponse = await conn.QuerySingleAsync<Visit>(query);

                    return new GenericResponse<Visit>()
                    {
                        Sucess = true,
                        Response = apiResponse,
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return new GenericResponse<Visit>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
   
        public async Task<GenericResponse<int>>Pagination(List<long> Id, long? interviewerId, long? candidateId, long? clientId, long? studyId, DateTime? startDate, DateTime? endDate, int? cityId, int? stateId, VisitStatus visitStatus = VisitStatus.NONE, int splitBy = 10)
        {
            string idFilter = string.Empty;
            if (Id!=null && Id.Count>0)
            {
                Id.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(idFilter)) idFilter+=", ";
                    idFilter+=e;
                });
                idFilter=string.Format("sv.StudyId IN ({0})", idFilter);
            }
            else
                idFilter = "1 = 1";

            string interviewerFilter = string.Empty;
            if (interviewerId !=null&& interviewerId !=0)
                interviewerFilter = $@"
                    INNER JOIN Study s
                    ON (
	                    s.Id = sv.StudyId 
                        AND s.InterviewerId = {interviewerId}
                    )";

            string candidateFilter = string.Empty;
            if (candidateId!=null&&candidateId!=0)
                candidateFilter = $@"
                    INNER JOIN Study s2
                    ON (
	                    s2.Id = sv.StudyId
	                    AND s2.CandidateId = {candidateId}
                    )";

            string clientFilter = string.Empty;
            if (clientId!=null&&clientId!=0)
                clientFilter = $@"
                    INNER JOIN Study s3
                    ON 
	                    s3.Id = sv.StudyId
                    INNER JOIN [Candidate] can
                    ON (
	                    can.Id = s3.CandidateId
	                    AND can.ClientId = {clientId}
                    )";

            string studyFilter = string.Empty;
            if (studyId!=null&&studyId!=0)
                studyFilter = $@" AND sv.StudyId = {studyId} ";

            string dateFilter = string.Empty;
            if (startDate!=null&&endDate!=null&&startDate!=DateTime.MinValue&&endDate!=DateTime.MinValue)
                dateFilter = $@" AND (sv.CreatedAt > '{startDate}' AND sv.CreatedAt < '{endDate}')";

            string cityFilter = string.Empty;
            if (cityId!=null&&cityId!=0)
                cityFilter = $@" AND sv.CityId = {cityId}";

            string stateFilter = string.Empty;
            if (stateId!=null&&stateId!=0)
                stateFilter = $@" AND sv.StateId = {stateId}";

            string visitStatusFilter = string.Empty;
            if (visitStatus != VisitStatus.NONE)
                visitStatusFilter = $@" AND sv.VisitStatusId = {(int)visitStatus}";

            try
            {
                var admin = await _accountRepository.GetAdminForCurrentUser();
                if (!admin.Sucess)
                {
                    return new GenericResponse<int>()
                    {
                        ErrorMessage = admin.ErrorMessage,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                    };
                }

                string query = $@"
                        SELECT 
                            COUNT(sv.StudyId) AS TotalRows
                        FROM ScheduledVisits sv
                        LEFT JOIN Cities ci
                        ON 
                            ci.Id = sv.CityId
                        LEFT JOIN States sta
                        ON  
                            sta.Id = sv.StateId
                        LEFT JOIN Media m 
                        ON
                            m.Id = sv.MediaId
                        {interviewerFilter}
                        {candidateFilter}
                        {clientFilter}
                        WHERE 
                        {idFilter}
                        AND sv.DeletedAt IS NULL 
                        {studyFilter}
                        {dateFilter}
                        {cityFilter}
                        {stateFilter}
                        {visitStatusFilter}
                    ";

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<int>(query);

                    var extra = (float)apiResponse%(float)splitBy;
                    return new GenericResponse<int>()
                    {
                        Sucess = true,
                        Response = (apiResponse/splitBy) + (extra!=0 ? 1 : 0),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return new GenericResponse<int>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }

        }
    }
}
