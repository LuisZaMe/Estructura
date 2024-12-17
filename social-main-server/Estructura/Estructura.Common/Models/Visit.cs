using Estructura.Common.Enums;
using System;

namespace Estructura.Common.Models
{
    public class Visit
    {
        public long Id { get; set; }
        public long ScheduledVisitsId { get; set; }
        public Study Study { get; set; }
        public string VisitDate { get; set; }
        public bool ConfirmAssistance { get; set; }
        public VisitStatus VisitStatus { get; set; }
        public City City { get; set; }
        public State State { get; set; }
        public string Address { get; set; }
        public string Observations { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public long? UnderAdminUserId { get; set; }
        public string NotationColor { get; set; }
        public Media Evidence { get; set; }
    }
}
