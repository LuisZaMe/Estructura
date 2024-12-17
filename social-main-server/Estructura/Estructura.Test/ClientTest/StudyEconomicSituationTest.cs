using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class StudyEconomicSituationTest:TestBase
    {
        //StudyEconomicSituation
        [TestMethod]
        public async Task CreateStudyEconomicSituationTest()
        {
            var request = new StudyEconomicSituation()
            {
                StudyId = 18,
                AdditionalIncomingList = new List<AdditionalIncoming>()
                {
                    new AdditionalIncoming()
                    {
                        Amount = 1000,
                        Activity = "Juegos de azar",
                        TimeFrame = "Jugar cartas"
                    },
                    new AdditionalIncoming()
                    {
                        Amount = 1200,
                        Activity = "Deportes",
                        TimeFrame = "baseball"
                    }
                },
                Cable = 100,
                Cellphone = 800,
                Clothing = 200,
                CreditList = new List<Credit>()
                {
                    new Credit()
                    {
                        AccountNumber = "1345t3fadsda",
                        Bank = "el banco de hierro",
                        CreditLimit = 99999,
                        Debt = 2000
                    }
                },
                Credits = 2000,
                EconomicSituationSummary = "Pasable",
                Electricity = 600,
                Entertainment = 999,
                EstateList = new List<Estate>()
                {
                    new Estate()
                    {
                        AcquisitionMethod = "Por mi",
                        AcquisitionDate = DateTime.UtcNow,
                        Concept = "Casa",
                        CurrentValue = 9999,
                        Owner = "yo",
                        PurchaseValue = 9000
                    }
                },
                Food = 642,
                Gas = 0,
                Gasoline = 1660,
                IncomingList = new List<Incoming>()
                {
                    new Incoming()
                    {
                        Amount = 40000,
                        Name = "Sueldos",
                        Relationship = "Laboral"
                    }
                },
                Infonavit = 124,
                Internet = 899,
                Maintenance = 300,
                Miscellaneous = 100,
                PropertyTax = 2000,
                Rent = 16000,
                Schoolar = 3000,
                VehicleList = new List<Vehicle>()
                {
                    new Vehicle()
                    {
                        BrandAndModel = "Ford",
                        CurrentValue = 160000,
                        Owner = "yo",
                        Plates = "asd123",
                        PurchaseValue = 80000,
                        SerialNumber = "123321345543"
                    }
                },
                Water = 80
            };
            request = new StudyEconomicSituation()
            {
                StudyId = 18,
                Cable = 100,
                Cellphone = 800,
                Clothing = 200,
                Credits = 2000,
                EconomicSituationSummary = "Pasable",
                Electricity = 600,
                Entertainment = 999,
                Food = 642,
                Gas = 0,
                Gasoline = 1660,
                Infonavit = 124,
                Internet = 899,
                Maintenance = 300,
                Miscellaneous = 100,
                PropertyTax = 2000,
                Rent = 16000,
                Schoolar = 3000,
                Water = 80
            };

            var response = await ApiClient.StudyEconomicSituationService.CreateStudyEconomicSituation(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetStudyEconomicSituationTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.GetStudyEconomicSituation(new List<long>() { 1, 2, 3, 4});
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateStudyEconomicSituationTest()
        {
            var request = new StudyEconomicSituation()
            {
                Id = 1,
                Cable = 100,
                Cellphone = 800,
                Clothing = 200,
                Credits = 2000,
                EconomicSituationSummary = "Pasable chido",
                Electricity = 600,
                Entertainment = 999,
                Food = 642,
                Gas = 0,
                Gasoline = 1660,
                Infonavit = 124,
                Internet = 899,
                Maintenance = 300,
                Miscellaneous = 100,
                PropertyTax = 2000,
                Rent = 16000,
                Schoolar = 3000,
                StudyId = 17,
                Water = 80
            };
            var response = await ApiClient.StudyEconomicSituationService.UpdateStudyEconomicSituation(request);
            Assert.IsTrue(response.Sucess);
        }
        
        [TestMethod]
        public async Task DeleteStudyEconomicSituationTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.DeleteStudyEconomicSituation(2);
            Assert.IsTrue(response.Sucess);
        }



        //Incoming
        [TestMethod]
        public async Task CreateIncomingTest()
        {
            var request = new List<Incoming>()
            {
                new Incoming()
                {
                    Amount = 50000,
                    Name = "Sueldos 2",
                    Relationship = "Laboral 2",
                    StudyEconomicSituationId = 1
                }
            };

            var response = await ApiClient.StudyEconomicSituationService.CreateIncoming(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetStudyIncomingTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.GetIncoming(new List<long>() { 1,2});
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateIncomingTest()
        {
            var request = new Incoming()
            {
                Id = 1,
                Amount = 60000,
                Name = "Sueldos 2",
                Relationship = "Laboral 2",
                StudyEconomicSituationId = 1
            };
            var response = await ApiClient.StudyEconomicSituationService.UpdateIncoming(request);
            Assert.IsTrue(response.Sucess);
        }
        
        [TestMethod]
        public async Task DeleteIncomingTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.DeleteIncoming(1);
            Assert.IsTrue(response.Sucess);
        }



        //AdditionalIncoming
        [TestMethod]
        public async Task CreateAdditionalIncomingTest()
        {
            var request = new List<AdditionalIncoming>()
                {
                    new AdditionalIncoming()
                    {
                        Amount = 50000,
                        Activity = "Sueldos 2",
                        TimeFrame = "Laboral 2",
                        StudyEconomicSituationId = 1
                    }
                };

            var response = await ApiClient.StudyEconomicSituationService.CreateAdditionalIncoming(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetAdditionalIncomingTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.GetAdditionalIncoming(new List<long>() { 1, 2, 3 });
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateAdditionalIncomingTest()
        {
            var request = new AdditionalIncoming()
            {
                Id = 1,
                Amount = 60000,
                Activity = "Sueldos 2",
                TimeFrame = "Laboral 2",
                StudyEconomicSituationId = 1
            };
            var response = await ApiClient.StudyEconomicSituationService.UpdateAdditionalIncoming(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteAdditionalIncomingTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.DeleteAdditionalIncoming(2);
            Assert.IsTrue(response.Sucess);
        }



        //Credit
        [TestMethod]
        public async Task CreateCreditTest()
        {
            var request = new List<Credit>()
                {
                    new Credit()
                    {
                        StudyEconomicSituationId = 1,
                        AccountNumber = "123426780987654",
                        Bank = "banco de hierro",
                        CreditLimit = 1000000,
                        Debt = 20
                    }
                };

            var response = await ApiClient.StudyEconomicSituationService.CreateCredit(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetCreditTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.GetCredit(new List<long>() { 1, 2, 3 });
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateCreditTest()
        {
            var request = new Credit()
            {
                Id = 1,
                AccountNumber = "123426780987654qwe",
                Bank = "banco de hierro 11",
                CreditLimit = 10022000,
                Debt = 500
            };
            var response = await ApiClient.StudyEconomicSituationService.UpdateCredit(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteCreditTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.DeleteCredit(2);
            Assert.IsTrue(response.Sucess);
        }





        //Estate
        [TestMethod]
        public async Task CreateEstateTest()
        {
            var request = new List<Estate>()
                {
                    new Estate()
                    {
                        StudyEconomicSituationId = 1,
                        AcquisitionMethod = "por mi",
                        AcquisitionDate = DateTime.UtcNow,
                        Concept = "casas",
                        CurrentValue = 1000,
                        Owner = "sho",
                        PurchaseValue = 500
                    }
                };

            var response = await ApiClient.StudyEconomicSituationService.CreateEstate(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetEstateTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.GetEstate(new List<long>() { 1, 2, 3 });
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateEstateTest()
        {
            var request = new Estate()
            {
                Id = 1,
                AcquisitionMethod = "por mi 2",
                AcquisitionDate = DateTime.UtcNow,
                Concept = "casas",
                CurrentValue = 1000,
                Owner = "sho",
                PurchaseValue = 500
            };
            var response = await ApiClient.StudyEconomicSituationService.UpdateEstate(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteEstateTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.DeleteEstate(2);
            Assert.IsTrue(response.Sucess);
        }



        //Estate
        [TestMethod]
        public async Task CreateVehicleTest()
        {
            var request = new List<Vehicle>()
                {
                    new Vehicle()
                    {
                        StudyEconomicSituationId = 1,
                        BrandAndModel = "Ford",
                        Plates = "1251gbv13g1t",
                        SerialNumber = "21490493434806801",
                        CurrentValue = 1000,
                        Owner = "sho",
                        PurchaseValue = 500
                    }
                };

            var response = await ApiClient.StudyEconomicSituationService.CreateVehicle(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetVehicleTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.GetVehicle(new List<long>() { 1, 2, 3 });
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateVehicleTest()
        {
            var request = new Vehicle()
            {
                Id = 1,
                BrandAndModel = "Ford 2",
                Plates = "1251gbv13g1t1212",
                SerialNumber = "2149049343480680132323",
                CurrentValue = 10080,
                Owner = "sho",
                PurchaseValue = 5030
            };
            var response = await ApiClient.StudyEconomicSituationService.UpdateVehicle(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteVehicleTest()
        {
            var response = await ApiClient.StudyEconomicSituationService.DeleteVehicle(2);
            Assert.IsTrue(response.Sucess);
        }
    }
}
