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
    public class StudySocialRepository:BaseRepository, IStudySocialRepository
    {
        public StudySocialRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) { }


        // Study Social
        public async Task<GenericResponse<StudySocial>> CreateStudySocial(StudySocial request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudySocial>($@"
                                INSERT INTO StudySocial (
	                                StudyId,
	                                SocialArea,
	                                Hobbies,
	                                HealthInformation)
                                OUTPUT 
                                    INSERTED.*
                                VALUES (
	                                {request.StudyId},
	                                '{request.SocialArea}',
	                                '{request.Hobbies}',
	                                '{request.HealthInformation}')
                    ");

                    return new GenericResponse<StudySocial>()
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
                return new GenericResponse<StudySocial>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudySocial>> UpdateStudySocial(StudySocial request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudySocial>($@"
                                UPDATE StudySocial 
                                SET
	                                SocialArea = '{request.SocialArea}',
	                                Hobbies = '{request.Hobbies}',
	                                HealthInformation = '{request.HealthInformation}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE 
                                    DeletedAt IS NULL
                                AND
                                    Id = {request.Id}");

                    return new GenericResponse<StudySocial>()
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
                return new GenericResponse<StudySocial>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<StudySocial>>> GetStudySocial(List<long> id, bool byStudy = false)
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
                    var apiResponse = await conn.QueryAsync<StudySocial, string, StudySocial>($@"
                        SELECT
                            ss.*,
                            (
                                SELECT
                                    *
                                FROM 
                                    SocialGoals sg 
                                WHERE 
                                    DeletedAt IS NULL 
                                AND 
                                    sg.StudySocialId = ss.Id
                                FOR JSON PATH
                            ) AS SocialGoalsJSON
                        FROM StudySocial ss
                        WHERE 
                            DeletedAt IS NULL
                        AND
                            {(byStudy ? $@"ss.StudyId IN ({ids})" : $@"ss.Id IN ({ids})")}", (social, SocialGoalsJSON) =>
                        {
                            if (!string.IsNullOrWhiteSpace(SocialGoalsJSON))
                                social.SocialGoalsList = JsonConvert.DeserializeObject<List<SocialGoals>>(SocialGoalsJSON);
                            else
                                social.SocialGoalsList = new List<SocialGoals>();
                            return social;
                        }, splitOn: "Id, SocialGoalsJSON");

                    return new GenericResponse<List<StudySocial>>()
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
                return new GenericResponse<List<StudySocial>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudySocial>> DeleteStudySocial(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudySocial>($@"
                                UPDATE StudySocial 
                                SET
	                                DeletedAt = '{DateTime.UtcNow}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE
                                    Id = {id}");

                    return new GenericResponse<StudySocial>()
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
                return new GenericResponse<StudySocial>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }



        // Social Goals
        public async Task<GenericResponse<List<SocialGoals>>> CreateSocialGoals(List<SocialGoals> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
	                    {e.StudySocialId},
	                    '{e.CoreValue}',
	                    '{e.LifeGoal}',
	                    '{e.NextGoal}')";
                });
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<SocialGoals>($@"
                            INSERT INTO SocialGoals (
	                            StudySocialId,
	                            CoreValue,
	                            LifeGoal,
	                            NextGoal)
                            OUTPUT 
                                INSERTED.*
                            VALUES 
                                {content}
                    ");

                    return new GenericResponse<List<SocialGoals>>()
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
                return new GenericResponse<List<SocialGoals>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<SocialGoals>> UpdateSocialGoals(SocialGoals request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<SocialGoals>($@"
                                UPDATE SocialGoals 
                                SET
	                                CoreValue = '{request.CoreValue}',
	                                LifeGoal = '{request.LifeGoal}',
	                                NextGoal = '{request.NextGoal}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE
                                    Id = {request.Id}");

                    return new GenericResponse<SocialGoals>()
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
                return new GenericResponse<SocialGoals>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<SocialGoals>>> GetSocialGoals(List<long> id)
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
                    var apiResponse = await conn.QueryAsync<SocialGoals>($@"
                        SELECT
                            sg.*
                        FROM SocialGoals sg
                        WHERE 
                            DeletedAt IS NULL
                        AND
                            sg.Id IN ({ids})");

                    return new GenericResponse<List<SocialGoals>>()
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
                return new GenericResponse<List<SocialGoals>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<SocialGoals>> DeleteSocialGoals(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<SocialGoals>($@"
                                UPDATE SocialGoals 
                                SET
	                                DeletedAt = '{DateTime.UtcNow}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE
                                    Id = {id}");

                    return new GenericResponse<SocialGoals>()
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
                return new GenericResponse<SocialGoals>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
