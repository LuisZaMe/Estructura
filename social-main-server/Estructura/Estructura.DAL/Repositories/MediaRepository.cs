using Dapper;
using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.DAL.Repositories
{
    public class MediaRepository : BaseRepository, IMediaRepository
    {
        private readonly IUtilitiesRepository _utilities;
        public MediaRepository(IUtilitiesRepository _utilities, IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor) : base(options, httpAccesor) { this._utilities=_utilities; }
        public async Task<GenericResponse<Media>> CreateMedia(Media request)
        {
            try
            {
                string query = $@"
                        INSERT INTO Media (
	                        ImageName,
	                        ImageRoute,
	                        StoreMediaTypeId)
                        OUTPUT INSERTED.*
                        VALUES (
	                        '{request.ImageName}',
	                        '{request.ImageRoute}',
	                        {(int)request.StoreMediaType})
                    ";

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Media>(query);
                    apiResponse.MediaURL = _utilities.ImageURLFormatter(apiResponse);
                    return new GenericResponse<Media>()
                    {
                        Response =  apiResponse,
                        StatusCode = System.Net.HttpStatusCode.Created,
                        Sucess = true
                    };
                }
            }
            catch (Exception exc)
            {
                return new GenericResponse<Media>()
                {
                    ErrorMessage =  exc.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<GenericResponse<Media>> DeleteMedia(long Id)
        {
            try
            {
                string query = $@"
                        UPDATE Media SET DeletedAt = '{DateTime.UtcNow}' OUTPUT INSERTED.* WHERE Id = {Id};
                    ";

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QuerySingleAsync<Media>(query);
                    return new GenericResponse<Media>()
                    {
                        Response =  apiResponse,
                        StatusCode = System.Net.HttpStatusCode.Created,
                        Sucess = true
                    };
                }
            }
            catch (Exception exc)
            {
                return new GenericResponse<Media>()
                {
                    ErrorMessage =  exc.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<GenericResponse<List<Media>>> GetMedia(List<long> Id)
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
                        SELECT m.*, m.StoreMediaTypeId as StoreMediaType FROM Media m
                        WHERE Id IN ({extra})
                    ";

                using (var conn = Connection)
                {
                    conn.Open();
                    var apiResponse = await conn.QueryAsync<Media>(query);

                    apiResponse.ToList().ForEach(e =>
                    {
                        e.MediaURL = _utilities.ImageURLFormatter(e);
                    });
                    return new GenericResponse<List<Media>>()
                    {
                        Response =  apiResponse.ToList(),
                        StatusCode = System.Net.HttpStatusCode.Created,
                        Sucess = true
                    };
                }
            }
            catch (Exception exc)
            {
                return new GenericResponse<List<Media>>()
                {
                    ErrorMessage =  exc.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<GenericResponse<Media>> StoreMedia(StoreMediaType place, Media media)
        {
            try
            {
                string path = string.Empty;
                string imageName = Guid.NewGuid().ToString()+".jpg";
                switch (place)
                {
                    case StoreMediaType.EVIDENCE:
                        path = $@"{_utilities.basePath}/{_utilities.EVIDENCE}";
                        break;
                    case StoreMediaType.PROFILE:
                        path = $@"{_utilities.basePath}/{_utilities.PROFILE}";
                        break;
                    case StoreMediaType.GENERAL:
                    default:
                        path = $@"{ _utilities.basePath}/{_utilities.GENERAL}";
                        break;
                }
                //System.IO.DirectoryInfo.
                System.IO.DirectoryInfo c = new DirectoryInfo(path);
                c.Create();
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(media.Base64Image)))
                {
                    using (Bitmap bm2 = new Bitmap(ms))
                    {
                        bm2.Save(string.Format("{0}/{1}", path, imageName));
                    }
                }

                media.ImageName = imageName;
                media.ImageRoute = path.Replace(_utilities.basePath, "");
                return new GenericResponse<Media>()
                {
                    Response = media,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Sucess = true
                };
            }
            catch (Exception e)
            {
                return new GenericResponse<Media>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Sucess = false,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}
