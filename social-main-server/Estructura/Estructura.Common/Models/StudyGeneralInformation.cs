using Estructura.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class StudyGeneralInformation
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string TimeOnComany { get; set; }
        public string EmployeeNumber { get; set; }
        public City BornCity { get; set; }
        public State BornState { get; set; }
        public string CountryName { get; set; }
        public DateTime BornDate { get; set; }
        public string Age { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string TaxRegime { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Suburb { get; set; }
        public string HomePhone { get; set; }
        public City City { get; set; }
        public State State { get; set; }
        public string MobilPhone { get; set; }


        public bool IDCardOriginal { get; set; }
        public bool IDCardCopy { get; set; }
        public string IDCardRecord { get; set; }
        public string IDCardExpeditionPlace { get; set; }
        public string IDCardObservations { get; set; }


        public bool AddressProofOriginal { get; set; }
        public bool AddressProofCopy { get; set; }
        public string AddressProofRecord { get; set; }
        public string AddressProofExpeditionPlace { get; set; }
        public string AddressProofObservations { get; set; }


        public bool BirthCertificateOriginal { get; set; }
        public bool BirthCertificateCopy { get; set; }
        public string BirthCertificateRecord { get; set; }
        public string BirthCertificateExpeditionPlace { get; set; }
        public string BirthCertificateObservations { get; set; }


        public bool CURPOriginal { get; set; }
        public bool CURPCopy { get; set; }
        public string CURPRecord { get; set; }
        public string CURPExpeditionPlace { get; set; }
        public string CURPObservations { get; set; }


        public bool StudyProofOriginal { get; set; }
        public bool StudyProofCopy { get; set; }
        public string StudyProofRecord { get; set; }
        public string StudyProofExpeditionPlace { get; set; }
        public string StudyProofObservations { get; set; }


        public bool SocialSecurityProofOriginal { get; set; }
        public bool SocialSecurityProofCopy { get; set; }
        public string SocialSecurityProofRecord { get; set; }
        public string SocialSecurityProofExpeditionPlace { get; set; }
        public string SocialSecurityProofObservations { get; set; }


        public bool   MilitaryLetterOriginal { get; set; }
        public bool   MilitaryLetterCopy { get; set; }
        public string MilitaryLetterRecord { get; set; }
        public string MilitaryLetterExpeditionPlace { get; set; }
        public string MilitaryLetterObservations { get; set; }


        public bool   RFCOriginal { get; set; }
        public bool   RFCCopy { get; set; }
        public string RFCRecord { get; set; }
        public string RFCExpeditionPlace { get; set; }
        public string RFCObservations { get; set; }


        public bool   CriminalRecordLetterOriginal { get; set; }
        public bool   CriminalRecordLetterCopy { get; set; }
        public string CriminalRecordLetterRecord { get; set; }
        public string CriminalRecordLetterExpeditionPlace { get; set; }
        public string CriminalRecordLetterObservations { get; set; }


        public Media INEFrontMedia { get; set; }
        public Media INEBackMedia { get; set; }
        public Media AddressProofMedia { get; set; }
        public Media BornCertificateMedia { get; set; }
        public Media CURPMedia { get; set; }
        public Media StudiesProofMedia { get; set; }
        public Media SocialSecurityProofMedia { get; set; }
        public Media MilitaryLetterMedia { get; set; }
        public Media RFCMedia { get; set; }
        public Media CriminalRecordMedia { get; set; }


        public Media PresentedIdentificationMedia { get; set; }
        public Media VerificationMedia { get; set; }

        public List<RecommendationCard> RecommendationCards { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
