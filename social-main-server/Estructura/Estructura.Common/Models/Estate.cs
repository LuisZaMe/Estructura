using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class Estate
    {
        public long Id { get; set; }
        public long StudyEconomicSituationId { get; set; }
        public string Concept { get;set; }
        public string AcquisitionMethod { get; set; }
        public DateTime AcquisitionDate    { get; set; }
        public string Owner { get; set; }
        public decimal PurchaseValue { get; set; }
        public decimal CurrentValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
