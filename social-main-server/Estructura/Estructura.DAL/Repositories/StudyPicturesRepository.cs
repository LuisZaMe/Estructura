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
    public class StudyPicturesRepository : BaseRepository, IStudyPicturesRepository
    {
        public StudyPicturesRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) { }

        public async Task<GenericResponse<StudyPictures>> CreateStudyPictures(StudyPictures request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<StudyPictures>($@"
                        INSERT INTO StudyPictures (	                                
                                StudyId ,
                                Media1Id,
                                Media2Id,
                                Media3Id,
                                Media4Id,
                                Media5Id,
                                Media6Id)
                        OUTPUT 
                            INSERTED.*
                        VALUES 
                            (
                                {request.StudyId},
                                {(request.Media1==null||request.Media1.Id==0? "null":  request.Media1.Id.ToString())},
                                {(request.Media2==null||request.Media2.Id==0? "null":  request.Media2.Id.ToString())},
                                {(request.Media3==null||request.Media3.Id==0? "null":  request.Media3.Id.ToString())},
                                {(request.Media4==null||request.Media4.Id==0? "null":  request.Media4.Id.ToString())},
                                {(request.Media5==null||request.Media5.Id==0? "null":  request.Media5.Id.ToString())},
                                {(request.Media6==null||request.Media6.Id==0? "null":  request.Media6.Id.ToString())}
                            )");

                    return new GenericResponse<StudyPictures>()
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
                return new GenericResponse<StudyPictures>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyPictures>> DeleteStudyPictures(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyPictures>($@"
                        UPDATE StudyPictures
                        SET
                            DeletedAt = '{DateTime.UtcNow}'
                        OUTPUT INSERTED.*
                        WHERE 
                            Id = {id}
                               
                        ");

                    return new GenericResponse<StudyPictures>()
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
                return new GenericResponse<StudyPictures>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<StudyPictures>>> GetStudyPictures(List<long> id, bool byStudy = false)
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
                    var apiResponse = await conn.QueryAsync<StudyPictures>($@"
                        SELECT
                            sp.* ,
                            sp.Media1Id AS m1i,
                            sp.Media2Id AS m2i,
                            sp.Media3Id AS m3i,
                            sp.Media4Id AS m4i,
                            sp.Media5Id AS m5i,
                            sp.Media6Id AS m6i
                        FROM StudyPictures sp   
                        WHERE 
                            sp.DeletedAt IS NULL 
                        AND
                        {(byStudy ? $@"sp.StudyId IN ({ids})" : $@"sp.Id IN ({ids})")}
                        ", new[]
                            {
                                typeof(StudyPictures),
                                typeof(long?),
                                typeof(long?),
                                typeof(long?),
                                typeof(long?),
                                typeof(long?),
                                typeof(long?)
                            }, (objects) =>
                            {
                                var current = objects[0] as StudyPictures;
                                current.Media1=new Media();
                                current.Media2=new Media();
                                current.Media3=new Media();
                                current.Media4=new Media();
                                current.Media5=new Media();
                                current.Media6=new Media();


                                if (objects[1] != null)
                                    current.Media1 = new Media() { Id = long.Parse(objects[1].ToString()) };
                                if (objects[2] != null)
                                    current.Media2 = new Media() { Id = long.Parse(objects[2].ToString()) };
                                if (objects[3] != null)
                                    current.Media3 = new Media() { Id = long.Parse(objects[3].ToString()) };
                                if (objects[4] != null)
                                    current.Media4 = new Media() { Id = long.Parse(objects[4].ToString()) };
                                if (objects[5] != null)
                                    current.Media5 = new Media() { Id = long.Parse(objects[5].ToString()) };
                                if (objects[6] != null)
                                    current.Media6 = new Media() { Id = long.Parse(objects[6].ToString()) };

                                return current;
                            }, splitOn: "Id, m1i,m2i,m3i,m4i,m5i,m6i");

                    return new GenericResponse<List<StudyPictures>>()
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
                return new GenericResponse<List<StudyPictures>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyPictures>> UpdateStudyPictures(StudyPictures request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyPictures>($@"
                        UPDATE StudyPictures
                        SET
                            Media1Id = {(request.Media1==null||request.Media1.Id==0 ? "null" : request.Media1.Id.ToString())},
                            Media2Id = {(request.Media2==null||request.Media2.Id==0 ? "null" : request.Media2.Id.ToString())},
                            Media3Id = {(request.Media3==null||request.Media3.Id==0 ? "null" : request.Media3.Id.ToString())},
                            Media4Id = {(request.Media4==null||request.Media4.Id==0 ? "null" : request.Media4.Id.ToString())},
                            Media5Id = {(request.Media5==null||request.Media5.Id==0 ? "null" : request.Media5.Id.ToString())},
                            Media6Id = {(request.Media6==null||request.Media6.Id==0 ? "null" : request.Media6.Id.ToString())},
                            UpdatedAt = '{DateTime.UtcNow}'
                        OUTPUT INSERTED.*
                        WHERE 
                            Id = {request.Id}
                               
                        ");

                    return new GenericResponse<StudyPictures>()
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
                return new GenericResponse<StudyPictures>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
