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
    public class StudyFamilyRepository : BaseRepository, IStudyFamilyRepository
    {
        public StudyFamilyRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor)
        {

        }


        //StudyFamily
        public async Task<GenericResponse<StudyFamily>> CreateStudyFamily(StudyFamily request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyFamily>($@"
                        INSERT INTO StudyFamily (
                            Notes,
                            FamiliarArea,
                            StudyId)
                        OUTPUT INSERTED.*
                        VALUES (
                            '{request.Notes}',
                            '{request.FamiliarArea}',
                            {request.StudyId}
                        )");

                    return new GenericResponse<StudyFamily>()
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
                return new GenericResponse<StudyFamily>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyFamily>> DeleteStudyFamily(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyFamily>($@"
                        UPDATE StudyFamily 
                        SET
                            DeletedAt = '{DateTime.UtcNow}'
                        OUTPUT INSERTED.*
                        WHERE
                            Id = {id}");

                    return new GenericResponse<StudyFamily>()
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
                return new GenericResponse<StudyFamily>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<StudyFamily>>> GetStudyFamily(List<long> id, bool byStudy)
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
                    var apiResponse = await conn.QueryAsync<StudyFamily, string, string, StudyFamily>($@"
                        SELECT
                            sf.*,
                            (
                                SELECT lf.*, lf.MaritalStatusId AS MaritalStatus FROM LivingFamily lf WHERE lf.StudyFamilyId = sf.Id AND lf.DeletedAt IS NULL
                                FOR JSON PATH
                            ) AS LivingFamilyJSON,
                            (
                                SELECT nlf.*, nlf.MaritalStatusId AS MaritalStatus FROM NoLivingFamily nlf WHERE nlf.StudyFamilyId = sf.Id AND nlf.DeletedAt IS NULL
                                FOR JSON PATH
                            ) AS NoLivingFamilyJSON
                        FROM StudyFamily sf
                        WHERE
                            DeletedAt IS NULL
                        AND
                            {(byStudy ? $@"sf.StudyId IN ({ids})" : $@"sf.Id IN ({ids})")}", (study, lfj, nlfj) =>
                    {
                        if (!string.IsNullOrWhiteSpace(lfj))
                            study.LivingFamilyList = JsonConvert.DeserializeObject<List<LivingFamily>>(lfj);
                        if (!string.IsNullOrWhiteSpace(nlfj))
                            study.NoLivingFamilyList = JsonConvert.DeserializeObject<List<NoLivingFamily>>(nlfj);
                        return study;
                    }, splitOn: "Id, LivingFamilyJSON, NoLivingFamilyJSON");

                    return new GenericResponse<List<StudyFamily>>()
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
                return new GenericResponse<List<StudyFamily>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyFamily>> UpdateStudyFamily(StudyFamily request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyFamily>($@"
                        UPDATE StudyFamily 
                        SET
                            Notes = '{request.Notes}',
                            FamiliarArea = '{request.FamiliarArea}',
                            UpdatedAt = '{DateTime.UtcNow}'
                        OUTPUT INSERTED.*
                        WHERE
                            Id = {request.Id}");

                    return new GenericResponse<StudyFamily>()
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
                return new GenericResponse<StudyFamily>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }



        //LivingFamily
        public async Task<GenericResponse<LivingFamily>> DeleteLivingFamily(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<LivingFamily>($@"
                        UPDATE LivingFamily 
                        SET
                            DeletedAt = '{DateTime.UtcNow}'
                        OUTPUT INSERTED.*
                        WHERE
                            Id = {id}");

                    return new GenericResponse<LivingFamily>()
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
                return new GenericResponse<LivingFamily>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<LivingFamily>>> GetLivingFamily(List<long> id)
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
                    var apiResponse = await conn.QueryAsync<LivingFamily>($@"
                        SELECT
                            lf.*,
                            lf.MaritalStatusId AS MaritalStatus
                        FROM LivingFamily lf
                        WHERE
                            DeletedAt IS NULL
                        AND
                            Id IN ({ids})");

                    return new GenericResponse<List<LivingFamily>>()
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
                return new GenericResponse<List<LivingFamily>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<LivingFamily>> UpdateLivingFamily(LivingFamily request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<LivingFamily>($@"
                        UPDATE LivingFamily 
                        SET
                            Name = '{request.Name}',
                            Relationship = '{request.Relationship}',
                            Age = '{request.Age}',
                            MaritalStatusId = '{(int)request.MaritalStatus}',
                            Schoolarity = '{request.Schoolarity}',
                            Address = '{request.Address}',
                            Phone = '{request.Phone}',
                            Occupation = '{request.Occupation}',
                            UpdatedAt = '{DateTime.UtcNow}'
                        OUTPUT 
                            INSERTED.*,
                            INSERTED.MaritalStatusId AS MaritalStatus
                        WHERE
                            Id = {request.Id}");

                    return new GenericResponse<LivingFamily>()
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
                return new GenericResponse<LivingFamily>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<LivingFamily>>> CreateLivingFamily(List<LivingFamily> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
                                    {e.StudyFamilyId},
                                    '{e.Name}',
                                    '{e.Relationship}',
                                    '{e.Age}',
                                    {(int)e.MaritalStatus},
                                    '{e.Schoolarity}',
                                    '{e.Address}',
                                    '{e.Phone}',
                                    '{e.Occupation}'
                                )";
                });

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<LivingFamily>($@"
                        INSERT INTO LivingFamily (
                            StudyFamilyId,
                            Name,
                            Relationship,
                            Age,
                            MaritalStatusId,
                            Schoolarity,
                            Address,
                            Phone,
                            Occupation)
                        OUTPUT INSERTED.*
                        VALUES {content}");

                    return new GenericResponse<List<LivingFamily>>()
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
                return new GenericResponse<List<LivingFamily>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }



        // NoLivingFamily
        public async Task<GenericResponse<List<NoLivingFamily>>> CreateNoLivingFamily(List<NoLivingFamily> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
                                    {e.StudyFamilyId},
                                    '{e.Name}',
                                    '{e.Relationship}',
                                    '{e.Age}',
                                    {(int)e.MaritalStatus},
                                    '{e.Schoolarity}',
                                    '{e.Address}',
                                    '{e.Phone}',
                                    '{e.Occupation}'
                                )";
                });

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<NoLivingFamily>($@"
                        INSERT INTO NoLivingFamily (
                            StudyFamilyId,
                            Name,
                            Relationship,
                            Age,
                            MaritalStatusId,
                            Schoolarity,
                            Address,
                            Phone,
                            Occupation)
                        OUTPUT INSERTED.*
                        VALUES {content}");

                    return new GenericResponse<List<NoLivingFamily>>()
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
                return new GenericResponse<List<NoLivingFamily>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<NoLivingFamily>> UpdateNoLivingFamily(NoLivingFamily request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<NoLivingFamily>($@"
                        UPDATE NoLivingFamily 
                        SET
                            Name = '{request.Name}',
                            Relationship = '{request.Relationship}',
                            Age = '{request.Age}',
                            MaritalStatusId = '{(int)request.MaritalStatus}',
                            Schoolarity = '{request.Schoolarity}',
                            Address = '{request.Address}',
                            Phone = '{request.Phone}',
                            Occupation = '{request.Occupation}',
                            UpdatedAt = '{DateTime.UtcNow}'
                        OUTPUT 
                            INSERTED.*,
                            INSERTED.MaritalStatusId AS MaritalStatus
                        WHERE
                            Id = {request.Id}");

                    return new GenericResponse<NoLivingFamily>()
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
                return new GenericResponse<NoLivingFamily>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<NoLivingFamily>>> GetNoLivingFamily(List<long> id)
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
                    var apiResponse = await conn.QueryAsync<NoLivingFamily>($@"
                        SELECT
                            nlf.*,
                            nlf.MaritalStatusId AS MaritalStatus
                        FROM NoLivingFamily nlf
                        WHERE
                            DeletedAt IS NULL
                        AND
                            Id IN ({ids})");

                    return new GenericResponse<List<NoLivingFamily>>()
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
                return new GenericResponse<List<NoLivingFamily>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<NoLivingFamily>> DeleteNoLivingFamily(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<NoLivingFamily>($@"
                        UPDATE NoLivingFamily 
                        SET
                            DeletedAt = '{DateTime.UtcNow}'
                        OUTPUT INSERTED.*
                        WHERE
                            Id = {id}");

                    return new GenericResponse<NoLivingFamily>()
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
                return new GenericResponse<NoLivingFamily>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
