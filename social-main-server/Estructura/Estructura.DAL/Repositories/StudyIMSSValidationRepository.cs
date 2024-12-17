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
    public class StudyIMSSValidationRepository : BaseRepository, IStudyIMSSValidationRepository
    {
        public StudyIMSSValidationRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) { }


        //StudyIMSSValidation
        public async Task<GenericResponse<StudyIMSSValidation>> CreateStudyIMSSValidation(StudyIMSSValidation request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyIMSSValidation>($@"
                                INSERT INTO StudyIMSSValidation (
	                                StudyId,
	                                CreditNumber,
	                                CreditStatus,
	                                GrantDate,
	                                ConciliationClaimsSummary)
                                OUTPUT 
                                    INSERTED.*
                                VALUES (
	                                {request.StudyId},
	                                '{request.CreditNumber}',
	                                '{request.CreditStatus}',
	                                '{request.GrantDate}',
	                                '{request.ConciliationClaimsSummary}')
                    ");

                    return new GenericResponse<StudyIMSSValidation>()
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
                return new GenericResponse<StudyIMSSValidation>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyIMSSValidation>> DeleteStudyIMSSValidation(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyIMSSValidation>($@"
                            UPDATE StudyIMSSValidation
                            SET
                                DeletedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE 
                                Id = {id}");

                    return new GenericResponse<StudyIMSSValidation>()
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
                return new GenericResponse<StudyIMSSValidation>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<StudyIMSSValidation>>> GetStudyIMSSValidation(List<long> id, bool byStudy= false)
        {
            try
            {
                string extra = string.Empty;
                id.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(extra)) extra += ", ";
                    extra += e;
                });

                if (!string.IsNullOrWhiteSpace(extra))
                {
                    extra = byStudy ? $@" AND siv.StudyId IN ({extra})" : $@" AND siv.Id IN ({extra})";
                }

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<StudyIMSSValidation, string, StudyIMSSValidation>($@"
                            SELECT
                                siv.*,
                                (
                                    SELECT
                                        iv.* 
                                    FROM IMSSValidation iv 
                                    WHERE 
                                        iv.StudyIMSSValidationId = siv.id 
                                    AND 
                                        iv.DeletedAt IS NULL
                                    FOR JSON PATH
                                ) AS IMSSValidationJson
                            FROM 
                                StudyIMSSValidation siv
                            WHERE siv.DeletedAt IS NULL
                                {extra}
                            ORDER BY siv.Id
                        ", (siv, iv) =>
                    {
                        if (!string.IsNullOrWhiteSpace(iv))
                            siv.IMSSValidationList = JsonConvert.DeserializeObject<List<IMSSValidation>>(iv);
                        else
                            siv.IMSSValidationList = new List<IMSSValidation>();
                        return siv;
                    }, splitOn: "Id, IMSSValidationJson");

                    return new GenericResponse<List<StudyIMSSValidation>>()
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
                return new GenericResponse<List<StudyIMSSValidation>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyIMSSValidation>> UpdateStudyIMSSValidation(StudyIMSSValidation request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyIMSSValidation>($@"
                            UPDATE StudyIMSSValidation
                            SET
                                CreditNumber = '{request.CreditNumber}',
                                CreditStatus = '{request.CreditStatus}',
                                GrantDate = '{request.GrantDate}',
                                ConciliationClaimsSummary = '{request.ConciliationClaimsSummary}',
                                UpdatedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE 
                                Id = {request.Id}");

                    return new GenericResponse<StudyIMSSValidation>()
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
                return new GenericResponse<StudyIMSSValidation>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }


        //IMSSValidation
        public async Task<GenericResponse<List<IMSSValidation>>> CreateIMSSValidation(List<IMSSValidation> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"
                        (
	                        {e.StudyIMSSValidationId},
	                        '{e.CompanyBusinessName}',
	                        '{e.StartDate}',
	                        '{e.EndDate}',
	                        '{e.Result}'
                        )";
                });
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<IMSSValidation>($@"
                                INSERT INTO IMSSValidation (
	                                StudyIMSSValidationId,
	                                CompanyBusinessName,
	                                StartDate,
	                                EndDate,
	                                Result)
                                OUTPUT 
                                    INSERTED.*
                                VALUES {content}
                    ");

                    return new GenericResponse<List<IMSSValidation>>()
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
                return new GenericResponse<List<IMSSValidation>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<IMSSValidation>> DeleteIMSSValidation(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<IMSSValidation>($@"
                            UPDATE IMSSValidation
                            SET
                                DeletedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE 
                                Id = {id}");

                    return new GenericResponse<IMSSValidation>()
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
                return new GenericResponse<IMSSValidation>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<IMSSValidation>>> GetIMSSValidation(List<long> id)
        {
            try
            {
                string extra = string.Empty;
                id.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(extra)) extra += ", ";
                    extra += e;
                });

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<IMSSValidation>($@"
                        SELECT
                            iv.* 
                        FROM IMSSValidation iv 
                        WHERE 
                            iv.DeletedAt IS NULL
                        AND     
                            iv.Id IN ({extra})
                        ");

                    return new GenericResponse<List<IMSSValidation>>()
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
                return new GenericResponse<List<IMSSValidation>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<IMSSValidation>> UpdateIMSSValidation(IMSSValidation request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<IMSSValidation>($@"
                            UPDATE IMSSValidation
                            SET
                                CompanyBusinessName = '{request.CompanyBusinessName}',
                                StartDate = '{request.StartDate}',
                                EndDate = '{request.EndDate}',
                                Result = '{request.Result}',
                                UpdatedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE 
                                Id = {request.Id}");

                    return new GenericResponse<IMSSValidation>()
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
                return new GenericResponse<IMSSValidation>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
