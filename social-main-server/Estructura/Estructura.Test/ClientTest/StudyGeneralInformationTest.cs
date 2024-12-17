using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class StudyGeneralInformationTest:TestBase
    {
        //Study general information
        [TestMethod]    
        public async Task CreateStudyGeneralInformationTest()
        {
            var request = new StudyGeneralInformation()
            {
                StudyId = 21,
                Address = "Mi casa 1234",
                AddressProofCopy = true,
                AddressProofExpeditionPlace = "la cfe4",
                AddressProofMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                AddressProofObservations = "Parece real4",
                AddressProofOriginal = true,
                AddressProofRecord = "la casa 1234",
                Age = "204",
                BirthCertificateCopy = true,
                BirthCertificateExpeditionPlace = "en las aguilas4",
                BirthCertificateObservations = "todo en orden con el acta de nacimiento4",
                BirthCertificateOriginal = true,
                BirthCertificateRecord = "1231231234",
                //BornCertificateMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                //},
                BornCity = new City() { Id = 2425 },
                BornState = new State() { Id = 14 },
                CURPRecord="el curp4",
                CURPOriginal=true,
                BornDate = DateTime.UtcNow.AddYears(-20),
                City = new City() { Id = 2425 },
                State = new State() { Id = 14 },
                CountryName = "Mexico4",
                CURPCopy = true,
                CURPExpeditionPlace = "las aguilas 24",
                //CURPMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                //},
                CURPObservations = "todo bien con el curp4",
                Email = "elemail@elemail.com4",
                EmployeeNumber = "12312314",
                HomePhone="33121234344",
                IDCardCopy = true,
                IDCardExpeditionPlace = "en las aguilas de nuevo4",
                IDCardObservations = "Todo en orden con el idcard4",
                IDCardOriginal = true,
                IDCardRecord = "123456884",
                INEBackMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                INEFrontMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                MaritalStatus= Common.Enums.MaritalStatus.IN_JUDICIAL_PROCESS,
                Name = "Juanito 224",
                PostalCode = "455554",
                MobilPhone = "3312321324",
                //PresentedIdentificationMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                //},
                SocialSecurityProofCopy = true,
                SocialSecurityProofExpeditionPlace = "en el seguro4",
                SocialSecurityProofMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                SocialSecurityProofObservations = "todo en orden con el numero de seguridad social4",
                SocialSecurityProofOriginal = true,
                SocialSecurityProofRecord="todo bien con el nss4",
                //StudiesProofMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                //},
                StudyProofCopy = true,
                StudyProofExpeditionPlace = "en la escuela4",
                StudyProofObservations = "certificado de estuudios correcto4",
                StudyProofOriginal= true,
                StudyProofRecord = "09876543214",
                Suburb = "jardines del sol4",
                TaxRegime = "empleado4",
                TimeOnComany = "100 dias 4",
                VerificationMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                RecommendationCards = new List<RecommendationCard>()
                {
                    new RecommendationCard()
                    {
                        IssueCompany = "OXXO 5",
                        TimeWorking = "10 años 5",
                        WorkingFrom = DateTime.UtcNow.AddYears(-10),
                        WorkingTo = DateTime.UtcNow
                    },
                    new RecommendationCard()
                    {
                        IssueCompany = "7eleven 5",
                        TimeWorking = "10 años 45",
                        WorkingFrom = DateTime.UtcNow.AddYears(-10),
                        WorkingTo = DateTime.UtcNow
                    },
                    new RecommendationCard()
                    {
                        IssueCompany = "tienda de la ezquina 5",
                        TimeWorking = "10 años 5",
                        WorkingFrom = DateTime.UtcNow.AddYears(-10),
                        WorkingTo = DateTime.UtcNow
                    },
                },
                CriminalRecordLetterCopy = true,
                CriminalRecordLetterExpeditionPlace = "En la comisaria",
                //CriminalRecordMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                //},
                //RFCMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                //},
                MilitaryLetterMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                CriminalRecordLetterObservations = "No hay records criminales",
                CriminalRecordLetterOriginal = true,
                CriminalRecordLetterRecord = "criminal record",
                MilitaryLetterCopy = true,
                MilitaryLetterExpeditionPlace = "academia militar",
                MilitaryLetterObservations = "Todo bien",
                MilitaryLetterOriginal = true,
                MilitaryLetterRecord = "121513123",
                RFCCopy = true,
                RFCExpeditionPlace = "SAt",
                RFCObservations = "Todo valido rfc",
                RFCOriginal = true,
                RFCRecord = "12623ygv13r"
            };

            var response = await ApiClient.StudyGeneralInformationService.CreateStudyGeneralInformation(request);
            Assert.IsTrue(response.Sucess);
        }
        
        [TestMethod]    
        public async Task GetStudyGeneralInformationTest()
        {
            var response = await ApiClient.StudyGeneralInformationService.GetStudyGeneralInformation(new List<long>() { 7,8,9 });
            Assert.IsTrue(response.Sucess);
        }
        
        [TestMethod]    
        public async Task DeleteStudyGeneralInformationTest()
        {
            var response = await ApiClient.StudyGeneralInformationService.DeleteStudyGeneralInformation(1);
            Assert.IsTrue(response.Sucess);
        }
        
        [TestMethod]    
        public async Task UpdateStudyGeneralInformationTest()
        {
            var request = new StudyGeneralInformation()
            {
                Id = 9,
                Address = "Mi casa 1234",
                AddressProofCopy = true,
                AddressProofExpeditionPlace = "la cfe4",
                AddressProofMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                AddressProofObservations = "Parece real4",
                AddressProofOriginal = true,
                AddressProofRecord = "la casa 1234",
                Age = "204",
                BirthCertificateCopy = true,
                BirthCertificateExpeditionPlace = "en las aguilas4",
                BirthCertificateObservations = "todo en orden con el acta de nacimiento4",
                BirthCertificateOriginal = true,
                BirthCertificateRecord = "1231231234",
                //BornCertificateMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                //},
                BornCity = new City() { Id = 2425 },
                BornState = new State() { Id = 14 },
                CURPRecord="el curp4",
                CURPOriginal=true,
                BornDate = DateTime.UtcNow.AddYears(-20),
                City = new City() { Id = 2425 },
                State = new State() { Id = 14 },
                CountryName = "Mexico4",
                CURPCopy = true,
                CURPExpeditionPlace = "las aguilas 24",
                CURPMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                CURPObservations = "todo bien con el curp4",
                Email = "elemail@elemail.com4",
                EmployeeNumber = "12312314",
                HomePhone="33121234344",
                IDCardCopy = true,
                IDCardExpeditionPlace = "en las aguilas de nuevo4",
                IDCardObservations = "Todo en orden con el idcard4",
                IDCardOriginal = true,
                IDCardRecord = "123456884",
                INEBackMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                INEFrontMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                MaritalStatus= Common.Enums.MaritalStatus.IN_JUDICIAL_PROCESS,
                Name = "Juanito 224",
                PostalCode = "455554",
                MobilPhone = "3312321324",
                //PresentedIdentificationMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                //},
                SocialSecurityProofCopy = true,
                SocialSecurityProofExpeditionPlace = "en el seguro4",
                SocialSecurityProofMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                SocialSecurityProofObservations = "todo en orden con el numero de seguridad social4",
                SocialSecurityProofOriginal = true,
                SocialSecurityProofRecord="todo bien con el nss4",
                //StudiesProofMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                //},
                StudyProofCopy = true,
                StudyProofExpeditionPlace = "en la escuela4",
                StudyProofObservations = "certificado de estuudios correcto4",
                StudyProofOriginal= true,
                StudyProofRecord = "09876543214",
                Suburb = "jardines del sol4",
                TaxRegime = "empleado4",
                TimeOnComany = "100 dias 4",
                VerificationMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                RecommendationCards = new List<RecommendationCard>()
                {
                    new RecommendationCard()
                    {
                        IssueCompany = "OXXO 5",
                        TimeWorking = "10 años 5",
                        WorkingFrom = DateTime.UtcNow.AddYears(-10),
                        WorkingTo = DateTime.UtcNow
                    },
                    new RecommendationCard()
                    {
                        IssueCompany = "7eleven 5",
                        TimeWorking = "10 años 45",
                        WorkingFrom = DateTime.UtcNow.AddYears(-10),
                        WorkingTo = DateTime.UtcNow
                    },
                    new RecommendationCard()
                    {
                        IssueCompany = "tienda de la ezquina 5",
                        TimeWorking = "10 años 5",
                        WorkingFrom = DateTime.UtcNow.AddYears(-10),
                        WorkingTo = DateTime.UtcNow
                    },
                },
                CriminalRecordLetterCopy = true,
                CriminalRecordLetterExpeditionPlace = "En la comisaria",
                //CriminalRecordMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                //},
                RFCMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                MilitaryLetterMedia = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                CriminalRecordLetterObservations = "No hay records 22criminales",
                CriminalRecordLetterOriginal = true,
                CriminalRecordLetterRecord = "criminal reco222rd",
                MilitaryLetterCopy = true,
                MilitaryLetterExpeditionPlace = "academia mi222litar",
                MilitaryLetterObservations = "Todo222 bien",
                MilitaryLetterOriginal = true,
                MilitaryLetterRecord = "222222",
                RFCCopy = true,
                RFCExpeditionPlace = "SA222222222t",
                RFCObservations = "Todo valido r2222fc",
                RFCOriginal = true,
                RFCRecord = "12623yg2222222v13r"
            };
            var response = await ApiClient.StudyGeneralInformationService.UpdateStudyGeneralInformation(request);
            Assert.IsTrue(response.Sucess);
        }



        //Recommendation card
        [TestMethod]    
        public async Task CreateRecommendationCardTest()
        {
            var request = new List<RecommendationCard>()
            {
                new RecommendationCard()
                {
                    IssueCompany = "OXXO",
                    StudyGeneralInformationId = 1,
                    TimeWorking = "10 años",
                    WorkingFrom = DateTime.UtcNow.AddYears(-10),
                    WorkingTo = DateTime.UtcNow
                },
                new RecommendationCard()
                {
                    IssueCompany = "7eleven",
                    StudyGeneralInformationId = 1,
                    TimeWorking = "10 años",
                    WorkingFrom = DateTime.UtcNow.AddYears(-10),
                    WorkingTo = DateTime.UtcNow
                },
            };

            var response = await ApiClient.StudyGeneralInformationService.CreateRecommendationCard(request);
            Assert.IsTrue(response.Sucess);
        }
        
        [TestMethod]    
        public async Task DeleteRecommendationCardTest()
        {
            var response = await ApiClient.StudyGeneralInformationService.DeleteRecommendationCard(1);
            Assert.IsTrue(response.Sucess);
        }
        
        [TestMethod]
        public async Task UpdateRecommendationCardTest()
        {
            var request = new RecommendationCard()
            {
                Id = 1,
                IssueCompany = "OXXO 2",
                TimeWorking = "10 años",
                WorkingFrom = DateTime.UtcNow.AddYears(-10),
                WorkingTo = DateTime.UtcNow
            };
            var response = await ApiClient.StudyGeneralInformationService.UpdateRecommendationCard(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetRecommendationCardTest()
        {
            var response = await ApiClient.StudyGeneralInformationService.GetRecommendationCard(new List<long>() { 1,2 });
            Assert.IsTrue(response.Sucess);
        }
    }
}
