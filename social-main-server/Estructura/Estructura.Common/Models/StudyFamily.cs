using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class StudyFamily
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public string Notes { get; set; }
        public string FamiliarArea { get; set; }
        public List<LivingFamily> LivingFamilyList { get; set; }
        public List<NoLivingFamily> NoLivingFamilyList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
