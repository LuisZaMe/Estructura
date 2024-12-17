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
    public class StudyFinalResultRepository: BaseRepository, IStudyFinalResultRepository
    {
        public StudyFinalResultRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) { }
        
        public async Task<GenericResponse<StudyFinalResult>> CreateStudyFinalResult(StudyFinalResult request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyFinalResult>($@"
                                INSERT INTO StudyFinalResults (
	                                StudyId,
	                                PositionSummary,
	                                AttitudeSummary,
	                                WorkHistorySummary,
	                                ArbitrationAndConciliationSummary,
	                                FinalResultsBy,
	                                FinalResultsPositionBy,
                                    ApplicationDate,
                                    VisitDate)
                                OUTPUT 
                                    INSERTED.*
                                VALUES (
	                                {request.StudyId},
	                                '{request.PositionSummary}',
	                                '{request.AttitudeSummary}',
	                                '{request.WorkHistorySummary}',
	                                '{request.ArbitrationAndConciliationSummary}',
	                                '{request.FinalResultsBy}',
	                                '{request.FinalResultsPositionBy}',
                                    '{request.ApplicationDate}',
                                    '{request.VisitDate}')
                    ");

                    return new GenericResponse<StudyFinalResult>()
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
                return new GenericResponse<StudyFinalResult>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyFinalResult>> DeleteStudyFinalResult(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyFinalResult>($@"
                                UPDATE StudyFinalResults 
                                SET 
                                    DeletedAt = '{DateTime.UtcNow}'
                                OUTPUT INSERTED.*
                                WHERE Id = {id}");

                    return new GenericResponse<StudyFinalResult>()
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
                return new GenericResponse<StudyFinalResult>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<StudyFinalResult>>> GetStudyFinalResult(List<long> id, int currentPage, int offset, bool byStudy = false)
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
                    extra = byStudy ? $@" AND sfr.StudyId IN ({extra})" : $@" AND sfr.Id IN ({extra})";
                    //extra = $@" AND sfr.Id IN ({extra})";
                }

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<StudyFinalResult>($@"
                            SELECT
                                sfr.* 
                            FROM 
                                StudyFinalResults sfr
                            WHERE sfr.DeletedAt IS NULL
                                {extra}
                            ORDER BY sfr.Id
							OFFSET {currentPage * offset} ROWS 
                            FETCH NEXT {offset} ROWS ONLY
                        ");

                    return new GenericResponse<List<StudyFinalResult>>()
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
                return new GenericResponse<List<StudyFinalResult>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyFinalResult>> UpdateStudyFinalResult(StudyFinalResult request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var ahora = DateTime.UtcNow.ToShortDateString();
                    var query = $@"
                                UPDATE StudyFinalResults 
                                SET 
                                    PositionSummary = '{request.PositionSummary}',
                                    AttitudeSummary = '{request.AttitudeSummary}',
                                    WorkHistorySummary = '{request.WorkHistorySummary}',
                                    ArbitrationAndConciliationSummary = '{request.ArbitrationAndConciliationSummary}',
                                    FinalResultsBy = '{request.FinalResultsBy}',
                                    FinalResultsPositionBy = '{request.FinalResultsPositionBy}',
                                    ApplicationDate = '{request.ApplicationDate}',
                                    VisitDate = '{request.VisitDate}',
                                    UpdatedAt = '{ahora}'
                                OUTPUT INSERTED.*
                                WHERE Id = {request.Id}";
                    var apiResponse = await conn.QuerySingleAsync<StudyFinalResult>(query);

                    return new GenericResponse<StudyFinalResult>()
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
                return new GenericResponse<StudyFinalResult>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
