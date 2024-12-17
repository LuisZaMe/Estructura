using Dapper;
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
    public class StudyPersonalReferenceRepository:BaseRepository, IStudyPersonalReferenceRepository
    {
        public StudyPersonalReferenceRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) { }

        public async Task<GenericResponse<List<StudyPersonalReference>>> CreateStudyPersonalReference(List<StudyPersonalReference> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
                                    {e.StudyId},
                                    '{e.ReferenceTitle}',
                                    '{e.Name}',
                                    '{e.CurrentJob}',
                                    '{e.Address}',
                                    '{e.Phone}',
                                    '{e.YearsKnowingEachOther}',
                                    '{e.KnowAddress}',
                                    '{e.YearsOnCurrentResidence}',
                                    '{e.KnowledgeAboutPreviousJobs}',
                                    '{e.OpinionAboutTheCandidate}',
                                    '{e.PoliticalActivity}',
                                    '{e.WouldYouRecommendIt}'
                                )";
                });
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<StudyPersonalReference>($@"
                                INSERT INTO StudyPersonalReference (	                                
                                     StudyId ,
                                     ReferenceTitle ,
                                     Name ,
                                     CurrentJob ,
                                     Address ,
                                     Phone ,
                                     YearsKnowingEachOther ,
                                     KnowAddress ,
                                     YearsOnCurrentResidence ,
                                     KnowledgeAboutPreviousJobs ,
                                     OpinionAboutTheCandidate ,
                                     PoliticalActivity ,
                                     WouldYouRecommendIt)
                                OUTPUT 
                                    INSERTED.*
                                VALUES {content} ");

                    return new GenericResponse<List<StudyPersonalReference>>()
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
                return new GenericResponse<List<StudyPersonalReference>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyPersonalReference>> DeleteStudyPersonalReference(long id)
        {
            try
            {
                string query = $@"
                                 UPDATE StudyPersonalReference
                                 SET
                                     DeletedAt = '{DateTime.UtcNow}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE Id = {id}";
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyPersonalReference>(query);

                    return new GenericResponse<StudyPersonalReference>()
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
                return new GenericResponse<StudyPersonalReference>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<StudyPersonalReference>>> GetStudyPersonalReference(List<long> id, bool byStudy = false)
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
                    var apiResponse = await conn.QueryAsync<StudyPersonalReference>($@"
                        SELECT 
                            spr.*
                        FROM
                            StudyPersonalReference spr
                        WHERE 
                            spr.DeletedAt IS NULL
                        AND
                            {(byStudy ? $@"spr.StudyId IN ({ids})" : $@"spr.Id IN ({ids})")}");

                    return new GenericResponse<List<StudyPersonalReference>>()
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
                return new GenericResponse<List<StudyPersonalReference>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyPersonalReference>> UpdateStudyPersonalReference(StudyPersonalReference request)
        {
            try
            {
                string query = $@"
                                 UPDATE StudyPersonalReference
                                 SET
                                    ReferenceTitle = '{request.ReferenceTitle}',
                                    Name = '{request.Name}',
                                    CurrentJob = '{request.CurrentJob}',
                                    Address = '{request.Address}',
                                    Phone = '{request.Phone}',
                                    YearsKnowingEachOther = '{request.YearsKnowingEachOther}',
                                    KnowAddress = '{request.KnowAddress}',
                                    YearsOnCurrentResidence = '{request.YearsOnCurrentResidence}',
                                    KnowledgeAboutPreviousJobs = '{request.KnowledgeAboutPreviousJobs}',
                                    OpinionAboutTheCandidate = '{request.OpinionAboutTheCandidate}' ,
                                    PoliticalActivity = '{request.PoliticalActivity}' ,
                                    WouldYouRecommendIt = '{request.WouldYouRecommendIt}',
                                    UpdatedAt = '{DateTime.UtcNow}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE Id = {request.Id}";
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyPersonalReference>(query);

                    return new GenericResponse<StudyPersonalReference>()
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
                return new GenericResponse<StudyPersonalReference>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
