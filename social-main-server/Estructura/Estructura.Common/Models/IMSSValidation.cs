using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class IMSSValidation
    {
        public long Id { get; set; }
        public long StudyIMSSValidationId { get; set; }
        public string CompanyBusinessName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Result { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
