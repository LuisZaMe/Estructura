using Estructura.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class ExtracurricularActivities
    {
        public long Id { get; set; }
        public long StudySchoolarityId { get; set; }
        public string Name { get; set; }
        public string Instituution { get; set; }
        public KnowldegeLevel KnowledgeLevel { get; set; }
        public string Period { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
