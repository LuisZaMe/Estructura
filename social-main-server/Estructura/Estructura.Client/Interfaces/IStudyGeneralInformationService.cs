using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IStudyGeneralInformationService
    {
        // StudyGeneralInformation
        Task<GenericResponse<StudyGeneralInformation>> CreateStudyGeneralInformation(StudyGeneralInformation request);
        Task<GenericResponse<List<StudyGeneralInformation>>> GetStudyGeneralInformation(List<long> id);
        Task<GenericResponse<StudyGeneralInformation>> UpdateStudyGeneralInformation(StudyGeneralInformation request);
        Task<GenericResponse<StudyGeneralInformation>> DeleteStudyGeneralInformation(long id);


        // Recommendation Card
        Task<GenericResponse<List<RecommendationCard>>> CreateRecommendationCard(List<RecommendationCard> request);
        Task<GenericResponse<List<RecommendationCard>>> GetRecommendationCard(List<long> id);
        Task<GenericResponse<RecommendationCard>> UpdateRecommendationCard(RecommendationCard request);
        Task<GenericResponse<RecommendationCard>> DeleteRecommendationCard(long id);
    }
}
