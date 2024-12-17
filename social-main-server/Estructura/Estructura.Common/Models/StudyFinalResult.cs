using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class StudyFinalResult
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public string PositionSummary { get; set; }
        public string AttitudeSummary { get; set; }
        public string WorkHistorySummary { get; set; }
        public string ArbitrationAndConciliationSummary { get; set; }
        public string FinalResultsBy { get; set; }
        public string FinalResultsPositionBy { get; set; }
        public string VisitDate { get; set; }
        public string ApplicationDate { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
