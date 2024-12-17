using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class StudyPersonalReferenceTest:TestBase
    {
        [TestMethod]
        public async Task CreateStudyPersonalReferenceTest()
        {
            var request = new List<StudyPersonalReference>()
            {
                new StudyPersonalReference()
                {
                    Address = "La casa 1",
                    CurrentJob = "Oxxo",
                    KnowAddress = "La del oxxo",
                    KnowledgeAboutPreviousJobs = "Farmacia GDL",
                    Name = "El nombre",
                    OpinionAboutTheCandidate = "Todo bien",
                    Phone = "1235624241",
                    PoliticalActivity = "No",
                    ReferenceTitle = "Referencia 1",
                    StudyId = 17,
                    WouldYouRecommendIt = "Si",
                    YearsKnowingEachOther = "30",
                    YearsOnCurrentResidence = "5",
                },
                new StudyPersonalReference()
                {
                    Address = "La casa 2",
                    CurrentJob = "Oxxo2",
                    KnowAddress = "La del oxxo 2",
                    KnowledgeAboutPreviousJobs = "Farmacia GDL 2",
                    Name = "El nombre 2",
                    OpinionAboutTheCandidate = "Todo bien x2",
                    Phone = "123562424112",
                    PoliticalActivity = "Si",
                    ReferenceTitle = "Referencia 2",
                    StudyId = 17,
                    WouldYouRecommendIt = "Si",
                    YearsKnowingEachOther = "35",
                    YearsOnCurrentResidence = "15",
                },

            };
            var response = await ApiClient.StudyPersonalReferenceService.CreateStudyPersonalReference(request);
            Assert.IsTrue(response.Sucess);
        }
        
        [TestMethod]
        public async Task UpdateStudyPersonalReferenceTest()
        {
            var request = new StudyPersonalReference()
            {
                Address = "La casa 3",
                CurrentJob = "Oxxo3",
                KnowAddress = "La del oxxo3",
                KnowledgeAboutPreviousJobs = "Farmacia GDL3",
                Name = "El nombre3",
                OpinionAboutTheCandidate = "Todo bien3",
                Phone = "12356242413",
                PoliticalActivity = "No3",
                ReferenceTitle = "Referencia 3",
                Id = 1,
                WouldYouRecommendIt = "Si",
                YearsKnowingEachOther = "30",
                YearsOnCurrentResidence = "5",
            };
            var response = await ApiClient.StudyPersonalReferenceService.UpdateStudyPersonalReference(request);
            Assert.IsTrue(response.Sucess);
        }
        
        [TestMethod]
        public async Task GetStudyPersonalReferenceTest()
        {
            var response = await ApiClient.StudyPersonalReferenceService.GetStudyPersonalReference(new List<long>() { 1,2,3});
            Assert.IsTrue(response.Sucess);
        }
        
        [TestMethod]
        public async Task DeleteStudyPersonalReferenceTest()
        {
            var response = await ApiClient.StudyPersonalReferenceService.DeleteStudyPersonalReference(1);
            Assert.IsTrue(response.Sucess);
        }
    }
}
