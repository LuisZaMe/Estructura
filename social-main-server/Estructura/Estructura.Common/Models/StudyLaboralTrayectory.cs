using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class StudyLaboralTrayectory
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public string TrayectoryName { get; set; }
        public string CompanyName { get; set; }
        public string CandidateBusinessName { get; set; }
        public string CompanyBusinessName { get; set; }
        public string CandidateRole { get; set; }
        public string CompanyRole { get; set; }
        public string CandidatePhone { get; set; }
        public string CompanyPhone { get; set; }
        public string CandidateAddress { get; set; }
        public string CompanyAddress { get; set; }
        public DateTime  CandidateStartDate { get; set; }
        public DateTime  CompanyStartDate { get; set; }
        public DateTime  CandidateEndDate { get; set; }
        public DateTime  CompanyEndDate { get; set; }
        public string CandidateInitialRole { get; set; }
        public string CompanyInitialRole { get; set; }
        public string CandidateFinalRole { get; set; }
        public string CompanyFinalRole { get; set; }
        public decimal CandidateStartSalary { get; set; }
        public decimal CompanyStartSalary { get; set; }
        public decimal CandidateEndSalary { get; set; }
        public decimal CompanyEndSalary { get; set; }
        public string CandidateBenefits { get; set; }
        public string CompanyBenefits { get; set; }
        public string CandidateResignationReason { get; set; }
        public string CompanyResignationReason { get; set; }
        public string CandidateDirectBoss { get; set; }
        public string CompanyDirectBoss { get; set; }
        public string CandidateLaborUnion { get; set; }
        public string CompanyLaborUnion { get; set; }
        public string Recommended { get; set; }
        public string RecommendedReasonWhy { get; set; }
        public string Rehire { get; set; }
        public string RehireReason { get; set; }
        public string Observations { get; set; }
        public string Notes { get; set; }
        public Media Media1 { get; set; }
        public Media Media2 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
