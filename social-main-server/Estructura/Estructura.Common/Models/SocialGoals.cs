using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class SocialGoals
    {
        public long Id { get; set; }    
        public long StudySocialId { get; set; }
        public string CoreValue { get; set; }
        public string LifeGoal { get; set; }
        public string NextGoal { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
