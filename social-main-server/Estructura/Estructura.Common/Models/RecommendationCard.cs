using System;

namespace Estructura.Common.Models
{
    public class RecommendationCard
    {
        public long Id { get; set; }
        public long StudyGeneralInformationId { get; set; }
        public string IssueCompany { get; set; }
        public DateTime WorkingFrom { get; set; }
        public DateTime WorkingTo { get; set; }
        public string TimeWorking { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
