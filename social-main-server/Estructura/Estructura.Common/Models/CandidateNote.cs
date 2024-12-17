using System;

namespace Estructura.Common.Models
{
    public class CandidateNote
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long CandidateId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
