using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class StudySocialTest:TestBase
    {
        // Study Social
        [TestMethod]
        public async Task CreateStudySocialTest()
        {
            var request = new StudySocial()
            {
                HealthInformation = "todo bien",
                Hobbies = "Musica",
                SocialArea = "el area",
                SocialGoalsList = new List<SocialGoals>()
                {
                    new SocialGoals()
                    {
                        CoreValue = "Dedicacion",
                        LifeGoal = "Ser lo mejor de uno mismo",
                        NextGoal = "Mejorar cada dia"
                    },
                    new SocialGoals()
                    {
                        CoreValue = "honestidad",
                        LifeGoal = "Ser lo mejor de uno mismo",
                        NextGoal = "Mejorar cada dia"
                    },
                },
                StudyId = 19,
            };

            var response = await ApiClient.StudySocialService.CreateStudySocial(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetStudySocialTest()
        {
            var response = await ApiClient.StudySocialService.GetStudySocial(new List<long>() { 1, 2, 3 });
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateStudySocialTest()
        {
            var request = new StudySocial()
            {
                HealthInformation = "todo bien 2",
                Hobbies = "Musica 2",
                SocialArea = "el area 2",
                Id = 1,
            };
            var response = await ApiClient.StudySocialService.UpdateStudySocial(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteStudySocialTest()
        {
            var response = await ApiClient.StudySocialService.DeleteStudySocial(1);
            Assert.IsTrue(response.Sucess);
        }




        // Study Social
        [TestMethod]
        public async Task CreateSocialGoalsTest()
        {
            var request = new List<SocialGoals>()
            {
                new SocialGoals()
                {
                    StudySocialId = 2,
                    CoreValue = "Dedicacion",
                    LifeGoal = "Ser lo mejor de uno mismo",
                    NextGoal = "Mejorar cada dia"
                }
            };

            var response = await ApiClient.StudySocialService.CreateSocialGoals(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetSocialGoalsTest()
        {
            var response = await ApiClient.StudySocialService.GetSocialGoals(new List<long>() { 1, 2, 5 });
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateSocialGoalsTest()
        {
            var request = new SocialGoals()
            {
                Id = 1,
                CoreValue = "Dedicacion 22",
                LifeGoal = "Ser lo mejor de uno mismo 22",
                NextGoal = "Mejorar cada dia 22"
            };
            var response = await ApiClient.StudySocialService.UpdateSocialGoals(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteSocialGoalsTest()
        {
            var response = await ApiClient.StudySocialService.DeleteSocialGoals(1);
            Assert.IsTrue(response.Sucess);
        }
    }
}
