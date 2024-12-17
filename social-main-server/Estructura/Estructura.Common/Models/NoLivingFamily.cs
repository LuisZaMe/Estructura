using Estructura.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class NoLivingFamily
    {
        public long Id { get; set; }
        public long StudyFamilyId { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string Age { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string Schoolarity { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Occupation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
