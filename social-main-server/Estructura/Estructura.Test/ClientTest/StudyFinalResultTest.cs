using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public  class StudyFinalResultTest:TestBase
    {
        [TestMethod]
        public async Task CreateStudyFinalResultTest()
        {
            var request = new StudyFinalResult()
            {
                StudyId = 17,
                PositionSummary = "Todo chido",
                AttitudeSummary = "Buena actitud",
                WorkHistorySummary = "Buen historial de trabajo",
                ArbitrationAndConciliationSummary = "concoiliacion y albitraje",
                FinalResultsBy = "Analizado por el fausto 123",
                FinalResultsPositionBy = "Cajero"
            };
            var response = await ApiClient.StudyFinalResultService.CreateStudyFinalResult(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateStudyFinalResultTest()
        {
            var request = new StudyFinalResult()
            {
                Id = 1,
                PositionSummary = "Todo chido 1",
                AttitudeSummary = "Buena actitud 1",
                WorkHistorySummary = "Buen historial de trabajo 1",
                ArbitrationAndConciliationSummary = "concoiliacion y albitraje 1",
                FinalResultsBy = "Analizado por el fausto 123 1",
                FinalResultsPositionBy = "Cajero 1"
            };
            var response = await ApiClient.StudyFinalResultService.UpdateStudyFinalResult(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetStudyFinalResultTest()
        {
            var response = await ApiClient.StudyFinalResultService.GetStudyFinalResult(new List<long>() { }, 0 , 10);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteStudyFinalResultTest()
        {
            var response = await ApiClient.StudyFinalResultService.DeleteStudyFinalResult(1);
            Assert.IsTrue(response.Sucess);
        }
    }
}
