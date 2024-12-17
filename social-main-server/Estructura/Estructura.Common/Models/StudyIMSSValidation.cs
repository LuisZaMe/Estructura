using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class StudyIMSSValidation
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public string CreditNumber { get; set; }
        public string CreditStatus { get; set; }
        public DateTime GrantDate { get; set; }
        public string ConciliationClaimsSummary { get; set; }
        public List<IMSSValidation> IMSSValidationList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
