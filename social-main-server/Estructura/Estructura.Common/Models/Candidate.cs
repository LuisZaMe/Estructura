using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class Candidate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CURP { get; set; }
        public string NSS { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public Media Media { get; set; }
        public Identity Client { get; set; }
        public DateTime CreatedAt { get; set; }
        public City City { get; set; }
        public State State { get; set; }
        public Enums.CandidateStatus CandidateStatus { get;set;}
        public long? UnderAdminUserId { get; set; }
    }
}
