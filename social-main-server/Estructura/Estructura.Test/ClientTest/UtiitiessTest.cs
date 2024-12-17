using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class UtiitiessTest : TestBase
    {

        [TestMethod]
        public async Task GetCitiesTest()
        {
            var response = await ApiClient.UtilitiesService.GetCities(1);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetStatesTest()
        {
            var response = await ApiClient.UtilitiesService.GetStates();
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetCurrentVersionTest()
        {
            var response = await ApiClient.UtilitiesService.GetCurrentVersion();
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task IsAppCompatibleTest()
        {
            var response = await ApiClient.UtilitiesService.IsAppCompatible(1);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task VerifyEmailExistTest()
        {
            string email = "ricardo.belmont21@gmail.com";
            var response = await ApiClient.UtilitiesService.VerifyEmailExist(email);
            Assert.IsTrue(response.Sucess);
        }
    }
}
