using Dapper;
using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.DAL.Repositories
{
    public class StudyRepository : BaseRepository, IStudyRepository
    {
        private readonly IAccountRepository _accountRepository;
        public StudyRepository(IAccountRepository _accountRepository, IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) {
            this._accountRepository=_accountRepository;
        }

        public async Task<GenericResponse<FieldsToFill>> CreateFieldsToFill(FieldsToFill request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<FieldsToFill>($@"
                        INSERT INTO FieldsToFill (
	                        StudyId,
	                        Resume,
	                        GeneralInformation,
	                        RecommendationLetter,
	                        IdentificationCardPics,
	                        EducationalLevel,
	                        Extracurricular,
	                        ScholarVerification,
	                        Family,
	                        EconomicSituation,
	                        Social,
	                        WorkHistory,
	                        IMSSValidation,
	                        PersonalReferences,
	                        Pictures)
                        OUTPUT INSERTED.*
                        VALUES (
	                        {request.StudyId},	                        
	                        {(request.Resume ? 1 : 0)},
	                        {(request.GeneralInformation ? 1 : 0)},
	                        {(request.RecommendationLetter ? 1 : 0)},
	                        {(request.IdentificationCardPics ? 1 : 0)},
	                        {(request.EducationalLevel ? 1 : 0)},
	                        {(request.Extracurricular ? 1 : 0)},
	                        {(request.ScholarVerification ? 1 : 0)},
	                        {(request.Family ? 1 : 0)},
	                        {(request.EconomicSituation ? 1 : 0)},
	                        {(request.Social ? 1 : 0)},
	                        {(request.WorkHistory ? 1 : 0)},
	                        {(request.IMSSValidation ? 1 : 0)},
	                        {(request.PersonalReferences ? 1 : 0)},
	                        {(request.Pictures ? 1 : 0)}
                        )
                    ");

                    return new GenericResponse<FieldsToFill>()
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
                return new GenericResponse<FieldsToFill>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<SocioeconomicStudy>> CreateSocuoeconomicStudy(SocioeconomicStudy request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<SocioeconomicStudy>($@"
                        INSERT INTO SocioeconomicStudy (
	                        	StudyId,
	                            IdentificationCard,
	                            AddressProof,
	                            BirthCertificate,
	                            CURP,
	                            StudiesProof,
	                            SocialSecurityNumber)
                        OUTPUT INSERTED.*
                        VALUES (
	                        {request.StudyId},	                        
	                        {(request.IdentificationCard ? 1 : 0)},
	                        {(request.AddressProof ? 1 : 0)},
	                        {(request.BirthCertificate ? 1 : 0)},
	                        {(request.CURP ? 1 : 0)},
	                        {(request.StudiesProof ? 1 : 0)},
	                        {(request.SocialSecurityNumber ? 1 : 0)}
                        )
                    ");

                    return new GenericResponse<SocioeconomicStudy>()
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
                return new GenericResponse<SocioeconomicStudy>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Study>> CreateStudy(Study request)
        {
            try
            {
                var genericError = new GenericResponse<Study>();
                var admin = await _accountRepository.GetAdminForCurrentUser();
                if (!admin.Sucess)
                {
                    genericError.ErrorMessage = admin.ErrorMessage;
                    return genericError;
                }
                string reviewer = request.Interviewer != null ? "InterviewerId" : "AnalystId";
                long reviewerId = request.Interviewer != null ? request.Interviewer.Id : request.Analyst.Id;
                using (var conn = Connection)
                {
                    conn.Open();
                    string query = $@"
                        INSERT INTO Study (
	                        CandidateId,
	                        ServiceTypeId,
	                        {reviewer},
                            StudyStatusId,
                            UnderAdminUserId,
                            StudyProgressStatusId)
                        OUTPUT INSERTED.*
                        VALUES (
	                        {request.Candidate.Id},
	                        {(int)request.ServiceType},
	                        {reviewerId},
                            {(long)request.StudyStatus},
                            {admin.Response.AdminId},
                            {(int)request.StudyProgressStatus})
                    ";
                    var apiResponse = await conn.QuerySingleAsync<Study>(query);

                    return new GenericResponse<Study>()
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
                return new GenericResponse<Study>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<WorkStudy>> CreateWorkStudy(WorkStudy request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<WorkStudy>($@"
                        INSERT INTO WorkStudy (
	                        StudyId,
	                        IdentificationCard,
	                        AddressProof,
	                        BirthCertificate,
	                        CURP,
	                        StudiesProof,
	                        SocialSecurityNumber,
	                        RFC,
	                        MilitaryLetter,
	                        CriminalRecordLetter
	                        )
                        OUTPUT INSERTED.*
                        VALUES (
                        {request.StudyId},
                        {(request.IdentificationCard?1:0)},
                        {(request.AddressProof ? "1":"0")},
                        {(request.BirthCertificate ? 1:0)},
                        {(request.CURP ? 1:0)},
                        {(request.StudiesProof ? 1:0)},
                        {(request.SocialSecurityNumber ? 1:0)},
                        {(request.RFC ? 1:0)},
                        {(request.MilitaryLetter ? 1:0)},
                        {(request.CriminalRecordLetter ? 1 : 0)})
                    ");

                    return new GenericResponse<WorkStudy>()
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
                return new GenericResponse<WorkStudy>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Study>> DeleteStudy(long Id, bool hardDelete)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Study>($@"
                        DECLARE @StudyID bigint = {Id}
                        DECLARE @HardDelete bit = {(hardDelete?1:0)}

                        IF @HardDelete = 1
	                        BEGIN
		                        DELETE FROM SocioeconomicStudy WHERE StudyId = @StudyID
		                        DELETE FROM FieldsToFill WHERE StudyId = @StudyID
		                        DELETE FROM WorkStudy WHERE StudyId = @StudyID
		                        DELETE FROM Study OUTPUT DELETED.* WHERE Id = @StudyID
	                        END

                        ELSE
	                        BEGIN
		                        UPDATE Study SET DeletedAt = GETUTCDATE() OUTPUT INSERTED.* WHERE Id = @StudyID
	                        END
                    ");

                    return new GenericResponse<Study>()
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
                return new GenericResponse<Study>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<Study>>> GetStudy(
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
            )
        {
            //By ID
            string idFilter = string.Empty;
            if (Id!=null && Id.Count>0)
            {
                Id.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(idFilter)) idFilter+=", ";
                    idFilter+=e;
                });
                idFilter=string.Format("s.Id IN ({0})", idFilter);
            }
            else
                idFilter = "1 = 1";

            string idAnalystFilter = string.Empty;

            if (AnalystId != 0 && bringStudiesOnlyOwn)
            {
                idAnalystFilter = string.Format(" AND s.AnalystId = {0}", AnalystId);
            }
            else if (AnalystId != 0 && !bringStudiesOnlyOwn)
            {
                idAnalystFilter = string.Format(" AND s.AnalystId <> {0}", AnalystId);
            }

            string bringStudiesOnlyFilter = string.Empty;
            if (bringStudiesOnly == 0 ) // All Cases
            {
                bringStudiesOnlyFilter = string.Empty;
            } else if (bringStudiesOnly == 1 ) // Only Analyst
            {
                idAnalystFilter = " AND s.InterviewerId is null";
            } else if (bringStudiesOnly == 2) // Only Interview
            {
                idAnalystFilter = " AND s.AnalystId is null";
            }


            string dateTimeFilter = string.Empty;
            if (startDate!=null&&endDate!=null&&startDate!=DateTime.MinValue&&endDate!=DateTime.MinValue)
            {
                dateTimeFilter = $@"AND (s.CreatedAt > '{startDate.ToString()}' AND s.CreatedAt < '{endDate.ToString()}')";
            }

            string serviceTypeFilter = string.Empty;
            if(serviceType!= Common.Enums.ServiceTypes.NONE)
            {
                serviceTypeFilter = $@" AND (s.ServiceTypeId = {(int)serviceType})";
            }

            string interviewerFilter = string.Empty;
            if(interviewerId != 0 && bringStudiesOnlyOwn)
            {
                interviewerFilter = $@" AND (s.InterviewerId = {interviewerId})";
            } else if (interviewerId != 0 && !bringStudiesOnlyOwn)
            {
                interviewerFilter = $@" AND (s.InterviewerId <> {interviewerId})";
            }

            string studyStatusFilter = string.Empty;
            if(studyStatus != Common.Enums.StudyStatus.NONE)
            {
                studyStatusFilter = $@"AND (s.StudyStatusId = {(int)studyStatus})";
            }

            string clientFilter = string.Empty;
            if (clientId!=0)
            {
                clientFilter =
                $@"
	                AND s.CandidateId IN (
                        SELECT can.Id FROM Candidate can
	                    INNER JOIN [User] cli
		                    ON cli.Id = can.ClientId
		                   -- AND cli.Id = {clientId} // Esta mal el enfoque de usuairo logeado VS e ID del cliente
                            AND can.DeletedAt IS NULL
                        -- ORDER BY can.Id
						-- OFFSET {currentPage * offset} ROWS 
                        -- FETCH NEXT {offset} ROWS ONLY
                    )
                ";
            }


            string candidateFilter = string.Empty;
            if (candidateId!=0)
            {
                candidateFilter =
                    $@"
	                AND (s.CandidateId = {candidateId})
                ";
            }


            string studyProgressStatusFilter = string.Empty;
            if(studyProgressStatus!= StudyProgressStatus.NONE)
                studyProgressStatusFilter = $@"AND (s.StudyProgressStatusId = {(int)studyProgressStatus})";


            var genericError = new GenericResponse<List<Study>>();
            var admin = await _accountRepository.GetAdminForCurrentUser();
            if (!admin.Sucess)
            {
                genericError.ErrorMessage = admin.ErrorMessage;
                return genericError;
            }

            try
            {
                string query = $@"
                        SELECT 
	                        s.*,
	                        s.ServiceTypeId as ServiceType,
	                        s.StudyStatusId as StudyStatus,
                            s.StudyProgressStatusId as StudyProgressStatus,
	                        ftf.*,
	                        ws.*,
	                        ss.*,
                            sf.*,
	                        (SELECT c.*, c.CandidateStatusId as CandidateStatus FROM Candidate c WHERE c.Id = s.CandidateId FOR JSON AUTO) cCandidate,
	                        (SELECT* FROM [User] u WHERE u.Id = s.InterviewerId FOR JSON AUTO) iUser,
	                        (SELECT* FROM [User] u WHERE u.Id = s.AnalystId FOR JSON AUTO) aUser
                        FROM Study s
                        LEFT JOIN FieldsToFill ftf
	                        ON ftf.StudyId = s.Id
                        LEFT JOIN WorkStudy ws
	                        ON ws.StudyId = s.Id
                        LEFT JOIN  SocioeconomicStudy ss
	                        ON ss.StudyId = s.Id
                        LEFT JOIN  StudyFinalResults sf
	                        ON sf.StudyId = s.Id
                        WHERE 
                            {idFilter}
                            {idAnalystFilter}
                            {dateTimeFilter}
                            {serviceTypeFilter}
                            {interviewerFilter}
                            {studyStatusFilter}
                            {clientFilter}
                            {candidateFilter}
                            {(admin.Response.FullSearch ? "" : $@"AND s.UnderAdminUserId = {admin.Response.AdminId}")}
                            {studyProgressStatusFilter}
                        ORDER BY s.Id
						OFFSET {currentPage * offset} ROWS 
                        FETCH NEXT {offset} ROWS ONLY
                    ";
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<Study>(query, new[]{
                        typeof(Study), //0
                        typeof(FieldsToFill), 
                        typeof(WorkStudy), //2
                        typeof(SocioeconomicStudy),
                        typeof(StudyFinalResult),
                        typeof(string), //4
                        typeof(string),
                        typeof(string), //6
                    }, (objects)=>
                    {
                        Console.WriteLine(objects);
                        var currentStudy = objects[0] as Study;
                        if (currentStudy==null) return null;
                        
                        currentStudy.FieldsToFill = objects[1] as FieldsToFill;

                        if (objects[2] as WorkStudy != null)
                            currentStudy.WorkStudy = objects[2] as WorkStudy;

                        if (objects[3] as SocioeconomicStudy != null)
                            currentStudy.SocioeconomicStudy = objects[3] as SocioeconomicStudy;

                        if (objects[4] as StudyFinalResult != null)
                            currentStudy.StudyFinalResult = objects[4] as StudyFinalResult;

                        if (!string.IsNullOrWhiteSpace(objects[5]?.ToString()))
                        {
                            var candidate = JsonConvert.DeserializeObject<List<Candidate>>(objects[5].ToString());
                            currentStudy.Candidate = candidate.FirstOrDefault();
                        }

                        if (!string.IsNullOrWhiteSpace(objects[6]?.ToString()))
                            currentStudy.Interviewer = JsonConvert.DeserializeObject<List<Identity>>(objects[6].ToString())?.FirstOrDefault();


                        if (!string.IsNullOrWhiteSpace(objects[7]?.ToString()))
                            currentStudy.Analyst = JsonConvert.DeserializeObject<List<Identity>>(objects[7].ToString())?.FirstOrDefault();


                        return currentStudy;
                    }, splitOn: "Id, Id, Id, Id, cCandidate, iUser, aUser");

                    return new GenericResponse<List<Study>>()
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
                return new GenericResponse<List<Study>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<int>> Pagination(
            List<long> Id, 
            DateTime? startDate,
            DateTime? endDate,
            ServiceTypes serviceType = ServiceTypes.NONE,
            long interviewerId = 0,
            StudyStatus studyStatus = StudyStatus.NONE,
            long clientId = 0,
            long candidateId = 0,
            StudyProgressStatus studyProgressStatus = StudyProgressStatus.NONE,
            int splitBy = 10
         )
        {            //By ID
            string idFilter = string.Empty;
            if (Id!=null && Id.Count>0)
            {
                Id.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(idFilter)) idFilter+=", ";
                    idFilter+=e;
                });
                idFilter=string.Format("s.Id IN ({0})", idFilter);
            }
            else
                idFilter = "1 = 1";

            string dateTimeFilter = string.Empty;
            if (startDate!=null&&endDate!=null&&startDate!=DateTime.MinValue&&endDate!=DateTime.MinValue)
            {
                dateTimeFilter = $@"AND (s.CreatedAt > '{startDate.ToString()}' AND s.CreatedAt < '{endDate.ToString()}')";
            }

            string serviceTypeFilter = string.Empty;
            if (serviceType!= Common.Enums.ServiceTypes.NONE)
            {
                serviceTypeFilter = $@" AND (s.ServiceTypeId = {(int)serviceType})";
            }

            string interviewerFilter = string.Empty;
            if (interviewerId != 0)
            {
                interviewerFilter = $@" AND (s.InterviewerId = {interviewerId})";
            }

            string studyStatusFilter = string.Empty;
            if (studyStatus != Common.Enums.StudyStatus.NONE)
            {
                studyStatusFilter = $@"AND (s.StudyStatusId = {(int)studyStatus})";
            }

            string clientFilter = string.Empty;
            if (clientId!=0)
            {
                clientFilter =
                $@"
	                AND s.CandidateId IN (
                        SELECT can.Id FROM Candidate can
	                    INNER JOIN [User] cli
		                    ON cli.Id = can.ClientId
		                   -- AND cli.Id = {clientId} -- Mal filtrado por Usuario que inicia sesión VS Propietario de empresa
                    )
                ";
            }


            string candidateFilter = string.Empty;
            if (candidateId!=0)
            {
                candidateFilter =
                    $@"
	                AND (s.CandidateId = {candidateId})
                ";
            }


            string studyProgressStatusFilter = string.Empty;
            if (studyProgressStatus!= StudyProgressStatus.NONE)
                studyProgressStatusFilter = $@"AND (s.StudyProgressStatusId = {(int)studyProgressStatus})";


            var genericError = new GenericResponse<int>();
            var admin = await _accountRepository.GetAdminForCurrentUser();
            if (!admin.Sucess)
            {
                genericError.ErrorMessage = admin.ErrorMessage;
                return genericError;
            }

            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<int>($@"
                        SELECT 
	                        COUNT(s.Id)
                        FROM Study s
                        LEFT JOIN FieldsToFill ftf
	                        ON ftf.StudyId = s.Id
                        LEFT JOIN WorkStudy ws
	                        ON ws.StudyId = s.Id
                        LEFT JOIN  SocioeconomicStudy ss
	                        ON ss.StudyId = s.Id
                        WHERE 
                            {idFilter}
                            {dateTimeFilter}
                            {serviceTypeFilter}
                            {interviewerFilter}
                            {studyStatusFilter}
                            {clientFilter}
                            {candidateFilter}
                            {studyProgressStatusFilter}
                            {(admin.Response.FullSearch ? "" : $@"AND s.UnderAdminUserId = {admin.Response.AdminId}")}
                    ");

                    var extra = (float)apiResponse%(float)splitBy;
                    return new GenericResponse<int>()
                    {
                        Sucess = true,
                        //Response = (apiResponse/splitBy) + (extra!=0 ? 1 : 0),
                        Response = ((apiResponse) / splitBy),
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

        public async Task<GenericResponse<Study>> UpdateStudy(Study request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    string query = $@"
                        UPDATE Study SET 
                         StudyStatusId = {(int)request.StudyStatus},                         
                         StudyProgressStatusId = {(int)request.StudyProgressStatus},
                         UpdatedAt = '{DateTime.UtcNow}'
                         {(request.Analyst!=null&&request.Analyst.Id>0? $@",AnalystId = {request.Analyst.Id}":"" )}
                         {(request.Interviewer != null && request.Interviewer.Id > 0 ? $@",InterviewerId = {request.Interviewer.Id}" : "")}
                        OUTPUT INSERTED.*
                        WHERE 
                         Id = {request.Id}
                    ";

                    var apiResponse = await conn.QuerySingleAsync<Study>(query);

                    return new GenericResponse<Study>()
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
                return new GenericResponse<Study>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }


        // Notes
        public async Task<GenericResponse<StudyNote>> CreateNote(StudyNote request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyNote>($@"
                        INSERT INTO StudyNote (
	                        Description,
	                        StudyId,
                            CreatedAt)
                        OUTPUT INSERTED.*
                        VALUES (
	                        '{request.Description}',
	                        {request.StudyId},
                            '{DateTime.UtcNow}')
                        ");

                    return new GenericResponse<StudyNote>()
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
                return new GenericResponse<StudyNote>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyNote>> UpdateNote(StudyNote request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyNote>($@"
                            UPDATE StudyNote
                            SET
	                            Description = '{request.Description}'
                            OUTPUT INSERTED.*
                            WHERE 
	                            Id = {request.Id}
                        ");

                    return new GenericResponse<StudyNote>()
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
                return new GenericResponse<StudyNote>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyNote>> DeleteNote(long Id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyNote>($@"
                            UPDATE StudyNote
                            SET
	                            DeletedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE 
	                            Id = {Id}
                        ");

                    return new GenericResponse<StudyNote>()
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
                return new GenericResponse<StudyNote>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<StudyNote>>> GetNotes(List<long> Id, string key = "", long studyId = 0, int currentPage = 0, int offset = 10)
        {
            try
            {
                string idFilter = string.Empty;
                if (Id!=null && Id.Count>0)
                {
                    Id.ForEach(e =>
                    {
                        if (!string.IsNullOrWhiteSpace(idFilter)) idFilter+=", ";
                        idFilter+=e;
                    });
                    idFilter=string.Format("Id IN ({0})", idFilter);
                }
                else
                    idFilter = "1 = 1";

                string keyFilter = string.Empty;
                if (!string.IsNullOrWhiteSpace(key))
                    keyFilter = $@" AND ( Description LIKE ('%{key}%') )";

                string candidateIdFilter = string.Empty;
                if (studyId!=0)
                    candidateIdFilter = $@" AND (StudyId = {studyId})";



                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<StudyNote>($@"
                            SELECT* FROM StudyNote
                            WHERE
                                {idFilter}
                                {keyFilter}
                                {candidateIdFilter}
                                AND DeletedAt IS NULL
                            ORDER BY Id
						    OFFSET {currentPage * offset} ROWS 
                            FETCH NEXT {offset} ROWS ONLY
                        ");

                    return new GenericResponse<List<StudyNote>>()
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
                return new GenericResponse<List<StudyNote>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
