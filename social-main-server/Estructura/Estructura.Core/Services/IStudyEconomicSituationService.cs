using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Services
{
    public interface IStudyEconomicSituationService
    {        
        //Study Economic Situation
        Task<GenericResponse<StudyEconomicSituation>> CreateStudyEconomicSituation(StudyEconomicSituation request);
        Task<GenericResponse<List<StudyEconomicSituation>>> GetStudyEconomicSituation(List<long> id, bool byStudy = false);
        Task<GenericResponse<StudyEconomicSituation>> UpdateStudyEconomicSituation(StudyEconomicSituation request);
        Task<GenericResponse<StudyEconomicSituation>> DeleteStudyEconomicSituation(long id);



        // Incoming
        Task<GenericResponse<List<Incoming>>> CreateIncoming(List<Incoming> request);
        Task<GenericResponse<List<Incoming>>> GetIncoming(List<long> id);
        Task<GenericResponse<Incoming>> UpdateIncoming(Incoming request);
        Task<GenericResponse<Incoming>> DeleteIncoming(long id);



        // Additional Incoming
        Task<GenericResponse<List<AdditionalIncoming>>> CreateAdditionalIncoming(List<AdditionalIncoming> request);
        Task<GenericResponse<List<AdditionalIncoming>>> GetAdditionalIncoming(List<long> id);
        Task<GenericResponse<AdditionalIncoming>> UpdateAdditionalIncoming(AdditionalIncoming request);
        Task<GenericResponse<AdditionalIncoming>> DeleteAdditionalIncoming(long id);



        // Credit
        Task<GenericResponse<List<Credit>>> CreateCredit(List<Credit> request);
        Task<GenericResponse<List<Credit>>> GetCredit(List<long> id);
        Task<GenericResponse<Credit>> UpdateCredit(Credit request);
        Task<GenericResponse<Credit>> DeleteCredit(long id);



        // Estate
        Task<GenericResponse<List<Estate>>> CreateEstate(List<Estate> request);
        Task<GenericResponse<List<Estate>>> GetEstate(List<long> id);
        Task<GenericResponse<Estate>> UpdateEstate(Estate request);
        Task<GenericResponse<Estate>> DeleteEstate(long id);



        // Vehicle
        Task<GenericResponse<List<Vehicle>>> CreateVehicle(List<Vehicle> request);
        Task<GenericResponse<List<Vehicle>>> GetVehicle(List<long> id);
        Task<GenericResponse<Vehicle>> UpdateVehicle(Vehicle request);
        Task<GenericResponse<Vehicle>> DeleteVehicle(long id);
    }
}
