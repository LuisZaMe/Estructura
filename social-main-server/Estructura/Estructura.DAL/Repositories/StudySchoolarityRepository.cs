using Dapper;
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
    public class StudySchoolarityRepository : BaseRepository, IStudySchoolarityRepository
    {
        public StudySchoolarityRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor)
        {

        }

        // Study scholarity
        public async Task<GenericResponse<StudySchoolarity>> CreateStudySchoolarity(StudySchoolarity request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudySchoolarity>($@"
                                INSERT INTO StudySchoolarity (
	                                StudyId,
	                                ScholarVerificationSummary,
	                                ScholarVerificationMediaId)
                                OUTPUT 
                                    INSERTED.*
                                VALUES (
	                                {request.StudyId},
	                                '{request.ScholarVerificationSummary}',
	                                {(request.ScholarVerificationMedia == null || request.ScholarVerificationMedia.Id == 0 ? "null" :  request.ScholarVerificationMedia.Id.ToString())})");

                    return new GenericResponse<StudySchoolarity>()
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
                return new GenericResponse<StudySchoolarity>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudySchoolarity>> DeleteStudySchoolarity(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudySchoolarity>($@"
                                UPDATE StudySchoolarity 
                                SET
	                                DeletedAt = '{DateTime.UtcNow}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE
                                    Id = {id}");

                    return new GenericResponse<StudySchoolarity>()
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
                return new GenericResponse<StudySchoolarity>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<StudySchoolarity>>> GetStudySchoolarity(List<long> id, bool byStudy = false)
        {
            try
            {
                string ids = string.Empty;
                id.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(ids)) ids+=", ";
                    ids+=e;
                });

                string query = $@"
                                SELECT
                                    ss.*,
                                    m.*,
                                (
                                    SELECT
                                        s.*,
                                        s.SchoolarLevelId as SchoolarLevel
                                    FROM Scholarity s
                                    WHERE 
                                        s.StudySchoolarityId = ss.Id
                                    AND
                                        s.DeletedAt IS NULL
                                    FOR JSON PATH) AS ScholarityJson,
                                (
                                    SELECT 
                                        ea.*,
                                        ea.KnowledgeLevelId as KnowledgeLevel
                                    FROM
                                        ExtracurricularActivities ea
                                    WHERE
                                        ea.DeletedAt IS NULL
                                    AND
                                        ea.StudySchoolarityId = ss.Id
                                    FOR JSON PATH
                                ) AS ExtracurricularActivitiesJson
                                FROM
                                    StudySchoolarity ss
                                LEFT JOIN Media m
                                ON
                                    ss.ScholarVerificationMediaId = m.Id
                                WHERE
                                    ss.DeletedAt IS NULL
                                AND
                                { (byStudy? $@"ss.StudyId IN ({ids})": $@"ss.Id IN ({ids})") }
                                    ";

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<StudySchoolarity>(query,
                                    new[]
                                    {
                                        typeof(StudySchoolarity),
                                        typeof(Media),
                                        typeof(string),
                                        typeof(string)
                                    }, (objects) =>
                                    {
                                        var current = objects[0] as StudySchoolarity;

                                        // media
                                        current.ScholarVerificationMedia = objects[1] as Media == null ? new Media() : objects[1] as Media;

                                        //Scholarity
                                        if (!string.IsNullOrWhiteSpace(objects[2] as string))
                                            current.ScholarityList = JsonConvert.DeserializeObject<List<Scholarity>>(objects[2] as string);

                                        //Extracurricular Activities
                                        if (!string.IsNullOrWhiteSpace(objects[3] as string))
                                            current.ExtracurricularActivitiesList = JsonConvert.DeserializeObject<List<ExtracurricularActivities>>(objects[3] as string);

                                        return current;
                                    },
                                    splitOn: "Id, Id, ScholarityJson, ExtracurricularActivitiesJson");

                    return new GenericResponse<List<StudySchoolarity>>()
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
                return new GenericResponse<List<StudySchoolarity>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudySchoolarity>> UpdateStudySchoolarity(StudySchoolarity request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudySchoolarity>($@"
                                UPDATE StudySchoolarity 
                                SET
	                                ScholarVerificationSummary = '{request.ScholarVerificationSummary}',
                                    ScholarVerificationMediaId = {(request.ScholarVerificationMedia==null||request.ScholarVerificationMedia.Id==0 ? "null" : request.ScholarVerificationMedia.Id.ToString())}
                                OUTPUT 
                                    INSERTED.*
                                WHERE Id = {request.Id}");

                    return new GenericResponse<StudySchoolarity>()
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
                return new GenericResponse<StudySchoolarity>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }


        //Extracurricular activity
        public async Task<GenericResponse<List<ExtracurricularActivities>>> CreateExtracurricularActivities(List<ExtracurricularActivities> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
                                    {e.StudySchoolarityId},
                                    '{e.Name}',
                                    '{e.Instituution}',
                                    {(int)e.KnowledgeLevel},
                                    '{e.Period}'
                                )";
                });

                string query = $@"
                                INSERT INTO ExtracurricularActivities (
	                                StudySchoolarityId,
	                                Name,
	                                Instituution,
                                    KnowledgeLevelId,
                                    Period)
                                OUTPUT 
                                    INSERTED.*,
                                    INSERTED.KnowledgeLevelId as KnowledgeLevel
                                VALUES {content} ";

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<ExtracurricularActivities>(query);

                    return new GenericResponse<List<ExtracurricularActivities>>()
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
                return new GenericResponse<List<ExtracurricularActivities>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<ExtracurricularActivities>> UpdateExtracurricularActivities(ExtracurricularActivities request)
        {
            try
            {
                string query = $@"
                                UPDATE ExtracurricularActivities
                                SET
	                                Name = '{request.Name}',
	                                Instituution = '{request.Instituution}',
                                    KnowledgeLevelId = {(int)request.KnowledgeLevel},
                                    Period = '{request.Period}',
                                    UpdatedAt = '{DateTime.UtcNow}'
                                OUTPUT 
                                    INSERTED.*,
                                    INSERTED.KnowledgeLevelId as KnowledgeLevel
                                WHERE Id = {request.Id}";
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<ExtracurricularActivities>(query);

                    return new GenericResponse<ExtracurricularActivities>()
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
                return new GenericResponse<ExtracurricularActivities>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<ExtracurricularActivities>>> GetExtracurricularActivities(List<long> id)
        {
            try
            {
                string ids = string.Empty;
                id.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(ids)) ids+=", ";
                    ids+=e;
                });
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<ExtracurricularActivities>($@"
                        SELECT 
                            ea.*,
                            ea.KnowledgeLevelId as KnowledgeLevel
                        FROM
                        ExtracurricularActivities ea
                        WHERE 
                            DeletedAt IS NULL
                        AND
                            Id IN ({ids})");

                    return new GenericResponse<List<ExtracurricularActivities>>()
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
                return new GenericResponse<List<ExtracurricularActivities>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<ExtracurricularActivities>> DeleteExtracurricularActivities(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<ExtracurricularActivities>($@"
                                UPDATE ExtracurricularActivities
                                SET
	                                DeletedAt = '{DateTime.UtcNow}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE Id = {id}");

                    return new GenericResponse<ExtracurricularActivities>()
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
                return new GenericResponse<ExtracurricularActivities>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }



        //Scholarity
        public async Task<GenericResponse<List<Scholarity>>> CreateSchoolarity(List<Scholarity> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
                                    {e.StudySchoolarityId},
                                    {(int)e.SchoolarLevel},
                                    '{e.Career}',
                                    '{e.Period}',
                                    '{e.Place}',
                                    '{e.TimeOnCareer}',
                                    '{e.Institution}',
                                    {e.DoccumentId}
                                )";
                });
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<Scholarity>($@"
                                INSERT INTO Scholarity (
	                                StudySchoolarityId,
	                                SchoolarLevelId,
	                                Career,
                                    Period,
                                    Place,
                                    TimeOnCareer,
                                    Institution,
                                    DoccumentId)
                                OUTPUT 
                                    INSERTED.*
                                VALUES {content} ");

                    return new GenericResponse<List<Scholarity>>()
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
                return new GenericResponse<List<Scholarity>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<Scholarity>>> GetSchoolarity(List<long> id)
        {
            try
            {
                string ids = string.Empty;
                id.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(ids)) ids+=", ";
                    ids+=e;
                });
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<Scholarity>($@"
                        SELECT 
                            s.*,
                            s.SchoolarLevelId as SchoolarLevel
                        FROM
                            Scholarity s
                        WHERE 
                            DeletedAt IS NULL
                        AND
                            Id IN ({ids})");

                    apiResponse.ToList().ForEach(e => e.Doccument = new Doccument() { Id = e.DoccumentId });

                    return new GenericResponse<List<Scholarity>>()
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
                return new GenericResponse<List<Scholarity>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Scholarity>> UpdateSchoolarity(Scholarity request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Scholarity>($@"
                                UPDATE Scholarity
                                SET
	                                SchoolarLevelId = {(int)request.SchoolarLevel},
	                                Career = '{request.Career}',
                                    Period = '{request.Period}',
                                    Place = '{request.Place}',
                                    TimeOnCareer = '{request.TimeOnCareer}',
                                    Institution = '{request.Institution}',
                                    DoccumentId = {request.Doccument.Id},
                                    UpdatedAt = '{DateTime.UtcNow}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE Id = {request.Id}");

                    return new GenericResponse<Scholarity>()
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
                return new GenericResponse<Scholarity>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Scholarity>> DeleteSchoolarity(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Scholarity>($@"
                                UPDATE Scholarity
                                SET
                                    DeletedAt = '{DateTime.UtcNow}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE Id = {id}");

                    return new GenericResponse<Scholarity>()
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
                return new GenericResponse<Scholarity>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
