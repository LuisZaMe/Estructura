using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Estructura.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.BLL.Services
{
    public class StudyPicturesService : IStudyPicturesService
    {
        private readonly IStudyPicturesRepository _studyPicturesRepository;
        private readonly IMediaService _mediaService;
        public StudyPicturesService(IStudyPicturesRepository _studyPicturesRepository, IMediaService _mediaService)
        {
            this._studyPicturesRepository=_studyPicturesRepository;
            this._mediaService=_mediaService;
        }
        public async Task<GenericResponse<StudyPictures>> CreateStudyPictures(StudyPictures request)
        {
            if (request.StudyId==0)
                return new GenericResponse<StudyPictures>()
                {
                    ErrorMessage = "Study id required",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            List<Task<GenericResponse<Media>>> taskList = new List<Task<GenericResponse<Media>>>();
            if (request.Media1!=null&&!string.IsNullOrWhiteSpace(request.Media1.Base64Image))
            {
                request.Media1.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media1);
                request.Media1.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }
            if (request.Media2!=null&&!string.IsNullOrWhiteSpace(request.Media2.Base64Image))
            {
                request.Media2.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media2);
                request.Media2.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }
            if (request.Media3!=null&&!string.IsNullOrWhiteSpace(request.Media3.Base64Image))
            {
                request.Media3.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media3);
                request.Media3.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }
            if (request.Media4!=null&&!string.IsNullOrWhiteSpace(request.Media4.Base64Image))
            {
                request.Media4.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media4);
                request.Media4.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }
            if (request.Media5!=null&&!string.IsNullOrWhiteSpace(request.Media5.Base64Image))
            {
                request.Media5.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media5);
                request.Media5.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }
            if (request.Media6!=null&&!string.IsNullOrWhiteSpace(request.Media6.Base64Image))
            {
                request.Media6.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media6);
                request.Media6.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }


            if (taskList.Count>0)
            {
                while (taskList.Count>0)
                {
                    var current = await Task.WhenAny(taskList);
                    if (current.IsFaulted)
                    {
                        return new GenericResponse<StudyPictures>()
                        {
                            ErrorMessage = "Error uploading images, verify data",
                            StatusCode = System.Net.HttpStatusCode.InternalServerError
                        };
                    }
                    if (request.Media1?.Id==current.Id)
                        request.Media1 = current.Result.Response;
                    if (request.Media2?.Id==current.Id)
                        request.Media2 = current.Result.Response;
                    if (request.Media3?.Id==current.Id)
                        request.Media3 = current.Result.Response;
                    if (request.Media4?.Id==current.Id)
                        request.Media4 = current.Result.Response;
                    if (request.Media5?.Id==current.Id)
                        request.Media5 = current.Result.Response;
                    if (request.Media6?.Id==current.Id)
                        request.Media6 = current.Result.Response;
                    taskList.Remove(current);
                }
            }

            var respone = await _studyPicturesRepository.CreateStudyPictures(request);
            if (respone.Sucess)
            {
                var items = await GetStudyPictures(new List<long>() { respone.Response.Id });
                if (items.Sucess)
                    respone.Response =items.Response.FirstOrDefault();
            }
            return respone;
        }

        public async Task<GenericResponse<StudyPictures>> DeleteStudyPictures(long id)
        {
            return await _studyPicturesRepository.DeleteStudyPictures(id);
        }

        public async Task<GenericResponse<List<StudyPictures>>> GetStudyPictures(List<long> id, bool byStudy = false)
        {
            var response= await _studyPicturesRepository.GetStudyPictures(id, byStudy);
            List<long> mediaIdList = new List<long>();
            if (mediaIdList.Count > 0)
            {
                response.Response.ForEach(e =>
                {
                    if (e.Media1?.Id != 0)
                        mediaIdList.Add(e.Media1.Id);
                    if (e.Media2?.Id != 0)
                        mediaIdList.Add(e.Media2.Id);
                    if (e.Media3?.Id != 0)
                        mediaIdList.Add(e.Media3.Id);
                    if (e.Media4?.Id != 0)
                        mediaIdList.Add(e.Media4.Id);
                    if (e.Media5?.Id != 0)
                        mediaIdList.Add(e.Media5.Id);
                    if (e.Media6?.Id != 0)
                        mediaIdList.Add(e.Media6.Id);
                });
            }

            var mediaItems = await _mediaService.GetMedia(mediaIdList);
            if (mediaItems.Sucess)
            {
                response.Response.ForEach(e =>
                {
                    e.Media1 = e.Media1.Id == 0 ? e.Media1 : mediaItems.Response.FirstOrDefault(f => f.Id == e.Media1.Id);
                    e.Media2 = e.Media2.Id == 0 ? e.Media2 : mediaItems.Response.FirstOrDefault(f => f.Id == e.Media2.Id);
                    e.Media3 = e.Media3.Id == 0 ? e.Media3 : mediaItems.Response.FirstOrDefault(f => f.Id == e.Media3.Id);
                    e.Media4 = e.Media4.Id == 0 ? e.Media4 : mediaItems.Response.FirstOrDefault(f => f.Id == e.Media4.Id);
                    e.Media5 = e.Media5.Id == 0 ? e.Media5 : mediaItems.Response.FirstOrDefault(f => f.Id == e.Media5.Id);
                    e.Media6 = e.Media6.Id == 0 ? e.Media6 : mediaItems.Response.FirstOrDefault(f => f.Id == e.Media6.Id);
                });
            }

            return response;
        }

        public async Task<GenericResponse<StudyPictures>> UpdateStudyPictures(StudyPictures request)
        {
            var currentList = await GetStudyPictures(new List<long>() { request.Id });
            if (!currentList.Sucess)
                return new GenericResponse<StudyPictures>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

            var currentMedia = currentList.Response.FirstOrDefault();
            List<Task<GenericResponse<Media>>> taskList = new List<Task<GenericResponse<Media>>>();

            if (request.Media1!=null && request.Media1.Id==0 && !string.IsNullOrWhiteSpace(request.Media1.Base64Image))
            {
                request.Media1.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media1);
                request.Media1.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }
            if (request.Media2!=null &&request.Media2.Id==0&&!string.IsNullOrWhiteSpace(request.Media2.Base64Image))
            {
                request.Media2.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media2);
                request.Media2.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }
            if (request.Media3!=null &&request.Media3.Id==0&&!string.IsNullOrWhiteSpace(request.Media3.Base64Image))
            {
                request.Media3.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media3);
                request.Media3.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }
            if (request.Media4!=null &&request.Media4.Id==0&&!string.IsNullOrWhiteSpace(request.Media4.Base64Image))
            {
                request.Media4.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media4);
                request.Media4.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }
            if (request.Media5!=null &&request.Media5.Id==0&&!string.IsNullOrWhiteSpace(request.Media5.Base64Image))
            {
                request.Media5.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media5);
                request.Media5.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }
            if (request.Media6!=null &&request.Media6.Id==0&&!string.IsNullOrWhiteSpace(request.Media6.Base64Image))
            {
                request.Media6.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> Media1Task = _mediaService.CreateMedia(request.Media6);
                request.Media6.Id = Media1Task.Id;
                taskList.Add(Media1Task);
            }

            if (taskList.Count>0)
            {
                while (taskList.Count>0)
                {
                    var current = await Task.WhenAny(taskList);
                    if (current.IsFaulted)
                    {
                        return new GenericResponse<StudyPictures>()
                        {
                            ErrorMessage = "Error uploading images, verify data",
                            StatusCode = System.Net.HttpStatusCode.InternalServerError
                        };
                    }
                    if (request.Media1?.Id==current.Id)
                        request.Media1 = current.Result.Response;
                    if (request.Media2?.Id==current.Id)
                        request.Media2 = current.Result.Response;
                    if (request.Media3?.Id==current.Id)
                        request.Media3 = current.Result.Response;
                    if (request.Media4?.Id==current.Id)
                        request.Media4 = current.Result.Response;
                    if (request.Media5?.Id==current.Id)
                        request.Media5 = current.Result.Response;
                    if (request.Media6?.Id==current.Id)
                        request.Media6 = current.Result.Response;
                    taskList.Remove(current);
                }
            }

            request.Media1 = request.Media1 == null || request.Media1.Id == 0 ? currentMedia.Media1 : request.Media1;
            request.Media2 = request.Media2 == null || request.Media2.Id == 0 ? currentMedia.Media2 : request.Media2;
            request.Media3 = request.Media3 == null || request.Media3.Id == 0 ? currentMedia.Media3 : request.Media3;
            request.Media4 = request.Media4 == null || request.Media4.Id == 0 ? currentMedia.Media4 : request.Media4;
            request.Media5 = request.Media5 == null || request.Media5.Id == 0 ? currentMedia.Media5 : request.Media5;
            request.Media6 = request.Media6 == null || request.Media6.Id == 0 ? currentMedia.Media6 : request.Media6;

            var response = await _studyPicturesRepository.UpdateStudyPictures(request);
            if (response.Sucess)
            {
                currentList = await GetStudyPictures(new List<long>() { request.Id });
                response.Response = currentList.Response.FirstOrDefault();
            }
            return response;
        }
    }
}
