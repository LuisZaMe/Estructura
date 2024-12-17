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
    public class StudyEconomicSituationRepository : BaseRepository, IStudyEconomicSituationRepository
    {
        public StudyEconomicSituationRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) { }



        // Study Economic Situation
        public async Task<GenericResponse<StudyEconomicSituation>> CreateStudyEconomicSituation(StudyEconomicSituation request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyEconomicSituation>($@"
                            INSERT INTO StudyEconomicSituation (
	                            StudyId,
	                            Electricity,
	                            Rent,
	                            Gas,
	                            Infonavit,
	                            Water,
	                            Credits,
	                            PropertyTax,
	                            Maintenance,
	                            Internet,
	                            Cable,
	                            Food,
	                            Cellphone,
	                            Gasoline,
	                            Entertainment,
	                            Clothing,
	                            Miscellaneous,
	                            Schoolar,
	                            EconomicSituationSummary
                            )
                            OUTPUT INSERTED.*
                            VALUES (
	                            {request.StudyId},
	                            {request.Electricity},
	                            {request.Rent},
	                            {request.Gas},
	                            {request.Infonavit},
	                            {request.Water},
	                            {request.Credits},
	                            {request.PropertyTax},
	                            {request.Maintenance},
	                            {request.Internet},
	                            {request.Cable},
	                            {request.Food},
	                            {request.Cellphone},
	                            {request.Gasoline},
	                            {request.Entertainment},
	                            {request.Clothing},
	                            {request.Miscellaneous},
	                            {request.Schoolar},
	                            '{request.EconomicSituationSummary}'
                            )
                    ");

                    return new GenericResponse<StudyEconomicSituation>()
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
                return new GenericResponse<StudyEconomicSituation>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyEconomicSituation>> DeleteStudyEconomicSituation(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyEconomicSituation>($@"
                            UPDATE StudyEconomicSituation 
                            SET
                                DeletedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {id}");

                    return new GenericResponse<StudyEconomicSituation>()
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
                return new GenericResponse<StudyEconomicSituation>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<StudyEconomicSituation>>> GetStudyEconomicSituation(List<long> id, bool byStudy = false)
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
                    extra =  byStudy ? $@"ses.StudyId IN ({extra})" : $@"ses.Id IN ({extra})";
                    //extra = $@" ses.Id IN ({extra})";
                }
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<StudyEconomicSituation>($@"
                            SELECT
                                ses.*,
                                (
                                    SELECT i.* FROM Incoming i 
                                    WHERE i.DeletedAt IS NULL AND i.StudyEconomicSituationId = ses.Id 
                                    FOR JSON PATH
                                )  AS IncomingJSON,
                                (
                                    SELECT ai.* FROM AdditionalIncoming ai 
                                    WHERE ai.DeletedAt IS NULL AND ai.StudyEconomicSituationId = ses.Id 
                                    FOR JSON PATH
                                )  AS AdditionalIncomingJSON,
                                (
                                    SELECT c.* FROM Credit c 
                                    WHERE c.DeletedAt IS NULL AND c.StudyEconomicSituationId = ses.Id 
                                    FOR JSON PATH
                                )  AS CreditJSON,
                                (
                                    SELECT e.* FROM Estate e
                                    WHERE e.DeletedAt IS NULL AND e.StudyEconomicSituationId = ses.Id 
                                    FOR JSON PATH
                                )  AS EstateJSON,
                                (
                                    SELECT v.* FROM Vehicle v
                                    WHERE v.DeletedAt IS NULL AND v.StudyEconomicSituationId = ses.Id 
                                    FOR JSON PATH
                                )  AS VehicleJSON
                            FROM 
                                StudyEconomicSituation ses
                            WHERE 
                                ses.DeletedAt IS NULL
                            AND 
                                {extra}",
                                new[]
                                {
                                    typeof(StudyEconomicSituation),
                                    typeof(string),
                                    typeof(string),
                                    typeof(string),
                                    typeof(string),
                                    typeof(string),
                                }, (objects) =>
                                {
                                    var current = objects[0] as StudyEconomicSituation;

                                    //Incoming list
                                    if (!string.IsNullOrWhiteSpace(objects[1] as string))
                                        current.IncomingList = JsonConvert.DeserializeObject<List<Incoming>>(objects[1] as string);
                                    else
                                        current.IncomingList = new List<Incoming>();

                                    //Additional Incoming list
                                    if (!string.IsNullOrWhiteSpace(objects[2] as string))
                                        current.AdditionalIncomingList = JsonConvert.DeserializeObject<List<AdditionalIncoming>>(objects[2] as string);
                                    else
                                        current.AdditionalIncomingList = new List<AdditionalIncoming>();

                                    //Credit list
                                    if (!string.IsNullOrWhiteSpace(objects[3] as string))
                                        current.CreditList = JsonConvert.DeserializeObject<List<Credit>>(objects[3] as string);
                                    else
                                        current.CreditList = new List<Credit>();

                                    //Estate list
                                    if (!string.IsNullOrWhiteSpace(objects[4] as string))
                                        current.EstateList = JsonConvert.DeserializeObject<List<Estate>>(objects[4] as string);
                                    else
                                        current.EstateList = new List<Estate>();

                                    //Estate list
                                    if (!string.IsNullOrWhiteSpace(objects[5] as string))
                                        current.VehicleList = JsonConvert.DeserializeObject<List<Vehicle>>(objects[5] as string);
                                    else
                                        current.VehicleList = new List<Vehicle>();

                                    return current;
                                }, splitOn: "Id, IncomingJSON, AdditionalIncomingJSON, CreditJSON, EstateJSON, VehicleJSON");

                    return new GenericResponse<List<StudyEconomicSituation>>()
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
                return new GenericResponse<List<StudyEconomicSituation>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<StudyEconomicSituation>> UpdateStudyEconomicSituation(StudyEconomicSituation request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<StudyEconomicSituation>($@"
                            UPDATE StudyEconomicSituation 
                            SET
	                            Electricity = {request.Electricity},
	                            Rent = {request.Rent},
	                            Gas = {request.Gas},
	                            Infonavit = {request.Infonavit},
	                            Water = {request.Water},
	                            Credits = {request.Credits},
	                            PropertyTax = {request.PropertyTax},
	                            Maintenance = {request.Maintenance},
	                            Internet = {request.Internet},
	                            Cable = {request.Cable},
	                            Food = {request.Food},
	                            Cellphone = {request.Cellphone},
	                            Gasoline = {request.Gasoline},
	                            Entertainment = {request.Entertainment},
	                            Clothing = {request.Clothing},
	                            Miscellaneous = {request.Miscellaneous},
	                            Schoolar = {request.Schoolar},
	                            EconomicSituationSummary = '{request.EconomicSituationSummary}',
                                UpdatedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {request.Id}");

                    return new GenericResponse<StudyEconomicSituation>()
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
                return new GenericResponse<StudyEconomicSituation>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }



        // Study Economic Situation
        public async Task<GenericResponse<List<Incoming>>> CreateIncoming(List<Incoming> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
                                    {e.StudyEconomicSituationId},
                                    '{e.Name}',
                                    '{e.Relationship}',
                                    {e.Amount}
                                )";
                });


                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<Incoming>($@"
                            INSERT INTO Incoming (
	                            StudyEconomicSituationId,
	                            Name,
	                            Relationship,
	                            Amount
                            )
                            OUTPUT INSERTED.*
                            VALUES {content}
                    ");

                    return new GenericResponse<List<Incoming>>()
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
                return new GenericResponse<List<Incoming>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Incoming>> DeleteIncoming(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Incoming>($@"
                            UPDATE Incoming 
                            SET
                                DeletedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {id}");

                    return new GenericResponse<Incoming>()
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
                return new GenericResponse<Incoming>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<Incoming>>> GetIncoming(List<long> id)
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
                    var apiResponse = await conn.QueryAsync<Incoming>($@"
                            SELECT 
                                i.* 
                            FROM
                                Incoming i
                            WHERE 
                                i.DeletedAt IS NULL
                            AND i.Id IN ({extra})");

                    return new GenericResponse<List<Incoming>>()
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
                return new GenericResponse<List<Incoming>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
        
        public async Task<GenericResponse<Incoming>> UpdateIncoming(Incoming request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Incoming>($@"
                            UPDATE Incoming 
                            SET
	                            Name = '{request.Name}',
	                            Relationship = '{request.Relationship}',
	                            Amount = {request.Amount},
                                UpdatedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {request.Id}");

                    return new GenericResponse<Incoming>()
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
                return new GenericResponse<Incoming>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }



        // Additional Incoming
        public async Task<GenericResponse<List<AdditionalIncoming>>> CreateAdditionalIncoming(List<AdditionalIncoming> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
                                    {e.StudyEconomicSituationId},
                                    '{e.Activity}',
                                    '{e.TimeFrame}',
                                    {e.Amount}
                                )";
                });


                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<AdditionalIncoming>($@"
                            INSERT INTO AdditionalIncoming (
	                            StudyEconomicSituationId,
	                            Activity,
	                            TimeFrame,
	                            Amount
                            )
                            OUTPUT INSERTED.*
                            VALUES {content}
                    ");

                    return new GenericResponse<List<AdditionalIncoming>>()
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
                return new GenericResponse<List<AdditionalIncoming>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<AdditionalIncoming>>> GetAdditionalIncoming(List<long> id)
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
                    var apiResponse = await conn.QueryAsync<AdditionalIncoming>($@"
                            SELECT 
                                ai.* 
                            FROM
                                AdditionalIncoming ai       
                            WHERE 
                                ai.DeletedAt IS NULL
                            AND ai.Id IN ({extra})");

                    return new GenericResponse<List<AdditionalIncoming>>()
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
                return new GenericResponse<List<AdditionalIncoming>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<AdditionalIncoming>> UpdateAdditionalIncoming(AdditionalIncoming request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<AdditionalIncoming>($@"
                            UPDATE AdditionalIncoming 
                            SET
	                            Activity = '{request.Activity}',
	                            TimeFrame = '{request.TimeFrame}',
	                            Amount = {request.Amount},
                                UpdatedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {request.Id}");

                    return new GenericResponse<AdditionalIncoming>()
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
                return new GenericResponse<AdditionalIncoming>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<AdditionalIncoming>> DeleteAdditionalIncoming(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<AdditionalIncoming>($@"
                            UPDATE AdditionalIncoming 
                            SET
                                DeletedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {id}");

                    return new GenericResponse<AdditionalIncoming>()
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
                return new GenericResponse<AdditionalIncoming>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }



        // Credit
        public async Task<GenericResponse<List<Credit>>> CreateCredit(List<Credit> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
                                    {e.StudyEconomicSituationId},
                                    '{e.Bank}',
                                    '{e.AccountNumber}',
                                    {e.CreditLimit},
                                    {e.Debt}
                                )";
                });


                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<Credit>($@"
                            INSERT INTO Credit (
	                            StudyEconomicSituationId,
	                            Bank,
	                            AccountNumber,
	                            CreditLimit,
                                Debt
                            )
                            OUTPUT INSERTED.*
                            VALUES {content}
                    ");

                    return new GenericResponse<List<Credit>>()
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
                return new GenericResponse<List<Credit>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<Credit>>> GetCredit(List<long> id)
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
                    var apiResponse = await conn.QueryAsync<Credit>($@"
                            SELECT 
                                c.* 
                            FROM
                                Credit c
                            WHERE 
                                c.DeletedAt IS NULL
                            AND c.Id IN ({extra})");

                    return new GenericResponse<List<Credit>>()
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
                return new GenericResponse<List<Credit>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Credit>> UpdateCredit(Credit request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Credit>($@"
                            UPDATE Credit 
                            SET
	                            Bank = '{request.Bank}',
	                            AccountNumber = '{request.AccountNumber}',
	                            CreditLimit = {request.CreditLimit},
	                            Debt = {request.Debt},
                                UpdatedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {request.Id}");

                    return new GenericResponse<Credit>()
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
                return new GenericResponse<Credit>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Credit>> DeleteCredit(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Credit>($@"
                            UPDATE Credit 
                            SET
                                DeletedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {id}");

                    return new GenericResponse<Credit>()
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
                return new GenericResponse<Credit>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }



        // Estate
        public async Task<GenericResponse<List<Estate>>> CreateEstate(List<Estate> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
                                    {e.StudyEconomicSituationId},
                                    '{e.Concept}',
                                    '{e.AcquisitionMethod}',
                                    '{e.AcquisitionDate}',
                                    '{e.Owner}',
                                    {e.PurchaseValue},
                                    {e.CurrentValue}
                                )";
                });


                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<Estate>($@"
                            INSERT INTO Estate (
	                            StudyEconomicSituationId,
	                            Concept,
	                            AcquisitionMethod,
	                            AcquisitionDate,
                                Owner,
                                PurchaseValue,
                                CurrentValue
                            )
                            OUTPUT INSERTED.*
                            VALUES {content}
                    ");

                    return new GenericResponse<List<Estate>>()
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
                return new GenericResponse<List<Estate>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<Estate>>> GetEstate(List<long> id)
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
                    var apiResponse = await conn.QueryAsync<Estate>($@"
                            SELECT 
                                e.* 
                            FROM
                                Estate e
                            WHERE 
                                e.DeletedAt IS NULL
                            AND e.Id IN ({extra})");

                    return new GenericResponse<List<Estate>>()
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
                return new GenericResponse<List<Estate>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Estate>> UpdateEstate(Estate request)
        {
            try
            {
                string query = $@"
                            UPDATE Estate 
                            SET
	                            Concept = '{request.Concept}',
	                            AcquisitionMethod = '{request.AcquisitionMethod}',
	                            AcquisitionDate = '{request.AcquisitionDate}',
	                            Owner = '{request.Owner}',
	                            PurchaseValue = {request.PurchaseValue},
	                            CurrentValue = {request.CurrentValue},
                                UpdatedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {request.Id}";
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Estate>(query);

                    return new GenericResponse<Estate>()
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
                return new GenericResponse<Estate>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Estate>> DeleteEstate(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Estate>($@"
                            UPDATE Estate 
                            SET
                                DeletedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {id}");

                    return new GenericResponse<Estate>()
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
                return new GenericResponse<Estate>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }



        // Vehicle
        public async Task<GenericResponse<List<Vehicle>>> CreateVehicle(List<Vehicle> request)
        {
            try
            {
                string content = string.Empty;
                request.ForEach(e =>
                {
                    if (!string.IsNullOrWhiteSpace(content)) content+=", ";
                    content+=$@"(
                                    {e.StudyEconomicSituationId},
                                    '{e.Plates}',
                                    '{e.SerialNumber}',
                                    '{e.BrandAndModel}',
                                    '{e.Owner}',
                                    {e.PurchaseValue},
                                    {e.CurrentValue}
                                )";
                });


                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<Vehicle>($@"
                            INSERT INTO Vehicle (
	                            StudyEconomicSituationId,
	                            Plates,
	                            SerialNumber,
	                            BrandAndModel,
                                Owner,
                                PurchaseValue,
                                CurrentValue
                            )
                            OUTPUT INSERTED.*
                            VALUES {content}
                    ");

                    return new GenericResponse<List<Vehicle>>()
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
                return new GenericResponse<List<Vehicle>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<Vehicle>>> GetVehicle(List<long> id)
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
                    var apiResponse = await conn.QueryAsync<Vehicle>($@"
                            SELECT 
                                v.* 
                            FROM
                                Vehicle v
                            WHERE 
                                v.DeletedAt IS NULL
                            AND v.Id IN ({extra})");

                    return new GenericResponse<List<Vehicle>>()
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
                return new GenericResponse<List<Vehicle>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Vehicle>> UpdateVehicle(Vehicle request)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Vehicle>($@"
                            UPDATE Vehicle 
                            SET
	                            Plates = '{request.Plates}',
	                            SerialNumber = '{request.SerialNumber}',
	                            BrandAndModel = '{request.BrandAndModel}',
	                            Owner = '{request.Owner}',
	                            PurchaseValue = {request.PurchaseValue},
	                            CurrentValue = {request.CurrentValue},
                                UpdatedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {request.Id}");

                    return new GenericResponse<Vehicle>()
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
                return new GenericResponse<Vehicle>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<Vehicle>> DeleteVehicle(long id)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Vehicle>($@"
                            UPDATE Vehicle 
                            SET
                                DeletedAt = '{DateTime.UtcNow}'
                            OUTPUT INSERTED.*
                            WHERE
                                Id = {id}");

                    return new GenericResponse<Vehicle>()
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
                return new GenericResponse<Vehicle>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
