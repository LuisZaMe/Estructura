using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class StudyPicturesTest:TestBase
    {
        [TestMethod]
        public async Task CreateStudyPicturesTest()
        {
            var request = new StudyPictures()
            {
                StudyId = 17,
                Media4 = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                Media5 = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                Media6 = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                }
            };
            var response = await ApiClient.StudyPicturesService.CreateStudyPictures(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteStudyPicturesTest()
        {
            var response = await ApiClient.StudyPicturesService.DeleteStudyPictures(1);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetStudyPicturesTest()
        {
            var response = await ApiClient.StudyPicturesService.GetStudyPictures(new List<long>() { 1, 2, 3});
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateStudyPicturesTest()
        {
            var request = new StudyPictures()
            {
                Id = 2,
                Media4 = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                Media5 = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.image64
                },
                Media6 = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                }
            };
            var response = await ApiClient.StudyPicturesService.UpdateStudyPictures(request);
            Assert.IsTrue(response.Sucess);
        }
    }
}
