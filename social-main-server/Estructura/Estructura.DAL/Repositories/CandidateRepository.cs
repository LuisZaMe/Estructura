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
    public class CandidateRepository:BaseRepository, ICandidateRepository
    {
        private readonly IAccountRepository _accountRepository;
        public CandidateRepository(IAccountRepository _accountRepository, IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor)
        {
            this._accountRepository=_accountRepository;
        }

        public async Task<GenericResponse<Candidate>> CreateCandidate(Candidate request)
        {
            try
            {
                var admin = await _accountRepository.GetAdminForCurrentUser();
                if (!admin.Sucess)
                {
                    return new GenericResponse<Candidate>()
                    {
                        ErrorMessage = admin.ErrorMessage,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                    };
                }
                using (var conn = Connection)
                {
                    conn.Open();
                    var query = $@"
                                INSERT INTO Candidate (
	                                Name,
	                                Lastname,
	                                Phone,
	                                Email,
	                                CURP,
	                                NSS,
	                                Address,
	                                Position,
                                    CityId,
                                    StateId,
                                    CandidateStatusId,
                                    UnderAdminUserId,
                                    IsActive,
                                    ClientId)
                                OUTPUT 
                                    INSERTED.*,
                                    INSERTED.ClientId as cid
                                VALUES (
	                                '{request.Name}',
	                                '{request.Lastname}',
	                                '{request.Phone}',
	                                '{request.Email}',
	                                '{request.CURP}',
	                                '{request.NSS}',
	                                '{request.Address}',
	                                '{request.Position}',
	                                '{request.City.Id}',
	                                '{request.State.Id}',
                                    1,
                                    0,
                                    1,
                                    '{(long)request.Client.Id}')
                    ";
                    var apiResponse = await conn.QueryAsync<Candidate, int, Candidate>(query, (candidate, client) =>
                    {
                        candidate.Client = new Identity()
                        {
                            Id = client
                        };
                        return candidate;
                    }, splitOn: "Id, cid");

                    return new GenericResponse<Candidate>()
                    {
                        Sucess = true,
                        Response = apiResponse.FirstOrDefault(),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return new GenericResponse<Candidate>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Candidate>> DeleteCandidate(long Id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Candidate>($@"
                            UPDATE 
                                Candidate 
                            SET 
                                IsActive = 0
                            OUTPUT 
                                INSERTED.*
                            WHERE 
                                Candidate.Id = {Id}
                        ");

                    return new GenericResponse<Candidate>()
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
                return new GenericResponse<Candidate>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<Candidate>>> GetCandidate(List<long> Id, int currentPage, int offset)
        {
            try
            {
                string extra = string.Empty;
                Id.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(extra)) extra += ", ";
                    extra += e;
                });

                if (!string.IsNullOrWhiteSpace(extra))
                    extra = $@"c.Id IN ({extra}) AND ";

                var admin = await _accountRepository.GetAdminForCurrentUser();
                if (!admin.Sucess)
                {
                    return new GenericResponse<List<Candidate>>()
                    {
                        ErrorMessage = admin.ErrorMessage,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                    };
                }

                using (var conn = Connection)
                {
                    conn.Open();
                    var query = $@"
                            SELECT
                                c.*,
                                C.CandidateStatusId AS CandidateStatus,
                                cl.*,
                                ci.*,
                                st.*,
                                m.*
                            FROM
	                            Candidate c
                            INNER JOIN [User] cl
                                ON cl.Id = ClientId
                            LEFT JOIN Cities ci
                                ON ci.Id = c.CityId
                            LEFT JOIN States st
                                ON st.Id = c.StateId
                            LEFT JOIN Media m
                                ON m.Id = c.MediaId
                            WHERE
                                c.Id >= {Id.FirstOrDefault()}
                            AND
                                c.IsActive = 1
                                ORDER BY c.Id
							OFFSET {currentPage * offset} ROWS 
                            FETCH NEXT {offset} ROWS ONLY
                        ";
                    var apiResponse = await conn.QueryAsync<Candidate, Identity, City, State, Media, Candidate>(query, (candidate, cliente, city, state, media) =>
                    {
                        candidate.City = city;
                        candidate.State = state;
                        candidate.Client = cliente;
                        candidate.Media = media;
                        return candidate;
                    },splitOn: "Id, Id, Id, Id, Id");

                    return new GenericResponse<List<Candidate>>()
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
                return new GenericResponse<List<Candidate>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<int>> Pagination(string key = "", long clientId = 0, int splitBy = 10)
        {
            try
            {
                string clientQuery = string.Empty;
                if (clientId!=0)
                {
                    clientQuery = $@"AND c.ClientId = {clientId}";
                }

                var admin = await _accountRepository.GetAdminForCurrentUser();
                if (!admin.Sucess)
                {
                    return new GenericResponse<int>()
                    {
                        ErrorMessage = admin.ErrorMessage,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                    };
                }

                string byKey = string.Empty;
                if (!string.IsNullOrEmpty(key))
                {
                    byKey = $@"
                        AND (
	                            c.Name LIKE ('%{key}%')
                            OR	c.Lastname LIKE ('%{key}%')
                            OR	c.Phone LIKE ('%{key}%')
                            OR	c.Email LIKE ('%{key}%')
                        )
                            ";
                }

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<int>($@"
                            SELECT COUNT(Id) AS TotalRows 
                            FROM Candidate c 
                            WHERE 
                                c.IsActive = 1 
                                {clientQuery}
                                {admin.Response.QueryFilter}
                                {byKey}
                        ");

                    var extra = (float)apiResponse%(float)splitBy;
                    return new GenericResponse<int>()
                    {
                        Sucess = true,
                        Response = (apiResponse/splitBy) + (extra!=0?1:0),
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

        public async Task<GenericResponse<List<Candidate>>> SearchCandidate(string key, long clientId = 0, int currentPage = 0, int offset = 10)
        {
            try
            {
                string clientQuery = string.Empty;
                if(clientId!=0)
                {
                    clientQuery = $@"AND c.ClientId = {clientId}";
                }
                var admin = await _accountRepository.GetAdminForCurrentUser();
                if (!admin.Sucess)
                {
                    return new GenericResponse<List<Candidate>>()
                    {
                        ErrorMessage = admin.ErrorMessage,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                    };
                }
                using (var conn = Connection)
                {
                    conn.Open();
                    var query = $@"
                            SELECT
                                c.*,
                                C.CandidateStatusId AS CandidateStatus,
                                cl.*,
                                ci.*,
                                st.*,
                                m.*
                            FROM
	                            Candidate c
                            LEFT JOIN [User] cl
                                ON cl.Id = ClientId                                
                            LEFT JOIN Cities ci
                                ON ci.Id = c.CityId
                            LEFT JOIN States st
                                ON st.Id = c.StateId
                            LEFT JOIN Media m
                                ON m.Id = c.MediaId
                            WHERE 
                                (
                                    c.Name LIKE '%{key}%' 
                                    OR c.Lastname LIKE '%{key}%' 
                                    OR c.Phone LIKE ('%{key}%')
                                    OR c.Email LIKE ('%{key}%')
                                )                         
                            {clientQuery}
                            {(admin.Response.FullSearch ? "" : $@" AND c.UnderAdminUserId = {admin.Response.AdminId}")}
                            ORDER BY c.Id
							OFFSET {currentPage * offset} ROWS 
                            FETCH NEXT {offset} ROWS ONLY
                    ";
                    var apiResponse = await conn.QueryAsync<Candidate, Identity, City, State, Media, Candidate>(query, (candidate, cliente, city, state, media) =>
                    {
                        candidate.City = city;
                        candidate.State = state;
                        candidate.Client = cliente;
                        candidate.Media = media;
                        return candidate;
                    }, splitOn: "Id, Id, Id, Id, Id");

                    return new GenericResponse<List<Candidate>>()
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
                return new GenericResponse<List<Candidate>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Candidate>> UpdateCandidate(Candidate request)
        {
            try
            {
                string query = $@"UPDATE Candidate
                                SET
                                    Name = '{request.Name}',
                                    Lastname = '{request.Lastname}',
                                    Phone = '{request.Phone}',
                                    Email = '{request.Email}',
                                    CURP = '{request.CURP}',
                                    NSS = '{request.NSS}',
                                    Address = '{request.Address}',
                                    Position = '{request.Position}',
                                    CityId = {request.City.Id},
                                    StateId = {request.State.Id},
                                    CandidateStatusId = {(int)request.CandidateStatus},
                                    MediaId = {(request.Media!=null&&request.Media.Id!=0? request.Media.Id.ToString(): "NULL")}
                                OUTPUT 
                                    INSERTED.*,
                                    INSERTED.CandidateStatusId AS CandidateStatus
                                WHERE
                                Id = { request.Id }
                ";
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Candidate>(query);

                    return new GenericResponse<Candidate>()
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
                return new GenericResponse<Candidate>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    


        // Notes
        public async Task<GenericResponse<CandidateNote>> CreateNote(CandidateNote request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<CandidateNote>($@"
                        INSERT INTO CandidateNote (
	                        Description,
	                        CandidateId,
                            CreatedAt)
                        OUTPUT INSERTED.*
                        VALUES (
	                        '{request.Description}',
	                        {request.CandidateId},
                            '{DateTime.UtcNow.ToShortDateString()}')
                        ");

                    return new GenericResponse<CandidateNote>()
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
                return new GenericResponse<CandidateNote>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<CandidateNote>> UpdateNote(CandidateNote request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<CandidateNote>($@"
                            UPDATE CandidateNote
                            SET
	                            Description = '{request.Description}'
                            OUTPUT INSERTED.*
                            WHERE 
	                            Id = {request.Id}
                        ");

                    return new GenericResponse<CandidateNote>()
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
                return new GenericResponse<CandidateNote>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<CandidateNote>> DeleteNote(long Id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<CandidateNote>($@"
                            UPDATE CandidateNote
                            SET
	                            DeletedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE 
	                            Id = {Id}
                        ");

                    return new GenericResponse<CandidateNote>()
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
                return new GenericResponse<CandidateNote>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<CandidateNote>>> GetNotes(List<long> Id, string key = "", long candidateId = 0, int currentPage = 0, int offset = 10)
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
                if(!string.IsNullOrWhiteSpace(key))
                    keyFilter = $@" AND ( Description LIKE ('%{key}%') )";

                string candidateIdFilter = string.Empty;
                if (candidateId!=0)
                    candidateIdFilter = $@" AND (CandidateId = {candidateId})";



                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<CandidateNote>($@"
                            SELECT* FROM CandidateNote
                            WHERE
                                {idFilter}
                                {keyFilter}
                                {candidateIdFilter}
                                AND DeletedAt IS NULL
                            ORDER BY Id
						    OFFSET {currentPage * offset} ROWS 
                            FETCH NEXT {offset} ROWS ONLY
                        ");

                    return new GenericResponse<List<CandidateNote>>()
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
                return new GenericResponse<List<CandidateNote>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
