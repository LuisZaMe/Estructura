using System;
using System.Collections.Generic;

namespace Estructura.Common.Models
{
    public class Study
    {
        public long Id { get; set; }
        public Candidate Candidate { get; set; }
        public Enums.ServiceTypes ServiceType { get; set; }
        public Identity Interviewer { get; set; }
        public Identity Analyst { get; set; }
        public Enums.StudyStatus StudyStatus { get; set; }
        public Enums.StudyProgressStatus StudyProgressStatus { get; set; }
        public WorkStudy WorkStudy { get; set; }
        public SocioeconomicStudy SocioeconomicStudy { get; set; }
        public FieldsToFill FieldsToFill { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public long? UnderAdminUserId { get; set; }



        //Studies specifications
        public StudyEconomicSituation StudyEconomicSituation { get; set; }
        public StudyFamily StudyFamily { get; set; }
        public StudyFinalResult StudyFinalResult { get; set; }
        public StudyGeneralInformation StudyGeneralInformation { get; set; }
        public StudySchoolarity StudySchoolarity { get; set; }
        public StudySocial StudySocial { get; set; }
        public List<StudyLaboralTrayectory> StudyLaboralTrayectoryList { get; set; }
        public StudyIMSSValidation StudyIMSSValidation { get; set; }
        public List<StudyPersonalReference> StudyPersonalReferenceList { get; set; }
        public StudyPictures StudyPictures { get; set; }
    }
}
