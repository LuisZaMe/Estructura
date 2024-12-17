using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class AdditionalIncoming
    {
        public long Id { get; set; }
        public long StudyEconomicSituationId { get; set; }
        public string Activity { get; set; }
        public string TimeFrame { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
