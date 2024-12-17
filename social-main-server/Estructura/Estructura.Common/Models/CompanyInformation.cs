using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class CompanyInformation
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string DireccionFiscal { get; set; }
        public string RegimenFiscal { get; set; }
        public PaymentMethod Payment { get; set; }
        public long TotalStudies { get; set; }
        public long CompletedStudies { get; set; }
    }
}
