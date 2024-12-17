using Dapper;
using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.DAL.Repositories
{
    public class FileRepository : BaseRepository, IFileRepository
    {
        private readonly IUtilitiesRepository _utilities;
        public FileRepository(IUtilitiesRepository _utilities, IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) { this._utilities=_utilities; }

        public async Task<GenericResponse<Doccument>> CreateFile(Doccument request, bool returnBase64Source = false)
        {
            try
            {
                string query = $@"
                        INSERT INTO Doccument (
	                        DoccumentName,
	                        DoccumentRoute,
	                        StoreMediaTypeId,
                            StoreFileTypeId)
                        OUTPUT INSERTED.*,
                        INSERTED.StoreMediaTypeId AS StoreMediaType,
                        INSERTED.StoreFileTypeId AS StoreFileType
                        VALUES (
	                        '{request.DoccumentName}',
	                        '{request.DoccumentRoute}',
	                        {(int)request.StoreMediaType},
	                        {(int)request.StoreFileType}                    
                            )";

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Doccument>(query);
                    apiResponse.DoccumentURL = _utilities.FileURLFormatter(apiResponse);
                    if (returnBase64Source)
                        apiResponse.Base64Doccument = request.Base64Doccument;
                    return new GenericResponse<Doccument>()
                    {
                        Response =  apiResponse,
                        StatusCode = System.Net.HttpStatusCode.Created,
                        Sucess = true
                    };
                }
            }
            catch (Exception exc)
            {
                return new GenericResponse<Doccument>()
                {
                    ErrorMessage =  exc.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<GenericResponse<Doccument>> DeleteFile(long Id)
        {
            try
            {
                string query = $@"
                        UPDATE Doccument SET DeletedAt = '{DateTime.UtcNow}' OUTPUT INSERTED.* WHERE Id = {Id};
                    ";

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Doccument>(query);
                    return new GenericResponse<Doccument>()
                    {
                        Response =  apiResponse,
                        StatusCode = System.Net.HttpStatusCode.Created,
                        Sucess = true
                    };
                }
            }
            catch (Exception exc)
            {
                return new GenericResponse<Doccument>()
                {
                    ErrorMessage =  exc.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<GenericResponse<List<Doccument>>> GetFile(List<long> Id)
        {
            string extra = string.Empty;
            Id.ForEach(e =>
            {
                if (!string.IsNullOrWhiteSpace(extra)) extra +=",";
                extra+=e;
            });
            try
            {
                string query = $@"
                        SELECT 
                            d.*, 
                            d.StoreMediaTypeId as StoreMediaType,
                            d.StoreFileTypeId as StoreFileType
                        FROM 
                            Doccument d
                        WHERE Id IN ({extra})
                    ";

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<Doccument>(query);

                    apiResponse.ToList().ForEach(e =>
                    {
                        e.DoccumentURL = _utilities.FileURLFormatter(e);
                    });
                    return new GenericResponse<List<Doccument>>()
                    {
                        Response =  apiResponse.ToList(),
                        StatusCode = System.Net.HttpStatusCode.Created,
                        Sucess = true
                    };
                }
            }
            catch (Exception exc)
            {
                return new GenericResponse<List<Doccument>>()
                {
                    ErrorMessage =  exc.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<GenericResponse<Doccument>> StoreFile(StoreMediaType place, Doccument media)
        {
            try
            {
                string fileExtension = string.Empty;
                switch (media.StoreFileType)
                {
                    case StoreFileType.PDF:
                        fileExtension=".pdf";
                        break;
                    default:
                        fileExtension=".txt";
                        break;
                }


                string path = string.Empty;
                string doccumentName = Guid.NewGuid().ToString()+fileExtension;
                switch (place)
                {
                    case StoreMediaType.EVIDENCE:
                        path = $@"{_utilities.baseFilePath}/{_utilities.EVIDENCE}";
                        break;
                    case StoreMediaType.PROFILE:
                        path = $@"{_utilities.baseFilePath}/{_utilities.PROFILE}";
                        break;
                    case StoreMediaType.GENERAL:
                    default:
                        path = $@"{_utilities.baseFilePath}/{_utilities.GENERAL}";
                        break;
                }
                System.IO.DirectoryInfo c = new DirectoryInfo(path);
                c.Create();

                using (FileStream fs = new FileStream(string.Format("{0}/{1}", path, doccumentName), FileMode.CreateNew))
                {
                    using(System.IO.BinaryWriter writer = new BinaryWriter(fs))
                    {
                        var bytes = Convert.FromBase64String(media.Base64Doccument);
                        writer.Write(bytes, 0, bytes.Length); 
                        writer.Close();
                    }
                }
                media.DoccumentName = doccumentName;
                media.DoccumentRoute = path.Replace(_utilities.baseFilePath, "");
                return new GenericResponse<Doccument>()
                {
                    Response = media,
                    Sucess = true,
                    StatusCode = System.Net.HttpStatusCode.Created
                };
            }
            catch (Exception e)
            {
                return new GenericResponse<Doccument>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Sucess = false,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}
