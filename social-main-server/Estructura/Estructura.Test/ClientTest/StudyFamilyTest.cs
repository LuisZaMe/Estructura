using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class StudyFamilyTest:TestBase
    {
        //Study Family
        [TestMethod]
        public async Task CreateStudyFamilyTest()
        {
            var request = new StudyFamily()
            {
                FamiliarArea = "Trabaja en el oxxo 3",
                LivingFamilyList = new List<LivingFamily>()
                {
                    new LivingFamily()
                    {
                        Address = "la casa living family",
                        Age = "30",
                        MaritalStatus = Common.Enums.MaritalStatus.DIVORCED,
                        Name = "el nombre living family",
                        Occupation = "Trabaja en el oxxo living family",
                        Phone = "4433221122",
                        Relationship = "Familiar living family",
                        Schoolarity = "Prepa living family"
                    },
                    new LivingFamily()
                    {
                        Address = "la casa living family 2",
                        Age = "31",
                        MaritalStatus = Common.Enums.MaritalStatus.DIVORCED,
                        Name = "el nombre living family 2",
                        Occupation = "Trabaja en el oxxo living family 2",
                        Phone = "44332211223",
                        Relationship = "Familiar living family 2",
                        Schoolarity = "Prepa living family 2"
                    }
                },
                NoLivingFamilyList = new List<NoLivingFamily>()
                {
                    new NoLivingFamily()
                    {
                        Address = "la casa no living family",
                        Age = "30",
                        MaritalStatus = Common.Enums.MaritalStatus.MARIED,
                        Name = "el nombre  no living family",
                        Occupation = "Trabaja en el oxxo no living family",
                        Phone = "44332211122",
                        Relationship = "Familiar no living family",
                        Schoolarity = "Prepa no living family"
                    },
                    new NoLivingFamily()
                    {
                        Address = "la casa no living family 2",
                        Age = "31",
                        MaritalStatus = Common.Enums.MaritalStatus.CONCUBINAGE,
                        Name = "el nombre no living family 2",
                        Occupation = "Trabaja en el oxxo no living family 2",
                        Phone = "443322111223",
                        Relationship = "Familiar no living family 2",
                        Schoolarity = "Prepa no living family 2"
                    }
                },
                Notes = "Todo bien en las notas >:v",
                StudyId = 18
            };

            var response = await ApiClient.StudyFamilyService.CreateStudyFamily(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetStudyFamilyTest()
        {
            var response = await ApiClient.StudyFamilyService.GetStudyFamily(new List<long>() { 1,2,3});
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateStudyFamilyTest()
        {
            var request = new StudyFamily()
            {
                FamiliarArea = "Trabaja en el oxxo 33123",
                Notes = "Todo bien en las notas >:v!!!!!!!!!!!!",
                Id = 2
            };
            var response = await ApiClient.StudyFamilyService.UpdateStudyFamily(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteStudyFamilyTest()
        {
            var response = await ApiClient.StudyFamilyService.DeleteStudyFamily(2);
            Assert.IsTrue(response.Sucess);
        }



        //LivingFamily
        [TestMethod]
        public async Task CreateLivingFamilyTest()
        {
            var request = new List<LivingFamily>()
            {
                new LivingFamily()
                {
                    Address = "la casa living family1323",
                    Age = "303123",
                    MaritalStatus = Common.Enums.MaritalStatus.CONCUBINAGE,
                    Name = "el nombre living family123",
                    Occupation = "Trabaja en el oxxo living family123",
                    Phone = "4433221122123",
                    Relationship = "Familiar living family123",
                    Schoolarity = "Prepa living family123",
                    StudyFamilyId = 3
                }
            };
            var response = await ApiClient.StudyFamilyService.CreateLivingFamily(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateLivingFamilyTest()
        {
            var request = new LivingFamily()
            {
                Address = "la casa living family1323",
                Age = "303123",
                MaritalStatus = Common.Enums.MaritalStatus.CONCUBINAGE,
                Name = "el nombre living family123",
                Occupation = "Trabaja en el oxxo living family123",
                Phone = "4433221122123",
                Relationship = "Familiar living family123",
                Schoolarity = "Prepa living family123",
                StudyFamilyId = 3,
                Id = 4
            };
            var response = await ApiClient.StudyFamilyService.UpdateLivingFamily(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetLivingFamilyTest()
        {
            var response = await ApiClient.StudyFamilyService.GetLivingFamily(new List<long>() { 3,4,5});
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteLivingFamilyTest()
        {
            var response = await ApiClient.StudyFamilyService.DeleteLivingFamily(4);
            Assert.IsTrue(response.Sucess);
        }



        //NoLivingFamily
        [TestMethod]
        public async Task CreateNoLivingFamilyTest()
        {
            var request = new List<NoLivingFamily>()
            {
                new NoLivingFamily()
                {
                    Address = "la casa no living family",
                    Age = "30",
                    MaritalStatus = Common.Enums.MaritalStatus.WIDOWER,
                    Name = "el nombre  no living family",
                    Occupation = "Trabaja en el oxxo no living family",
                    Phone = "44332211122",
                    Relationship = "Familiar no living family",
                    Schoolarity = "Prepa no living family",
                    StudyFamilyId = 3,
                }
            };
            var response = await ApiClient.StudyFamilyService.CreateNoLivingFamily(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateNoLivingFamilyTest()
        {
            var request = new NoLivingFamily()
            {
                Address = "la casa no living family 123",
                Age = "30",
                MaritalStatus = Common.Enums.MaritalStatus.WIDOWER,
                Name = "el nombre  no living family",
                Occupation = "Trabaja en el oxxo no living family123",
                Phone = "44332211122123",
                Relationship = "Familiar no living family123",
                Schoolarity = "Prepa no living family123",
                Id = 5,
            };
            var response = await ApiClient.StudyFamilyService.UpdateNoLivingFamily(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetNoLivingFamilyTest()
        {
            var response = await ApiClient.StudyFamilyService.GetNoLivingFamily(new List<long>() { 3,4,5});
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteNoLivingFamilyTest()
        {
            var response = await ApiClient.StudyFamilyService.DeleteNoLivingFamily(5);
            Assert.IsTrue(response.Sucess);
        }
    }
}
