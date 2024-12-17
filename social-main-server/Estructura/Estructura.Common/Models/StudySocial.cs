using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class StudySocial
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public string SocialArea { get; set; }
        public string Hobbies { get; set; }
        public string HealthInformation { get; set; }
        public List<SocialGoals> SocialGoalsList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
