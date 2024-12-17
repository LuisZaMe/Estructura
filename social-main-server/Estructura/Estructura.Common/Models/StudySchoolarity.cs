using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class StudySchoolarity
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public string ScholarVerificationSummary { get; set; }
        public Media ScholarVerificationMedia { get; set;}
        public List<Scholarity> ScholarityList { get; set; }
        public List<ExtracurricularActivities> ExtracurricularActivitiesList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
