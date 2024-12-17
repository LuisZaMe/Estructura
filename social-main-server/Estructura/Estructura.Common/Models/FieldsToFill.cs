using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class FieldsToFill
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public bool Resume { get; set; }
        public bool GeneralInformation { get; set; }
        public bool RecommendationLetter { get; set; }
        public bool IdentificationCardPics { get; set; }
        public bool EducationalLevel { get; set; }
        public bool Extracurricular { get; set; }
        public bool ScholarVerification { get; set; }
        public bool Family { get; set; }
        public bool EconomicSituation { get; set; }
        public bool Social { get; set; }
        public bool WorkHistory { get; set; }
        public bool IMSSValidation { get; set; }
        public bool PersonalReferences { get; set; }
        public bool Pictures { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
