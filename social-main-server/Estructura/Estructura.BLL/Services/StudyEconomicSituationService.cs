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
    public class StudyEconomicSituationService: IStudyEconomicSituationService
    {
        private readonly IStudyEconomicSituationRepository _studyEconomicSituationRepository;
        public StudyEconomicSituationService(IStudyEconomicSituationRepository _studyEconomicSituationRepository)
        {
            this._studyEconomicSituationRepository=_studyEconomicSituationRepository;
        }



        //Study Economic Situation
        public async Task<GenericResponse<StudyEconomicSituation>> CreateStudyEconomicSituation(StudyEconomicSituation request)
        {
            var error = new GenericResponse<StudyEconomicSituation>()
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };

            var response = await _studyEconomicSituationRepository.CreateStudyEconomicSituation(request);
            if (!response.Sucess)
                return response;

            if (request.IncomingList!=null&&request.IncomingList.Count>0)
            {
                request.IncomingList.ForEach(e => e.StudyEconomicSituationId = response.Response.Id);
                var lst = await CreateIncoming(request.IncomingList);
                if (!lst.Sucess)
                {
                    await DeleteStudyEconomicSituation(response.Response.Id);
                    error.ErrorMessage = "error creating Incoming items";
                    return error;
                }
                response.Response.IncomingList = lst.Response;
            }

            if (request.AdditionalIncomingList!=null&&request.AdditionalIncomingList.Count>0)
            {
                request.AdditionalIncomingList.ForEach(e => e.StudyEconomicSituationId = response.Response.Id);
                var lst = await CreateAdditionalIncoming(request.AdditionalIncomingList);
                if (!lst.Sucess)
                {
                    await DeleteStudyEconomicSituation(response.Response.Id);
                    error.ErrorMessage = "error creating Additional Incoming items";
                    return error;
                }
                response.Response.AdditionalIncomingList = lst.Response;
            }

            if (request.CreditList!=null&&request.CreditList.Count>0)
            {
                request.CreditList.ForEach(e => e.StudyEconomicSituationId = response.Response.Id);
                var lst = await CreateCredit(request.CreditList);
                if (!lst.Sucess)
                {
                    await DeleteStudyEconomicSituation(response.Response.Id);
                    error.ErrorMessage = "error creating Credit items";
                    return error;
                }
                response.Response.CreditList = lst.Response;
            }

            if (request.EstateList!=null&&request.EstateList.Count>0)
            {
                request.EstateList.ForEach(e => e.StudyEconomicSituationId = response.Response.Id);
                var lst = await CreateEstate(request.EstateList);
                if (!lst.Sucess)
                {
                    await DeleteStudyEconomicSituation(response.Response.Id);
                    error.ErrorMessage = "error creating Estate items";
                    return error;
                }
                response.Response.EstateList = lst.Response;
            }

            if (request.VehicleList!=null&&request.VehicleList.Count>0)
            {
                request.VehicleList.ForEach(e => e.StudyEconomicSituationId = response.Response.Id);
                var lst = await CreateVehicle(request.VehicleList);
                if (!lst.Sucess)
                {
                    await DeleteStudyEconomicSituation(response.Response.Id);
                    error.ErrorMessage = "error creating Vehicle items";
                    return error;
                }
                response.Response.VehicleList = lst.Response;
            }

            var responseList = await GetStudyEconomicSituation(new List<long>() { response.Response.Id });
            if (responseList.Sucess && responseList.Response.Count>0)
                response.Response = responseList.Response.FirstOrDefault();

            return response;
        }

        public async Task<GenericResponse<StudyEconomicSituation>> DeleteStudyEconomicSituation(long id)
        {
            return await _studyEconomicSituationRepository.DeleteStudyEconomicSituation(id);
        }

        public async Task<GenericResponse<List<StudyEconomicSituation>>> GetStudyEconomicSituation(List<long> id, bool byStudy = false)
        {
            return await _studyEconomicSituationRepository.GetStudyEconomicSituation(id, byStudy);
        }

        public async Task<GenericResponse<StudyEconomicSituation>> UpdateStudyEconomicSituation(StudyEconomicSituation request)
        {
            var responseItems = await GetStudyEconomicSituation(new List<long>() { request.Id });

            if (!responseItems.Sucess || responseItems.Response.Count==0)
                return new GenericResponse<StudyEconomicSituation>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

            var current = responseItems.Response.FirstOrDefault();

            request.EconomicSituationSummary = String.IsNullOrWhiteSpace(request.EconomicSituationSummary)? current.EconomicSituationSummary:request.EconomicSituationSummary;


            var response = await _studyEconomicSituationRepository.UpdateStudyEconomicSituation(request);

            var responseList = await GetStudyEconomicSituation(new List<long>() { response.Response.Id });
            if (responseList.Sucess && responseList.Response.Count>0)
                response.Response = responseList.Response.FirstOrDefault();

            return response;
        }



        // Incoming
        public async Task<GenericResponse<List<Incoming>>> CreateIncoming(List<Incoming> request)
        {
            if (request.Any(e => e.StudyEconomicSituationId == 0))
                return new GenericResponse<List<Incoming>>()
                {
                    ErrorMessage = "all items need StudyEconomicSituationId",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            return await _studyEconomicSituationRepository.CreateIncoming(request);
        }

        public async Task<GenericResponse<Incoming>> DeleteIncoming(long id)
        {
            return await _studyEconomicSituationRepository.DeleteIncoming(id);
        }

        public async Task<GenericResponse<List<Incoming>>> GetIncoming(List<long> id)
        {
            return await _studyEconomicSituationRepository.GetIncoming(id);
        }

        public async Task<GenericResponse<Incoming>> UpdateIncoming(Incoming request)
        {
            var responseItems = await GetIncoming(new List<long>() { request.Id });

            if (!responseItems.Sucess || responseItems.Response.Count==0)
                return new GenericResponse<Incoming>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

            var current = responseItems.Response.FirstOrDefault();

            request.Name = String.IsNullOrWhiteSpace(request.Name) ? current.Name : request.Name;
            request.Relationship = String.IsNullOrWhiteSpace(request.Relationship) ? current.Relationship : request.Relationship;


            var response = await _studyEconomicSituationRepository.UpdateIncoming(request);

            var responseList = await GetIncoming(new List<long>() { response.Response.Id });
            if (responseList.Sucess && responseList.Response.Count>0)
                response.Response = responseList.Response.FirstOrDefault();

            return response;
        }



        // Additional Incoming
        public async Task<GenericResponse<List<AdditionalIncoming>>> CreateAdditionalIncoming(List<AdditionalIncoming> request)
        {
            if (request.Any(e => e.StudyEconomicSituationId == 0))
                return new GenericResponse<List<AdditionalIncoming>>()
                {
                    ErrorMessage = "all items need StudyEconomicSituationId",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            return await _studyEconomicSituationRepository.CreateAdditionalIncoming(request);
        }

        public async Task<GenericResponse<AdditionalIncoming>> DeleteAdditionalIncoming(long id)
        {
            return await _studyEconomicSituationRepository.DeleteAdditionalIncoming(id);
        }

        public async Task<GenericResponse<List<AdditionalIncoming>>> GetAdditionalIncoming(List<long> id)
        {
            return await _studyEconomicSituationRepository.GetAdditionalIncoming(id);
        }

        public async Task<GenericResponse<AdditionalIncoming>> UpdateAdditionalIncoming(AdditionalIncoming request)
        {
            var responseItems = await GetAdditionalIncoming(new List<long>() { request.Id });

            if (!responseItems.Sucess || responseItems.Response.Count==0)
                return new GenericResponse<AdditionalIncoming>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

            var current = responseItems.Response.FirstOrDefault();

            request.Activity = String.IsNullOrWhiteSpace(request.Activity) ? current.Activity : request.Activity;
            request.TimeFrame = String.IsNullOrWhiteSpace(request.TimeFrame) ? current.TimeFrame : request.TimeFrame;


            var response = await _studyEconomicSituationRepository.UpdateAdditionalIncoming(request);

            var responseList = await GetAdditionalIncoming(new List<long>() { response.Response.Id });
            if (responseList.Sucess && responseList.Response.Count>0)
                response.Response = responseList.Response.FirstOrDefault();

            return response;
        }



        // Credit
        public async Task<GenericResponse<List<Credit>>> CreateCredit(List<Credit> request)
        {
            if (request.Any(e => e.StudyEconomicSituationId == 0))
                return new GenericResponse<List<Credit>>()
                {
                    ErrorMessage = "all items need Credit",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            return await _studyEconomicSituationRepository.CreateCredit(request);
        }

        public async Task<GenericResponse<Credit>> DeleteCredit(long id)
        {
            return await _studyEconomicSituationRepository.DeleteCredit(id);
        }

        public async Task<GenericResponse<List<Credit>>> GetCredit(List<long> id)
        {
            return await _studyEconomicSituationRepository.GetCredit(id);
        }

        public async Task<GenericResponse<Credit>> UpdateCredit(Credit request)
        {
            var responseItems = await GetCredit(new List<long>() { request.Id });

            if (!responseItems.Sucess || responseItems.Response.Count==0)
                return new GenericResponse<Credit>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

            var current = responseItems.Response.FirstOrDefault();

            request.Bank = String.IsNullOrWhiteSpace(request.Bank) ? current.Bank : request.Bank;
            request.AccountNumber = String.IsNullOrWhiteSpace(request.AccountNumber) ? current.AccountNumber : request.AccountNumber;


            var response = await _studyEconomicSituationRepository.UpdateCredit(request);

            var responseList = await GetCredit(new List<long>() { response.Response.Id });
            if (responseList.Sucess && responseList.Response.Count>0)
                response.Response = responseList.Response.FirstOrDefault();

            return response;
        }



        // Estate
        public async Task<GenericResponse<List<Estate>>> CreateEstate(List<Estate> request)
        {
            if (request.Any(e => e.StudyEconomicSituationId == 0))
                return new GenericResponse<List<Estate>>()
                {
                    ErrorMessage = "all items need Credit",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            return await _studyEconomicSituationRepository.CreateEstate(request);
        }

        public async Task<GenericResponse<Estate>> DeleteEstate(long id)
        {
            return await _studyEconomicSituationRepository.DeleteEstate(id);
        }

        public async Task<GenericResponse<List<Estate>>> GetEstate(List<long> id)
        {
            return await _studyEconomicSituationRepository.GetEstate(id);
        }

        public async Task<GenericResponse<Estate>> UpdateEstate(Estate request)
        {
            var responseItems = await GetEstate(new List<long>() { request.Id });

            if (!responseItems.Sucess || responseItems.Response.Count==0)
                return new GenericResponse<Estate>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

            var current = responseItems.Response.FirstOrDefault();

            request.Concept = String.IsNullOrWhiteSpace(request.Concept) ? current.Concept : request.Concept;
            request.AcquisitionMethod = String.IsNullOrWhiteSpace(request.AcquisitionMethod) ? current.AcquisitionMethod : request.AcquisitionMethod;
            request.AcquisitionDate = request.AcquisitionDate == DateTime.MinValue ? current.AcquisitionDate : request.AcquisitionDate;
            request.Owner = String.IsNullOrWhiteSpace(request.Owner) ? current.Owner : request.Owner;


            var response = await _studyEconomicSituationRepository.UpdateEstate(request);

            var responseList = await GetEstate(new List<long>() { response.Response.Id });
            if (responseList.Sucess && responseList.Response.Count>0)
                response.Response = responseList.Response.FirstOrDefault();

            return response;
        }



        // Vehicle
        public async Task<GenericResponse<List<Vehicle>>> CreateVehicle(List<Vehicle> request)
        {
            if (request.Any(e => e.StudyEconomicSituationId == 0))
                return new GenericResponse<List<Vehicle>>()
                {
                    ErrorMessage = "all items need Credit",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            return await _studyEconomicSituationRepository.CreateVehicle(request);
        }

        public async Task<GenericResponse<Vehicle>> DeleteVehicle(long id)
        {
            return await _studyEconomicSituationRepository.DeleteVehicle(id);
        }

        public async Task<GenericResponse<List<Vehicle>>> GetVehicle(List<long> id)
        {
            return await _studyEconomicSituationRepository.GetVehicle(id);
        }

        public async Task<GenericResponse<Vehicle>> UpdateVehicle(Vehicle request)
        {
            var responseItems = await GetVehicle(new List<long>() { request.Id });

            if (!responseItems.Sucess || responseItems.Response.Count==0)
                return new GenericResponse<Vehicle>()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

            var current = responseItems.Response.FirstOrDefault();

            request.Plates = String.IsNullOrWhiteSpace(request.Plates) ? current.Plates : request.Plates;
            request.SerialNumber = String.IsNullOrWhiteSpace(request.SerialNumber) ? current.SerialNumber : request.SerialNumber;
            request.BrandAndModel = String.IsNullOrWhiteSpace(request.BrandAndModel) ? current.BrandAndModel : request.BrandAndModel;
            request.Owner = String.IsNullOrWhiteSpace(request.Owner) ? current.Owner : request.Owner;


            var response = await _studyEconomicSituationRepository.UpdateVehicle(request);

            var responseList = await GetVehicle(new List<long>() { response.Response.Id });
            if (responseList.Sucess && responseList.Response.Count>0)
                response.Response = responseList.Response.FirstOrDefault();

            return response;
        }
    }
}
