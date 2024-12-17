using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class FileTest:TestBase
    {
        [TestMethod]
        public async Task CreateFileTest()
        {
            var request = new Doccument()
            {
                Base64Doccument = TestSettings.FileTestSouurce.PDF64,
                StoreFileType = Common.Enums.StoreFileType.PDF,
                StoreMediaType = Common.Enums.StoreMediaType.EVIDENCE// only for test
            };
            var response = await ApiClient.FileService.CreateFile(request);
            Assert.IsTrue(response.Sucess);
        }
    }
}
