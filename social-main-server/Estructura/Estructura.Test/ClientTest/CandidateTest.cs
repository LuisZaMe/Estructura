using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class CandidateTest:TestBase
    {
        [TestMethod]
        public async Task CreateCandidateTest()
        {
            var request = new Candidate()
            {
                Address = "Mi casa 1243",
                CURP = "123123123313",
                Email = "miemail@mail.com3",
                State = new State()
                {
                    Id = 1
                },
                City = new City()
                {
                    Id = 43
                },
                Client = new Identity()
                {
                    Id = 12
                },
                Lastname = "lastname34",
                Name = "el nombre34",
                NSS = "321321321124",
                Phone = "11332233124",
                Position = "Janitor4",
                
            };
            var response = await ApiClient.CandidateService.CreateCandidate(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetCandidateTest()
        {
            var response = await ApiClient.CandidateService.GetCandidate(new List<long>() {  },0,100);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateCandidate()
        {
            var request = new Candidate()
            {
                Id = 6,
                Address = "Mi casa 12356",
                CURP = "12312311232331",
                Email = "miemail@mail.commm",
                Media = new Media()
                {
                    Base64Image = TestSettings.ImageTestSource.imageLopez64
                },
                CandidateStatus = Common.Enums.CandidateStatus.IN_PROGRESS
            };
            var response = await ApiClient.CandidateService.UpdateCandidate(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task PaginationTest()
        {
            var response = await ApiClient.CandidateService.Pagination("CAN",0, 10);
            Assert.IsTrue(response.Sucess);
        }
    
        [TestMethod]
        public async Task DeleteTest()
        {
            var response = await ApiClient.CandidateService.DeleteCandidate(3);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task SearchTest()
        {
            var response = await ApiClient.CandidateService.SearchCandidate("can", 0, 0 , 20);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task CreateNoteTest()
        {
            var note = new CandidateNote()
            {
                CandidateId = 6,
                Description = "Nota!"
            };
            var response = await ApiClient.CandidateService.CreateNote(note);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateNoteTest()
        {
            var note = new CandidateNote()
            {
                Id = 2,
                CandidateId = 6,
                Description = "Nota!!!"
            };
            var response = await ApiClient.CandidateService.UpdateNote(note);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteNoteTest()
        {
            var response = await ApiClient.CandidateService.DeleteNote(2);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetNotesTest()
        {
            var response = await ApiClient.CandidateService.GetNotes(null, key: "No", 0, 0, 10);
            Assert.IsTrue(response.Sucess);
        }
    }
}
