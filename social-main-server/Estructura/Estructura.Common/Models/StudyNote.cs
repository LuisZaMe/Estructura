using System;

namespace Estructura.Common.Models
{
    public class StudyNote
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long StudyId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
