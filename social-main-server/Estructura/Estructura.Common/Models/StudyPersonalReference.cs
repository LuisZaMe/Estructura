using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class StudyPersonalReference
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public string ReferenceTitle { get; set; }
        public string Name { get; set; }
        public string CurrentJob { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string YearsKnowingEachOther { get; set; }
        public string KnowAddress { get; set; }
        public string YearsOnCurrentResidence { get; set; }
        public string KnowledgeAboutPreviousJobs { get; set; }
        public string OpinionAboutTheCandidate { get; set; }
        public string PoliticalActivity { get; set; }
        public string WouldYouRecommendIt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
