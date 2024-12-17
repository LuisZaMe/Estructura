using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class VisitTest:TestBase
    {
        [TestMethod]
        public async Task CreateVisitTest()
        {
            var visit = new Visit()
            {
                Address = "The address 1",
                City = new City()
                {//zapopan
                    Id= 2425
                },
                State = new State()
                {//jalisco
                    Id = 14
                },
                NotationColor = "#555555",
                Observations = "Todo chido de nuevo",
                Study = new Study() { Id = 18 },
                VisitDate = DateTime.UtcNow.AddDays(25).ToString()
            };
            var response = await ApiClient.VisitService.CreateVisit(visit);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetVisitTest()
        {
            var response = await ApiClient.VisitService.GetVisit(
                new List<long>(),
                51,
                7,
                12,
                18,
                new DateTime(2022, 7, 8),
                new DateTime(2022, 9, 9),
                2425,
                14,
                Common.Enums.VisitStatus.VISIT_NOT_STARTED,
                0,
                10);

            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateVisitTest()
        {
            var response = await ApiClient.VisitService.UpdateVisit(new Visit()
            {
                Id = 2,
                VisitDate = DateTime.UtcNow.AddDays(30).ToString(),
                ConfirmAssistance = false,
                VisitStatus = Common.Enums.VisitStatus.VISIT_NOT_STARTED,
                City = new City()
                {//zapopan
                    Id= 2425
                },
                State = new State()
                {//jalisco
                    Id = 14
                },
                Address = "The address 2",
                Observations = "Todo chido prron en el update",
                NotationColor = "#999111",
                Evidence = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.image64,
                }
            });

            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task PaginationTest()
        {
            var response = await ApiClient.VisitService.Pagination(
                new List<long>(),
                51,
                7,
                12,
                18,
                new DateTime(2022, 7, 8),
                new DateTime(2022, 9, 9),
                2425,
                14,
                Common.Enums.VisitStatus.VISIT_NOT_STARTED,
                1);

            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteVisitTest()
        {
            var response = await ApiClient.VisitService.DeleteVisit(2);
            Assert.IsTrue(response.Sucess);
        }
    }
}
