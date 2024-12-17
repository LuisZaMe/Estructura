using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class StudyIMSSValidationTest:TestBase
    {
        [TestMethod]
        public async Task CreateStudyIMSSValidationTest()
        {
            var request = new StudyIMSSValidation()
            {
                ConciliationClaimsSummary = "Todo bien",
                CreditNumber = "125132432154",
                CreditStatus = "en progreso",
                GrantDate = DateTime.Now,
                IMSSValidationList = new List<IMSSValidation>()
                {
                    new IMSSValidation()
                    {
                        CompanyBusinessName = "La compania #1",
                        EndDate = DateTime.UtcNow.AddDays(100),
                        Result = "Todo bien por ahora",
                        StartDate = DateTime.UtcNow.AddDays(-100)
                    },
                    new IMSSValidation()
                    {
                        CompanyBusinessName = "La compania #2",
                        EndDate = DateTime.UtcNow.AddDays(100),
                        Result = "Todo super por ahora",
                        StartDate = DateTime.UtcNow.AddDays(-100)
                    },
                },
                StudyId = 17,
            };
            var response = await ApiClient.StudyIMSSValidationService.CreateStudyIMSSValidation(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateStudyIMSSValidationTest()
        {
            var request = new StudyIMSSValidation()
            {
                ConciliationClaimsSummary = "Todo bien 1",
                CreditNumber = "125132432154 1",
                CreditStatus = "en progreso 1",
                GrantDate = DateTime.Now,
                Id = 1,
            };
            var response = await ApiClient.StudyIMSSValidationService.UpdateStudyIMSSValidation(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetStudyIMSSValidationTest()
        {
            var response = await ApiClient.StudyIMSSValidationService.GetStudyIMSSValidation(new List<long>() { 1,2,3});
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteStudyIMSSValidationTest()
        {
            var response = await ApiClient.StudyIMSSValidationService.DeleteStudyIMSSValidation(1);
            Assert.IsTrue(response.Sucess);
        }



        [TestMethod]
        public async Task CreateIMSSValidationTest()
        {
            var request = new List<IMSSValidation>()
            {
                new IMSSValidation()
                {
                    CompanyBusinessName = "La compania #1",
                    EndDate = DateTime.UtcNow.AddDays(100),
                    Result = "Todo bien por ahora",
                    StartDate = DateTime.UtcNow.AddDays(-100),
                    StudyIMSSValidationId = 2
                },
                new IMSSValidation()
                {
                    CompanyBusinessName = "La compania #2",
                    EndDate = DateTime.UtcNow.AddDays(100),
                    Result = "Todo super por ahora",
                    StartDate = DateTime.UtcNow.AddDays(-100),
                    StudyIMSSValidationId = 2
                },
            };
            var response = await ApiClient.StudyIMSSValidationService.CreateIMSSValidation(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateIMSSValidationTest()
        {
            var request = new IMSSValidation()
            {
                CompanyBusinessName = "La compania #3",
                EndDate = DateTime.UtcNow.AddDays(200),
                Result = "Todo super por ahora 3",
                StartDate = DateTime.UtcNow.AddDays(-200),
                Id = 3
            };
            var response = await ApiClient.StudyIMSSValidationService.UpdateIMSSValidation(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetIMSSValidationTest()
        {
            var response = await ApiClient.StudyIMSSValidationService.GetIMSSValidation(new List<long>() { 1, 2, 3, 4 });
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteIMSSValidationTest()
        {
            var response = await ApiClient.StudyIMSSValidationService.DeleteIMSSValidation(4);
            Assert.IsTrue(response.Sucess);
        }
    }
}
