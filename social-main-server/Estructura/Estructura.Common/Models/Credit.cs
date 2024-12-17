using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class Credit
    {
        public long Id { get; set; }
        public long StudyEconomicSituationId { get; set; }
        public string Bank { get; set; }
        public string AccountNumber { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Debt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
