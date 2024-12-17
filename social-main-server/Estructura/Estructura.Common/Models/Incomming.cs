using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class Incoming
    {
        public long Id { get; set; }
        public long StudyEconomicSituationId { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
