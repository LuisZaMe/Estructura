using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class StudyTest:TestBase
    {
        [TestMethod]
        public async Task CreateStudyTest()
        {
            var study = new Study()
            {
                Candidate = new Candidate()
                {
                    Id= 7
                },
                FieldsToFill = new FieldsToFill()
                {
                    GeneralInformation = true,
                    IdentificationCardPics = true,
                    IMSSValidation  = true,
                    EconomicSituation = true,
                    Extracurricular = true,
                    EducationalLevel = true,
                    Family = true,
                    PersonalReferences = true,
                    Pictures = true,
                    RecommendationLetter = true,
                    Resume = true,
                    ScholarVerification = true,
                    Social = true,
                    WorkHistory = true
                },
                Interviewer = new Identity()
                {
                    Id = 51
                },
                ServiceType = Common.Enums.ServiceTypes.ESTUDIO_LABORAL,
                WorkStudy = new WorkStudy()
                {
                    AddressProof = true,
                    BirthCertificate = true,
                    CriminalRecordLetter = true,
                    CURP = true,
                    IdentificationCard = true,
                    MilitaryLetter = true,
                    RFC = true,
                    SocialSecurityNumber = true,
                    StudiesProof = true
                },
                SocioeconomicStudy = new SocioeconomicStudy()
                {
                    StudiesProof = true,
                    SocialSecurityNumber = true,
                    IdentificationCard = true,
                    CURP = true,
                    AddressProof = true,
                    BirthCertificate = true
                }
            };
            var response = await ApiClient.StudyService.CreateStudy(study);
            Assert.IsTrue(response.Sucess);
        }
    
        [TestMethod]
        public async Task GetStudyTest()
        {
            var response = await ApiClient.StudyService.GetStudy(
                new List<long>() { },
                0,
                DateTime.UtcNow.AddDays(-500),
                DateTime.UtcNow.AddDays(500),
                Common.Enums.ServiceTypes.ESTUDIO_SOCIOECONOMICO,
                0,
                Common.Enums.StudyStatus.NOT_STARTED,
                12,
                0,
                Common.Enums.StudyProgressStatus.UNDER_ADMON,
                0, 
                100);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task PaginationTest()
        {
            var response = await ApiClient.StudyService.Pagination(
                new List<long>() { },
                DateTime.UtcNow.AddDays(-50),
                DateTime.UtcNow.AddDays(50),
                Common.Enums.ServiceTypes.ESTUDIO_SOCIOECONOMICO,
                48,
                Common.Enums.StudyStatus.NOT_STARTED,
                12,
                8,
                Common.Enums.StudyProgressStatus.NONE,
                1
                );
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteStudyTest()
        {
            var response = await ApiClient.StudyService.DeleteStudy(17);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateStudyTest()
        {
            var study = new Study()
            {
                Id=18,
                Analyst = new Identity()
                {
                    Id = 46
                },
                StudyStatus = Common.Enums.StudyStatus.IN_PROGRESS,

            };
            var response = await ApiClient.StudyService.UpdateStudy(study);
            Assert.IsTrue(response.Sucess);
        }


        //Note

        [TestMethod]
        public async Task CreateNoteTest()
        {
            var note = new StudyNote()
            {
                StudyId = 17,
                Description = "Nota!!!!!"
            };
            var response = await ApiClient.StudyService.CreateNote(note);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateNoteTest()
        {
            var note = new StudyNote()
            {
                Id = 2,
                StudyId = 6,
                Description = "Nota!!!!!!!!!!!!!!"
            };
            var response = await ApiClient.StudyService.UpdateNote(note);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteNoteTest()
        {
            var response = await ApiClient.StudyService.DeleteNote(2);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetNotesTest()
        {
            var response = await ApiClient.StudyService.GetNotes(null, key: "No", 0, 0, 10);
            Assert.IsTrue(response.Sucess);
        }
    }
}
