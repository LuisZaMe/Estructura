using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class MediaTest: TestBase
    {
        [TestMethod]
        public async Task CreateMediaTest()
        {
            var response = await ApiClient.MediaService.CreateMedia(new Common.Models.Media()
            {
                Base64Image = TestSettings.ImageTestSource.image64,
                StoreMediaType = Common.Enums.StoreMediaType.GENERAL
            });
            Assert.IsTrue(response.Sucess);
        }
    }
}
