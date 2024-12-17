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
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor)
        {

        }

        public async Task<GenericResponse<CompanyInformation>> AddCompanyInformation(CompanyInformation request)
        {
            var commonError = new GenericResponse<Identity>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<CompanyInformation, long, CompanyInformation>($@"

                        INSERT INTO CompanyInformation (
                            UserId,
                            CompanyName,
                            CompanyPhone,
                            RazonSocial,
                            RFC,
                            DireccionFiscal,
                            RegimenFiscal,
                            PaymentMethodId)
                        OUTPUT 
                            INSERTED.*,
                            INSERTED.PaymentMethodId as pmId
                        VALUES (
                            {request.UserId},
                            '{request.CompanyName}',
                            '{request.CompanyPhone}',
                            '{request.RazonSocial}',
                            '{request.RFC}',
                            '{request.DireccionFiscal}',
                            '{request.RegimenFiscal}',
                            {request.Payment.Id}
                        )
                    ", (companyInformation, mehtodId) =>
                    {
                        companyInformation.Payment = new PaymentMethod()
                        {
                            Id= mehtodId
                        };
                        return companyInformation;
                    }, splitOn: "Id, pmId");

                    return new GenericResponse<CompanyInformation>()
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
                return new GenericResponse<CompanyInformation>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<CompanyInformation>>> GetCompanyInformation(List<long> Id, int currentPage, int offset)
        {
            var commonError = new GenericResponse<Identity>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            string extra = string.Empty;
            Id.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(extra)) extra+=", ";
                extra+=e;
            });

            if (!string.IsNullOrWhiteSpace(extra))
                extra=$@"WHERE ci.Id IN ({extra})";

            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<CompanyInformation, PaymentMethod, CompanyInformation>($@"
                        SELECT
                            ci.*,
                            pm.*
                        FROM CompanyInformation ci
                        LEFT JOIN PaymentMethod pm
	                        on ci.PaymentMethodId = pm.Id
                        {extra}
                        ORDER BY ci.Id
                        OFFSET {currentPage * offset} ROWS 
                        FETCH NEXT {offset} ROWS ONLY
                    ", (companyInformation, mehtodId) =>
                    {
                        mehtodId.Method = (Common.Enums.PaymentMethods)mehtodId.Id;
                        companyInformation.Payment =mehtodId;
                        return companyInformation;
                    }, splitOn: "Id, pmId");

                    return new GenericResponse<List<CompanyInformation>>()
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
                return new GenericResponse<List<CompanyInformation>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<List<CompanyInformation>>> GetCompanyInformation(List<long> userId)
        {
            var commonError = new GenericResponse<Identity>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };

            string extra = string.Empty;
            userId.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(extra)) extra+=", ";
                extra+=e;
            });

            if (!string.IsNullOrWhiteSpace(extra))
                extra=$@"WHERE ci.UserId IN ({extra})";

            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<CompanyInformation, PaymentMethod, CompanyInformation>($@"
                        SELECT
                            ci.*,
                            pm.*
                        FROM CompanyInformation ci
                        LEFT JOIN PaymentMethod pm
	                        on ci.PaymentMethodId = pm.Id
                        {extra}
                        ORDER BY ci.Id
                    ", (companyInformation, mehtodId) =>
                    {
                        mehtodId.Method = (Common.Enums.PaymentMethods)mehtodId.Id;
                        companyInformation.Payment =mehtodId;
                        return companyInformation;
                    }, splitOn: "Id, Id");

                    return new GenericResponse<List<CompanyInformation>>()
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
                return new GenericResponse<List<CompanyInformation>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }

        public async Task<GenericResponse<CompanyInformation>> UpdateComanyInformation(CompanyInformation request)
        {
            var commonError = new GenericResponse<Identity>()
            {
                ErrorMessage = "Unhandled error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Sucess = false
            };
            
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<CompanyInformation, PaymentMethod, CompanyInformation>($@"
                        UPDATE CompanyInformation 
                        SET 
                            CompanyName = '{request.CompanyName}',
                            CompanyPhone = '{request.CompanyPhone}',
                            RazonSocial = '{request.RazonSocial}',
                            RFC = '{request.RFC}',
                            DireccionFiscal = '{request.DireccionFiscal}',
                            RegimenFiscal = '{request.RegimenFiscal}',
                            PaymentMethodId = {request.Payment?.Id}
                        OUTPUT 
                            INSERTED.*,
                            INSERTED.PaymentMethodId as pmId
                        WHERE Id = {request.Id}
                    ", (companyInformation, mehtodId) =>
                    {
                        companyInformation.Payment = mehtodId;
                        return companyInformation;
                    }, splitOn: "Id, pmId");

                    return new GenericResponse<CompanyInformation>()
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
                return new GenericResponse<CompanyInformation>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = exc.Message
                };
            }
        }
    }
}
