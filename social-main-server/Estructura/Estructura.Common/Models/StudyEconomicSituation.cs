using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class StudyEconomicSituation
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public decimal Electricity { get; set; }
        public decimal Rent { get; set; }
        public decimal Gas { get; set; }
        public decimal Infonavit { get; set; }
        public decimal Water { get; set; }
        public decimal Credits { get; set; }
        public decimal PropertyTax { get; set; }
        public decimal Maintenance { get; set; }
        public decimal Internet { get; set; }
        public decimal Cable { get; set; }
        public decimal Food { get; set; }
        public decimal Cellphone { get; set; }
        public decimal Gasoline { get; set; }
        public decimal Entertainment { get; set; }
        public decimal Clothing { get; set; }
        public decimal Miscellaneous { get; set; }
        public decimal Schoolar { get; set; }
        public string EconomicSituationSummary { get; set; }
        public List<Incoming> IncomingList { get; set; }
        public List<AdditionalIncoming> AdditionalIncomingList { get; set; }
        public List<Credit> CreditList { get; set; }
        public List<Estate> EstateList { get; set; }
        public List<Vehicle> VehicleList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
