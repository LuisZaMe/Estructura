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
    public class StudyLaboralTrayectoryRepository:BaseRepository, IStudyLaboralTrayectoryRepository
    {
        public StudyLaboralTrayectoryRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) { }

        public async Task<GenericResponse<List<StudyLaboralTrayectory>>> CreateStudyLaboralTrayectory(List<StudyLaboralTrayectory> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
                                    {e.StudyId},
                                    '{e.TrayectoryName}',
                                    '{e.CompanyName}',
                                    '{e.CandidateBusinessName}',
                                    '{e.CompanyBusinessName}',
                                    '{e.CandidateRole}',
                                    '{e.CompanyRole}',
                                    '{e.CandidatePhone}',
                                    '{e.CompanyPhone}',
                                    '{e.CandidateAddress}',
                                    '{e.CompanyAddress}',
                                    '{e.CandidateStartDate}',
                                    '{e.CompanyStartDate}',
                                    '{e.CandidateEndDate}',
                                    '{e.CompanyEndDate}',
                                    '{e.CandidateInitialRole}',
                                    '{e.CompanyInitialRole}',
                                    '{e.CandidateFinalRole}',
                                    '{e.CompanyFinalRole}',
                                    {e.CandidateStartSalary},
                                    {e.CompanyStartSalary},
                                    {e.CandidateEndSalary},
                                    {e.CompanyEndSalary},
                                    '{e.CandidateBenefits}',
                                    '{e.CompanyBenefits}',
                                    '{e.CandidateResignationReason}',
                                    '{e.CompanyResignationReason}',
                                    '{e.CandidateDirectBoss}',
                                    '{e.CompanyDirectBoss}',
                                    '{e.CandidateLaborUnion}',
                                    '{e.CompanyLaborUnion}',
                                    '{e.Recommended}',
                                    '{e.RecommendedReasonWhy}',
                                    '{e.Rehire}',
                                    '{e.RehireReason}',
                                    '{e.Observations}',
                                    '{e.Notes}',
                                    {(e.Media1 == null || e.Media1.Id ==0? "null": e.Media1.Id.ToString())},
                                    {(e.Media2 == null || e.Media2.Id ==0 ? "null" : e.Media2.Id.ToString())}
                                )";
                });
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<StudyLaboralTrayectory>($@"
                                INSERT INTO StudyLaboralTrayectory (	                                
                                     StudyId ,
                                     TrayectoryName ,
                                     CompanyName ,
                                     CandidateBusinessName ,
                                     CompanyBusinessName ,
                                     CandidateRole ,
                                     CompanyRole ,
                                     CandidatePhone ,
                                     CompanyPhone ,
                                     CandidateAddress ,
                                     CompanyAddress ,
                                     CandidateStartDate ,
                                     CompanyStartDate ,
                                     CandidateEndDate ,
                                     CompanyEndDate ,
                                     CandidateInitialRole ,
                                     CompanyInitialRole ,
                                     CandidateFinalRole ,
                                     CompanyFinalRole ,
                                     CandidateStartSalary ,
                                     CompanyStartSalary ,
                                     CandidateEndSalary ,
                                     CompanyEndSalary ,
                                     CandidateBenefits ,
                                     CompanyBenefits ,
                                     CandidateResignationReason ,
                                     CompanyResignationReason ,
                                     CandidateDirectBoss ,
                                     CompanyDirectBoss ,
                                     CandidateLaborUnion ,
                                     CompanyLaborUnion ,
                                     Recommended ,
                                     RecommendedReasonWhy ,
                                     Rehire ,
                                     RehireReason ,
                                     Observations ,
                                     Notes ,
                                     Media1Id ,
                                     Media2Id)
                                OUTPUT 
                                    INSERTED.*
                                VALUES {content} ");

                    return new GenericResponse<List<StudyLaboralTrayectory>>()
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
                return new GenericResponse<List<StudyLaboralTrayectory>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyLaboralTrayectory>> DeleteStudyLaboralTrayectory(long id)
        {
            try
            {
                string query = $@"
                                 UPDATE StudyLaboralTrayectory
                                 SET
                                     DeletedAt = '{DateTime.UtcNow}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE Id = {id}";
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyLaboralTrayectory>(query);

                    return new GenericResponse<StudyLaboralTrayectory>()
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
                return new GenericResponse<StudyLaboralTrayectory>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<StudyLaboralTrayectory>>> GetStudyLaboralTrayectory(List<long> id, bool byStudy = false)
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
                    var apiResponse = await conn.QueryAsync<StudyLaboralTrayectory, long?, long?, StudyLaboralTrayectory>($@"
                        SELECT 
                            slt.*,
                            slt.Media1Id as m1,
                            slt.Media2Id as m2
                        FROM
                        StudyLaboralTrayectory slt
                        WHERE 
                            slt.DeletedAt IS NULL
                        AND
                            {(byStudy ? $@"slt.StudyId IN ({ids})" : $@"slt.Id IN ({ids})")}", 
                            (trayectory, m1, m2) =>
                    {
                        trayectory.Media1 = new Media() { Id = m1==null ? 0 : (long)m1 };
                        trayectory.Media2 = new Media() { Id =  m2==null ? 0 : (long)m2 };
                        return trayectory;
                    }, splitOn:"Id, m1, m2");

                    return new GenericResponse<List<StudyLaboralTrayectory>>()
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
                return new GenericResponse<List<StudyLaboralTrayectory>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyLaboralTrayectory>> UpdateStudyLaboralTrayectory(StudyLaboralTrayectory request)
        {
            try
            {
                string query = $@"
                                 UPDATE StudyLaboralTrayectory
                                 SET
                                    TrayectoryName = '{request.TrayectoryName}',
                                    CompanyName = '{request.CompanyName}',
                                    CandidateBusinessName = '{request.CandidateBusinessName}',
                                    CompanyBusinessName = '{request.CompanyBusinessName}',
                                    CandidateRole = '{request.CandidateRole}',
                                    CompanyRole = '{request.CompanyRole}',
                                    CandidatePhone = '{request.CandidatePhone}',
                                    CompanyPhone = '{request.CompanyPhone}',
                                    CandidateAddress = '{request.CandidateAddress}',
                                    CompanyAddress = '{request.CompanyAddress}' ,
                                    CandidateStartDate = '{request.CandidateStartDate}' ,
                                    CompanyStartDate = '{request.CompanyStartDate}' ,
                                    CandidateEndDate = '{request.CandidateEndDate}' ,
                                    CompanyEndDate = '{request.CompanyEndDate}' ,
                                    CandidateInitialRole = '{request.CandidateInitialRole}' ,
                                    CompanyInitialRole = '{request.CompanyInitialRole}' ,
                                    CandidateFinalRole = '{request.CandidateFinalRole}' ,
                                    CompanyFinalRole = '{request.CompanyFinalRole}' ,
                                    CandidateStartSalary  = {request.CandidateStartSalary},
                                    CompanyStartSalary = {request.CompanyStartSalary} ,
                                    CandidateEndSalary = {request.CandidateEndSalary} ,
                                    CompanyEndSalary = {request.CompanyEndSalary} ,
                                    CandidateBenefits = '{request.CandidateBenefits}' ,
                                    CompanyBenefits = '{request.CompanyBenefits}' ,
                                    CandidateResignationReason = '{request.CandidateResignationReason}' ,
                                    CompanyResignationReason  = '{request.CompanyResignationReason}',
                                    CandidateDirectBoss = '{request.CandidateDirectBoss}' ,
                                    CompanyDirectBoss = '{request.CompanyDirectBoss}' ,
                                    CandidateLaborUnion = '{request.CandidateLaborUnion}',
                                    CompanyLaborUnion = '{request.CompanyLaborUnion}' ,
                                    Recommended = '{request.Recommended}' ,
                                    RecommendedReasonWhy  = '{request.RecommendedReasonWhy}',
                                    Rehire = '{request.Rehire}' ,
                                    RehireReason = '{request.RehireReason}' ,
                                    Observations = '{request.Observations}' ,
                                    Notes  = '{request.Notes}',
                                    Media1Id = {(request.Media1 == null || request.Media1.Id ==0 ? "null" : request.Media1.Id.ToString())},
                                    Media2Id = {(request.Media2 == null || request.Media2.Id ==0 ? "null" : request.Media2.Id.ToString())},
                                    UpdatedAt = '{DateTime.UtcNow}'
                                OUTPUT 
                                    INSERTED.*
                                WHERE Id = {request.Id}";
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyLaboralTrayectory>(query);

                    return new GenericResponse<StudyLaboralTrayectory>()
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
                return new GenericResponse<StudyLaboralTrayectory>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
