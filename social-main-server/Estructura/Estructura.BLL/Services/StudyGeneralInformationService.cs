using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Estructura.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estructura.BLL.Services
{
    public class StudyGeneralInformationService : IStudyGeneralInformationService
    {
        private readonly IStudyGeneralInformationRepository _studyGeneralInformationRepository;
        private readonly IMediaService _mediaService;
        public StudyGeneralInformationService(IStudyGeneralInformationRepository _studyGeneralInformationRepository, IMediaService _mediaService)
        {
            this._studyGeneralInformationRepository=_studyGeneralInformationRepository;
            this._mediaService=_mediaService;
        }

        //Study general information
        public async Task<GenericResponse<StudyGeneralInformation>> CreateStudyGeneralInformation(StudyGeneralInformation request)
        {
            var genericError = new GenericResponse<StudyGeneralInformation>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
            string ErrorMediaRequest = string.Empty;
            if (request.StudyId ==0)
                ErrorMediaRequest+= "StudyId is required\r\n";
            if(string.IsNullOrWhiteSpace(request.Name))
                ErrorMediaRequest+= "Name is required\r\n";
            if(string.IsNullOrWhiteSpace(request.Email))
                ErrorMediaRequest+= "Email is required\r\n";
            if(request.BornCity == null ||request.BornCity.Id ==0)
                ErrorMediaRequest+= "BornCity is required\r\n";
            if(request.BornState == null ||request.BornState.Id ==0)
                ErrorMediaRequest+= "BornState is required\r\n";
            if(request.BornDate == new DateTime())
                ErrorMediaRequest+= "BornDate is required\r\n";
            if(request.MaritalStatus == Common.Enums.MaritalStatus.NONE)
                ErrorMediaRequest+= "MaritalStatus is required\r\n";
            if (request.City == null ||request.City.Id ==0)
                ErrorMediaRequest+= "City is required\r\n";
            if (request.State == null ||request.State.Id ==0)
                ErrorMediaRequest+= "BornState is required\r\n";
           

            if (!string.IsNullOrWhiteSpace(ErrorMediaRequest))
            {
                genericError.ErrorMessage = ErrorMediaRequest;
                return genericError;
            }


            List<Task<GenericResponse<Media>>> mediaArray = new List<Task<GenericResponse<Media>>>();
            if (request.INEFrontMedia != null && !string.IsNullOrWhiteSpace(request.INEFrontMedia.Base64Image))
            {
                request.INEFrontMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.INEFrontMedia);
                request.INEFrontMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.INEBackMedia != null && !string.IsNullOrWhiteSpace(request.INEBackMedia.Base64Image))
            {
                request.INEBackMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.INEBackMedia);
                request.INEBackMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.AddressProofMedia != null && !string.IsNullOrWhiteSpace(request.AddressProofMedia.Base64Image))
            {
                request.AddressProofMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.AddressProofMedia);
                request.AddressProofMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.BornCertificateMedia != null && !string.IsNullOrWhiteSpace(request.BornCertificateMedia.Base64Image))
            {
                request.BornCertificateMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.BornCertificateMedia);
                request.BornCertificateMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.CURPMedia != null && !string.IsNullOrWhiteSpace(request.CURPMedia.Base64Image))
            {
                request.CURPMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.CURPMedia);
                request.CURPMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.StudiesProofMedia != null && !string.IsNullOrWhiteSpace(request.StudiesProofMedia.Base64Image))
            {
                request.StudiesProofMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.StudiesProofMedia);
                request.StudiesProofMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.SocialSecurityProofMedia != null && !string.IsNullOrWhiteSpace(request.SocialSecurityProofMedia.Base64Image))
            {
                request.SocialSecurityProofMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.SocialSecurityProofMedia);
                request.SocialSecurityProofMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.PresentedIdentificationMedia != null && !string.IsNullOrWhiteSpace(request.PresentedIdentificationMedia.Base64Image))
            {
                request.PresentedIdentificationMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.PresentedIdentificationMedia);
                request.PresentedIdentificationMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.VerificationMedia != null && !string.IsNullOrWhiteSpace(request.VerificationMedia.Base64Image))
            {
                request.VerificationMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.VerificationMedia);
                request.VerificationMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.MilitaryLetterMedia != null && !string.IsNullOrWhiteSpace(request.MilitaryLetterMedia.Base64Image))
            {
                request.MilitaryLetterMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.MilitaryLetterMedia);
                request.MilitaryLetterMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.RFCMedia != null && !string.IsNullOrWhiteSpace(request.RFCMedia.Base64Image))
            {
                request.RFCMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.RFCMedia);
                request.RFCMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.CriminalRecordMedia != null && !string.IsNullOrWhiteSpace(request.CriminalRecordMedia.Base64Image))
            {
                request.CriminalRecordMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.CriminalRecordMedia);
                request.CriminalRecordMedia.Id = task.Id;
                mediaArray.Add(task);
            }


            while (mediaArray.Count > 0)
            {
                var current = await Task.WhenAny(mediaArray);
                if(current.IsFaulted)
                {
                    return new GenericResponse<StudyGeneralInformation>()
                    {
                        ErrorMessage = "Error uploading media, verify data",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }

                if (request.INEFrontMedia!=null&&request.INEFrontMedia.Id == current.Id)
                    request.INEFrontMedia = current.Result.Response;
                else if (request.INEBackMedia!=null&&request.INEBackMedia.Id == current.Id)
                    request.INEBackMedia = current.Result.Response;
                else if (request.AddressProofMedia!=null&&request.AddressProofMedia.Id == current.Id)
                    request.AddressProofMedia = current.Result.Response;
                else if (request.BornCertificateMedia!=null&&request.BornCertificateMedia.Id == current.Id)
                    request.BornCertificateMedia = current.Result.Response;
                else if (request.CURPMedia!=null&&request.CURPMedia.Id == current.Id)
                    request.CURPMedia = current.Result.Response;
                else if (request.StudiesProofMedia!=null&&request.StudiesProofMedia.Id == current.Id)
                    request.StudiesProofMedia = current.Result.Response;
                else if (request.SocialSecurityProofMedia!=null&&request.SocialSecurityProofMedia.Id == current.Id)
                    request.SocialSecurityProofMedia = current.Result.Response;
                else if (request.PresentedIdentificationMedia!=null&&request.PresentedIdentificationMedia.Id == current.Id)
                    request.PresentedIdentificationMedia = current.Result.Response;
                else if (request.VerificationMedia!=null&&request.VerificationMedia.Id == current.Id)
                    request.VerificationMedia = current.Result.Response;
                else if (request.MilitaryLetterMedia!=null&&request.MilitaryLetterMedia.Id == current.Id)
                    request.MilitaryLetterMedia = current.Result.Response;
                else if (request.RFCMedia!=null&&request.RFCMedia.Id == current.Id)
                    request.RFCMedia = current.Result.Response;
                else if (request.CriminalRecordMedia!=null&&request.CriminalRecordMedia.Id == current.Id)
                    request.CriminalRecordMedia = current.Result.Response;

                mediaArray.Remove(current);
            }

            var result = await _studyGeneralInformationRepository.CreateStudyGeneralInformation(request);
            if(result!=null&&result.Sucess)
            {
                //Recommendation cards                
                if (request.RecommendationCards!=null&&request.RecommendationCards.Count>0)
                {
                    request.RecommendationCards.ForEach(e => e.StudyGeneralInformationId= result.Response.Id);
                    await CreateRecommendationCard(request.RecommendationCards);
                }

                var response = await GetStudyGeneralInformation(new List<long>() { result.Response.Id });
                result.Response = response.Response.FirstOrDefault();
            }
            return result;
        }

        public async Task<GenericResponse<StudyGeneralInformation>> DeleteStudyGeneralInformation(long id)
        {
            return await _studyGeneralInformationRepository.DeleteStudyGeneralInformation(id);
        }

        public async Task<GenericResponse<List<StudyGeneralInformation>>> GetStudyGeneralInformation(List<long> id, bool byStudy = false)
        {
            var studies = await _studyGeneralInformationRepository.GetStudyGeneralInformation(id, byStudy);

            if (studies==null||!studies.Sucess)
                return new GenericResponse<List<StudyGeneralInformation>>()
                {
                    ErrorMessage = "Error getting study general information",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };

            List<long> mediaIds = new List<long>();
            studies.Response.ForEach(e=>
            {
                if (e.INEFrontMedia!=null&&e.INEFrontMedia.Id!=0)
                    mediaIds.Add(e.INEFrontMedia.Id);
                if (e.INEBackMedia!=null&&e.INEBackMedia.Id!=0)
                    mediaIds.Add(e.INEBackMedia.Id);
                if (e.AddressProofMedia!=null&&e.AddressProofMedia.Id!=0)
                    mediaIds.Add(e.AddressProofMedia.Id);
                if (e.BornCertificateMedia!=null&&e.BornCertificateMedia.Id!=0)
                    mediaIds.Add(e.BornCertificateMedia.Id);
                if (e.CURPMedia!=null&&e.CURPMedia.Id!=0)
                    mediaIds.Add(e.CURPMedia.Id);
                if (e.StudiesProofMedia!=null&&e.StudiesProofMedia.Id!=0)
                    mediaIds.Add(e.StudiesProofMedia.Id);
                if (e.SocialSecurityProofMedia!=null&&e.SocialSecurityProofMedia.Id!=0)
                    mediaIds.Add(e.SocialSecurityProofMedia.Id);
                if (e.PresentedIdentificationMedia!=null&&e.PresentedIdentificationMedia.Id!=0)
                    mediaIds.Add(e.PresentedIdentificationMedia.Id);
                if (e.VerificationMedia!=null&&e.VerificationMedia.Id!=0)
                    mediaIds.Add(e.VerificationMedia.Id);
                if (e.RFCMedia!=null&&e.RFCMedia.Id!=0)
                    mediaIds.Add(e.RFCMedia.Id);
                if (e.MilitaryLetterMedia!=null&&e.MilitaryLetterMedia.Id!=0)
                    mediaIds.Add(e.MilitaryLetterMedia.Id);
                if (e.CriminalRecordMedia!=null&&e.CriminalRecordMedia.Id!=0)
                    mediaIds.Add(e.CriminalRecordMedia.Id);
            });

            if (mediaIds.Count>0)
            {
                var mediaItems = await _mediaService.GetMedia(mediaIds);
                studies.Response.ForEach(s =>
                {
                    s.INEFrontMedia = s.INEFrontMedia==null||s.INEFrontMedia.Id==0? new Media(): mediaItems.Response.First(mi => mi.Id == s.INEFrontMedia.Id);
                    s.INEBackMedia =  s.INEBackMedia==null||s.INEBackMedia.Id==0 ? new Media() : mediaItems.Response.First(mi => mi.Id == s.INEBackMedia.Id);
                    s.AddressProofMedia=  s.AddressProofMedia==null||s.AddressProofMedia.Id==0 ? new Media() : mediaItems.Response.First(mi => mi.Id == s.AddressProofMedia.Id);
                    s.BornCertificateMedia=  s.BornCertificateMedia==null||s.BornCertificateMedia.Id==0 ? new Media() : mediaItems.Response.First(mi => mi.Id == s.BornCertificateMedia.Id);
                    s.CURPMedia=  s.CURPMedia==null||s.CURPMedia.Id==0 ? new Media() : mediaItems.Response.First(mi => mi.Id == s.CURPMedia.Id);
                    s.StudiesProofMedia= s.StudiesProofMedia==null||s.StudiesProofMedia.Id==0 ? new Media() : mediaItems.Response.First(mi => mi.Id == s.StudiesProofMedia.Id);
                    s.SocialSecurityProofMedia= s.SocialSecurityProofMedia==null||s.SocialSecurityProofMedia.Id==0 ? new Media() : mediaItems.Response.First(mi => mi.Id == s.SocialSecurityProofMedia.Id);
                    s.PresentedIdentificationMedia= s.PresentedIdentificationMedia==null||s.PresentedIdentificationMedia.Id==0 ? new Media() : mediaItems.Response.First(mi => mi.Id == s.PresentedIdentificationMedia.Id);
                    s.VerificationMedia= s.VerificationMedia==null||s.VerificationMedia.Id==0 ? new Media() : mediaItems.Response.First(mi => mi.Id == s.VerificationMedia.Id);
                    s.MilitaryLetterMedia= s.MilitaryLetterMedia==null||s.MilitaryLetterMedia.Id==0 ? new Media() : mediaItems.Response.First(mi => mi.Id == s.MilitaryLetterMedia.Id);
                    s.RFCMedia= s.RFCMedia==null||s.RFCMedia.Id==0 ? new Media() : mediaItems.Response.First(mi => mi.Id == s.RFCMedia.Id);
                    s.CriminalRecordMedia= s.CriminalRecordMedia==null||s.CriminalRecordMedia.Id==0 ? new Media() : mediaItems.Response.First(mi => mi.Id == s.CriminalRecordMedia.Id);
                });
            }

            return studies;
        }

        public async Task<GenericResponse<StudyGeneralInformation>> UpdateStudyGeneralInformation(StudyGeneralInformation request)
        {
            var error = new GenericResponse<StudyGeneralInformation>() { StatusCode = System.Net.HttpStatusCode.BadRequest };
            var items = await GetStudyGeneralInformation(new List<long>() { request.Id });
            if(items==null||!items.Sucess || items.Response.Count==0)
            {
                error.ErrorMessage = "Item for update not found";
                return error;
            }


            List<Task<GenericResponse<Media>>> mediaArray = new List<Task<GenericResponse<Media>>>();

            if (request.INEFrontMedia != null && !string.IsNullOrWhiteSpace(request.INEFrontMedia.Base64Image) && request.INEFrontMedia.Id == 0)
            {
                request.INEFrontMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.INEFrontMedia);
                request.INEFrontMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.INEBackMedia != null && !string.IsNullOrWhiteSpace(request.INEBackMedia.Base64Image) && request.INEBackMedia.Id == 0)
            {
                request.INEBackMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.INEBackMedia);
                request.INEBackMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.AddressProofMedia != null && !string.IsNullOrWhiteSpace(request.AddressProofMedia.Base64Image) && request.AddressProofMedia.Id == 0)
            {
                request.AddressProofMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.AddressProofMedia);
                request.AddressProofMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.BornCertificateMedia != null && !string.IsNullOrWhiteSpace(request.BornCertificateMedia.Base64Image) && request.BornCertificateMedia.Id == 0)
            {
                request.BornCertificateMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.BornCertificateMedia);
                request.BornCertificateMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.CURPMedia != null && !string.IsNullOrWhiteSpace(request.CURPMedia.Base64Image) && request.CURPMedia.Id == 0)
            {
                request.CURPMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.CURPMedia);
                request.CURPMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.StudiesProofMedia != null && !string.IsNullOrWhiteSpace(request.StudiesProofMedia.Base64Image) && request.StudiesProofMedia.Id == 0)
            {
                request.StudiesProofMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.StudiesProofMedia);
                request.StudiesProofMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.SocialSecurityProofMedia != null && !string.IsNullOrWhiteSpace(request.SocialSecurityProofMedia.Base64Image) && request.SocialSecurityProofMedia.Id == 0)
            {
                request.SocialSecurityProofMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.SocialSecurityProofMedia);
                request.SocialSecurityProofMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.PresentedIdentificationMedia != null && !string.IsNullOrWhiteSpace(request.PresentedIdentificationMedia.Base64Image) && request.PresentedIdentificationMedia.Id == 0)
            {
                request.PresentedIdentificationMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.PresentedIdentificationMedia);
                request.PresentedIdentificationMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.VerificationMedia != null && !string.IsNullOrWhiteSpace(request.VerificationMedia.Base64Image) && request.VerificationMedia.Id == 0)
            {
                request.VerificationMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.VerificationMedia);
                request.VerificationMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.MilitaryLetterMedia != null && !string.IsNullOrWhiteSpace(request.MilitaryLetterMedia.Base64Image) && request.MilitaryLetterMedia.Id == 0)
            {
                request.MilitaryLetterMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.MilitaryLetterMedia);
                request.MilitaryLetterMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.RFCMedia != null && !string.IsNullOrWhiteSpace(request.RFCMedia.Base64Image) && request.RFCMedia.Id == 0)
            {
                request.RFCMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.RFCMedia);
                request.RFCMedia.Id = task.Id;
                mediaArray.Add(task);
            }
            if (request.CriminalRecordMedia != null && !string.IsNullOrWhiteSpace(request.CriminalRecordMedia.Base64Image) && request.CriminalRecordMedia.Id == 0)
            {
                request.CriminalRecordMedia.StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE;
                Task<GenericResponse<Media>> task = _mediaService.CreateMedia(request.CriminalRecordMedia);
                request.CriminalRecordMedia.Id = task.Id;
                mediaArray.Add(task);
            }

            while (mediaArray.Count > 0)
            {
                var currentTask = await Task.WhenAny(mediaArray);
                if (currentTask.IsFaulted)
                {
                    return new GenericResponse<StudyGeneralInformation>()
                    {
                        ErrorMessage = "Error uploading media, verify data",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }

                if (request.INEFrontMedia!=null&&request.INEFrontMedia.Id == currentTask.Id)
                    request.INEFrontMedia = currentTask.Result.Response;
                else if (request.INEBackMedia!=null&&request.INEBackMedia.Id == currentTask.Id)
                    request.INEBackMedia = currentTask.Result.Response;
                else if (request.AddressProofMedia!=null&&request.AddressProofMedia.Id == currentTask.Id)
                    request.AddressProofMedia = currentTask.Result.Response;
                else if (request.BornCertificateMedia!=null&&request.BornCertificateMedia.Id == currentTask.Id)
                    request.BornCertificateMedia = currentTask.Result.Response;
                else if (request.CURPMedia!=null&&request.CURPMedia.Id == currentTask.Id)
                    request.CURPMedia = currentTask.Result.Response;
                else if (request.StudiesProofMedia!=null&&request.StudiesProofMedia.Id == currentTask.Id)
                    request.StudiesProofMedia = currentTask.Result.Response;
                else if (request.SocialSecurityProofMedia!=null&&request.SocialSecurityProofMedia.Id == currentTask.Id)
                    request.SocialSecurityProofMedia = currentTask.Result.Response;
                else if (request.PresentedIdentificationMedia!=null&&request.PresentedIdentificationMedia.Id == currentTask.Id)
                    request.PresentedIdentificationMedia = currentTask.Result.Response;
                else if (request.VerificationMedia!=null&&request.VerificationMedia.Id == currentTask.Id)
                    request.VerificationMedia = currentTask.Result.Response;
                else if (request.MilitaryLetterMedia!=null&&request.MilitaryLetterMedia.Id == currentTask.Id)
                    request.MilitaryLetterMedia = currentTask.Result.Response;
                else if (request.RFCMedia!=null&&request.RFCMedia.Id == currentTask.Id)
                    request.RFCMedia = currentTask.Result.Response;
                else if (request.CriminalRecordMedia!=null&&request.CriminalRecordMedia.Id == currentTask.Id)
                    request.CriminalRecordMedia = currentTask.Result.Response;

                mediaArray.Remove(currentTask);
            }


            var current = items.Response.First();
            request.Name = string.IsNullOrWhiteSpace(request.Name)?current.Name: request.Name;
            request.Email = string.IsNullOrWhiteSpace(request.Email)?current.Email: request.Email;  
            request.TimeOnComany = string.IsNullOrWhiteSpace(request.TimeOnComany)?current.TimeOnComany: request.TimeOnComany;
            request.EmployeeNumber = String.IsNullOrWhiteSpace(request.EmployeeNumber)?current.EmployeeNumber: request.EmployeeNumber;
            request.BornCity = request.BornCity==null||request.BornCity.Id==0 ? current.BornCity : request.BornCity;
            request.BornState = request.BornState==null||request.BornState.Id==0 ? current.BornState : request.BornState;
            request.CountryName = String.IsNullOrWhiteSpace(request.CountryName)?current.CountryName: request.CountryName;
            request.BornDate= request.BornDate==DateTime.MinValue?current.BornDate: request.BornDate;
            request.Age = String.IsNullOrWhiteSpace(request.Age)?current.Age:request.Age;
            request.MaritalStatus = request.MaritalStatus== Common.Enums.MaritalStatus.NONE?current.MaritalStatus: request.MaritalStatus;
            request.TaxRegime = String.IsNullOrWhiteSpace(request.TaxRegime) ? current.TaxRegime : request.TaxRegime;
            request.Address = String.IsNullOrWhiteSpace(request.Address)?current.Address: request.Address;
            request.PostalCode = String.IsNullOrWhiteSpace(request.PostalCode)?current.PostalCode: request.PostalCode;
            request.Suburb= String.IsNullOrWhiteSpace(request.Suburb)?current.Suburb: request.Suburb;
            request.HomePhone = String.IsNullOrWhiteSpace(request.HomePhone)?current.HomePhone: request.HomePhone;
            request.City = request.City==null||request.City.Id==0 ? current.City : request.City;
            request.State = request.State==null||request.State.Id==0 ? current.State : request.State;
            request.MobilPhone = String.IsNullOrWhiteSpace(request.MobilPhone)?current.MobilPhone: request.MobilPhone;

            
            request.IDCardRecord = String.IsNullOrWhiteSpace(request.IDCardRecord)?current.IDCardRecord: request.IDCardRecord;
            request.IDCardExpeditionPlace = String.IsNullOrWhiteSpace(request.IDCardExpeditionPlace)? current.IDCardExpeditionPlace: request.IDCardExpeditionPlace;
            request.IDCardObservations = String.IsNullOrWhiteSpace(request.IDCardObservations) ? current.IDCardObservations : request.IDCardObservations;
            
            request.AddressProofRecord = String.IsNullOrWhiteSpace(request.AddressProofRecord) ?current.AddressProofRecord : request.AddressProofRecord;
            request.AddressProofExpeditionPlace = String.IsNullOrWhiteSpace(request.AddressProofExpeditionPlace) ? current.AddressProofExpeditionPlace : request.AddressProofExpeditionPlace;
            request.AddressProofObservations = String.IsNullOrWhiteSpace(request.AddressProofObservations) ? current.AddressProofObservations : request.AddressProofObservations;
                        
            request.BirthCertificateRecord = String.IsNullOrWhiteSpace(request.BirthCertificateRecord) ?current.BirthCertificateRecord : request.BirthCertificateRecord;
            request.BirthCertificateExpeditionPlace = String.IsNullOrWhiteSpace(request.BirthCertificateExpeditionPlace) ? current.BirthCertificateExpeditionPlace : request.BirthCertificateExpeditionPlace;
            request.BirthCertificateObservations = String.IsNullOrWhiteSpace(request.BirthCertificateObservations) ? current.BirthCertificateObservations : request.BirthCertificateObservations;
                        
            request.CURPRecord = String.IsNullOrWhiteSpace(request.CURPRecord) ?current.CURPRecord : request.CURPRecord;
            request.CURPExpeditionPlace = String.IsNullOrWhiteSpace(request.CURPExpeditionPlace) ? current.CURPExpeditionPlace : request.CURPExpeditionPlace;
            request.CURPObservations = String.IsNullOrWhiteSpace(request.CURPObservations) ? current.CURPObservations : request.CURPObservations;
                        
            request.StudyProofRecord = String.IsNullOrWhiteSpace(request.StudyProofRecord) ?current.StudyProofRecord : request.StudyProofRecord;
            request.StudyProofExpeditionPlace = String.IsNullOrWhiteSpace(request.StudyProofExpeditionPlace) ? current.StudyProofExpeditionPlace : request.StudyProofExpeditionPlace;
            request.StudyProofObservations = String.IsNullOrWhiteSpace(request.StudyProofObservations) ? current.StudyProofObservations : request.StudyProofObservations;
                        
            request.SocialSecurityProofRecord = String.IsNullOrWhiteSpace(request.SocialSecurityProofRecord) ?current.SocialSecurityProofRecord : request.SocialSecurityProofRecord;
            request.SocialSecurityProofExpeditionPlace = String.IsNullOrWhiteSpace(request.SocialSecurityProofExpeditionPlace) ? current.SocialSecurityProofExpeditionPlace : request.SocialSecurityProofExpeditionPlace;
            request.SocialSecurityProofObservations = String.IsNullOrWhiteSpace(request.SocialSecurityProofObservations) ? current.SocialSecurityProofObservations : request.SocialSecurityProofObservations;
                        
            request.MilitaryLetterRecord = String.IsNullOrWhiteSpace(request.MilitaryLetterRecord) ? current.MilitaryLetterRecord : request.MilitaryLetterRecord;
            request.MilitaryLetterExpeditionPlace = String.IsNullOrWhiteSpace(request.MilitaryLetterExpeditionPlace) ? current.MilitaryLetterExpeditionPlace : request.MilitaryLetterExpeditionPlace;
            request.MilitaryLetterObservations = String.IsNullOrWhiteSpace(request.MilitaryLetterObservations) ? current.MilitaryLetterObservations : request.MilitaryLetterObservations;
                        
            request.RFCRecord = String.IsNullOrWhiteSpace(request.RFCRecord) ?current.RFCRecord : request.RFCRecord;
            request.RFCExpeditionPlace = String.IsNullOrWhiteSpace(request.RFCExpeditionPlace) ? current.RFCExpeditionPlace : request.RFCExpeditionPlace;
            request.RFCObservations = String.IsNullOrWhiteSpace(request.RFCObservations) ? current.RFCObservations : request.RFCObservations;
                        
            request.CriminalRecordLetterRecord = String.IsNullOrWhiteSpace(request.CriminalRecordLetterRecord) ?current.CriminalRecordLetterRecord : request.CriminalRecordLetterRecord;
            request.CriminalRecordLetterExpeditionPlace = String.IsNullOrWhiteSpace(request.CriminalRecordLetterExpeditionPlace) ? current.CriminalRecordLetterExpeditionPlace : request.CriminalRecordLetterExpeditionPlace;
            request.CriminalRecordLetterObservations = String.IsNullOrWhiteSpace(request.CriminalRecordLetterObservations) ? current.CriminalRecordLetterObservations : request.CriminalRecordLetterObservations;


            request.INEFrontMedia = request.INEFrontMedia==null||request.INEFrontMedia.Id==0?current.INEFrontMedia:request.INEFrontMedia;
            request.INEBackMedia = request.INEBackMedia==null||request.INEBackMedia.Id==0?current.INEBackMedia : request.INEBackMedia;
            request.AddressProofMedia = request.AddressProofMedia==null||request.AddressProofMedia.Id==0?current.AddressProofMedia : request.AddressProofMedia;
            request.BornCertificateMedia = request.BornCertificateMedia==null||request.BornCertificateMedia.Id==0?current.BornCertificateMedia : request.BornCertificateMedia;
            request.CURPMedia = request.CURPMedia==null||request.CURPMedia.Id==0?current.CURPMedia : request.CURPMedia;
            request.StudiesProofMedia = request.StudiesProofMedia==null||request.StudiesProofMedia.Id==0?current.StudiesProofMedia : request.StudiesProofMedia;
            request.SocialSecurityProofMedia = request.SocialSecurityProofMedia==null||request.SocialSecurityProofMedia.Id==0?current.SocialSecurityProofMedia : request.SocialSecurityProofMedia;
            request.PresentedIdentificationMedia = request.PresentedIdentificationMedia==null||request.PresentedIdentificationMedia.Id==0?current.PresentedIdentificationMedia : request.PresentedIdentificationMedia;
            request.VerificationMedia = request.VerificationMedia==null||request.VerificationMedia.Id==0?current.VerificationMedia : request.VerificationMedia;
            request.MilitaryLetterMedia = request.MilitaryLetterMedia==null||request.MilitaryLetterMedia.Id==0?current.MilitaryLetterMedia : request.MilitaryLetterMedia;
            request.RFCMedia = request.RFCMedia==null||request.RFCMedia.Id==0?current.RFCMedia : request.RFCMedia;
            request.CriminalRecordMedia = request.CriminalRecordMedia==null||request.CriminalRecordMedia.Id==0?current.CriminalRecordMedia : request.CriminalRecordMedia;

            var response = await _studyGeneralInformationRepository.UpdateStudyGeneralInformation(request);
            if (response !=null && response.Sucess)
            {
                var result = await GetStudyGeneralInformation(new List<long>() { request.Id });
                response.Response = result.Response.FirstOrDefault();
            }
            return response;
        }


        //Recommendation card
        public async Task<GenericResponse<List<RecommendationCard>>> CreateRecommendationCard(List<RecommendationCard> request)
        {
            return await _studyGeneralInformationRepository.CreateRecommendationCard(request);
        }

        public async Task<GenericResponse<RecommendationCard>> DeleteRecommendationCard(long id)
        {
            return await _studyGeneralInformationRepository.DeleteRecommendationCard(id);
        }

        public async Task<GenericResponse<List<RecommendationCard>>> GetRecommendationCard(List<long> id)
        {
            return await _studyGeneralInformationRepository.GetRecommendationCard(id);
        }

        public async Task<GenericResponse<RecommendationCard>> UpdateRecommendationCard(RecommendationCard request)
        {
            var items = await GetRecommendationCard(new List<long>() { request.Id });
            var current = items.Response.FirstOrDefault();

            request.IssueCompany = string.IsNullOrWhiteSpace(request.IssueCompany) ? current.IssueCompany : request.IssueCompany;
            request.TimeWorking = string.IsNullOrWhiteSpace(request.TimeWorking) ? current.TimeWorking : request.TimeWorking;
            request.WorkingFrom = request.WorkingFrom == DateTime.MinValue ? current.WorkingFrom : request.WorkingFrom;
            request.WorkingTo = request.WorkingTo == DateTime.MinValue ? current.WorkingTo : request.WorkingTo;

            return await _studyGeneralInformationRepository.UpdateRecommendationCard(request);
        }
    }
}
