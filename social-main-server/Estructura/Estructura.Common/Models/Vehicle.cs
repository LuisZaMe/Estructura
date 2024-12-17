using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class Vehicle
    {
        public long Id { get; set; }
        public long StudyEconomicSituationId { get; set; }
        public string Plates { get; set; }
        public string SerialNumber { get; set; }
        public string BrandAndModel { get; set; }
        public string Owner { get; set; }
        public decimal PurchaseValue { get; set; }
        public decimal CurrentValue { get; set; }
        public DateTime CreatedAt   { get; set; }
        public DateTime UpdatedAt  { get; set; }
    }
}
