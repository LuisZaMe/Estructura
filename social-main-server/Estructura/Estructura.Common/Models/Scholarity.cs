using Estructura.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class Scholarity
    {
        public long Id { get; set; }
        public long StudySchoolarityId { get; set; }
        public SchoolarLevel SchoolarLevel { get; set; }
        public string Career { get; set; }
        public string Period { get; set; }
        public string TimeOnCareer { get; set; }
        public string Institution { get; set; }
        public long DoccumentId { get; set; }
        public string Place { get; set; }
        public Doccument Doccument { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
