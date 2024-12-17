using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class StudySchoolarityTest:TestBase
    {
        // Create Study schoolarity
        [TestMethod]
        public async Task CreateStudySchoolarityTest()
        {
            var request = new StudySchoolarity()
            {
                ExtracurricularActivitiesList = new List<ExtracurricularActivities>() {
                    new ExtracurricularActivities()
                    {
                        Instituution = "La institucion 123",
                        KnowledgeLevel = Common.Enums.KnowldegeLevel.INTERMEDIO,
                        Name = "El nombre del curso",
                        Period = "2022",
                        StudySchoolarityId = 3,

                    },
                    new ExtracurricularActivities()
                    {
                        Instituution = "La institucion 456",
                        KnowledgeLevel = Common.Enums.KnowldegeLevel.AVANZADO,
                        Name = "El nombre del curso avanzado",
                        Period = "2022 - 2023",
                        StudySchoolarityId = 3
                    }
                },
                ScholarityList = new List<Scholarity>() {
                    new Scholarity()
                    {
                        Career = "La carrera prrona",
                        Doccument = new Doccument()
                        {
                            Base64Doccument = TestSettings.FileTestSouurce.PDF64,
                            StoreFileType = Common.Enums.StoreFileType.PDF
                        },
                        Institution = "La institucion uno",
                        Period = "Periodo 2022",
                        SchoolarLevel = Common.Enums.SchoolarLevel.PRIMARIA,
                        TimeOnCareer = "5 a;os",
                        StudySchoolarityId = 3
                    },
                    new Scholarity()
                    {
                        Career = "La carrera prrona 2",
                        Doccument = new Doccument()
                        {
                            Base64Doccument = TestSettings.FileTestSouurce.PDF64,
                            StoreFileType = Common.Enums.StoreFileType.PDF
                        },
                        Institution = "La institucion dos",
                        Period = "Periodo 2022 - 2022",
                        SchoolarLevel = Common.Enums.SchoolarLevel.BACHILLERATO,
                        TimeOnCareer = "3 a;os",
                        StudySchoolarityId = 3
                    },

                },
                //ScholarVerificationMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.image64
                //},
                ScholarVerificationSummary = "Todo bien con sus estudios >:v!!!!!!!!!!!",
                StudyId = 18,
            };
            var response = await ApiClient.StudySchoolarityService.CreateStudySchoolarity(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteStudySchoolarityTest()
        {
            var response = await ApiClient.StudySchoolarityService.DeleteStudySchoolarity(2);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetStudySchoolarityTest()
        {
            var response = await ApiClient.StudySchoolarityService.GetStudySchoolarity(new List<long>() { 1,2,3,4,5,6,7,8,9,10,11,12,13 });
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateStudySchoolarityTest()
        {
            var request = new StudySchoolarity()
            {
                Id = 11,
                //ScholarVerificationMedia = new Media()
                //{
                //    Base64Image = TestSettings.ImageTestSource.image64
                //},
                ScholarVerificationSummary = "Todo bien con sus estudios >:v!!!!!"
            };
            var response = await ApiClient.StudySchoolarityService.UpdateStudySchoolarity(request);
            Assert.IsTrue(response.Sucess);
        }



        //Extracurricular activity


        [TestMethod]
        public async Task CreateExtracurricularActivitiesTest()
        {
            var request = new List<ExtracurricularActivities>()
            {
                new ExtracurricularActivities()
                {
                    Instituution = "La institucion 123",
                    KnowledgeLevel = Common.Enums.KnowldegeLevel.INTERMEDIO,
                    Name = "El nombre del curso",
                    Period = "2022",
                    StudySchoolarityId = 3,

                },
                new ExtracurricularActivities()
                {
                    Instituution = "La institucion 456",
                    KnowledgeLevel = Common.Enums.KnowldegeLevel.AVANZADO,
                    Name = "El nombre del curso avanzado",
                    Period = "2022 - 2023",
                    StudySchoolarityId = 3
                }
            };
            var response = await ApiClient.StudySchoolarityService.CreateExtracurricularActivities(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetExtracurricularActivitiesTest()
        {
            var response = await ApiClient.StudySchoolarityService.GetExtracurricularActivities(new List<long>() { 5,6});
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteExtracurricularActivitiesTest()
        {
            var response = await ApiClient.StudySchoolarityService.DeleteExtracurricularActivities(6);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateExtracurricularActivitiesTest()
        {
            var request = new ExtracurricularActivities()
            {
                Id = 5,
                Instituution = "La institucion 123111",
                KnowledgeLevel = Common.Enums.KnowldegeLevel.AVANZADO,
                Name = "El nombre del curso 111",
                Period = "2022 - 2023"
            };
            var response = await ApiClient.StudySchoolarityService.UpdateExtracurricularActivities(request);
            Assert.IsTrue(response.Sucess);
        }



        // Schoolarity
        [TestMethod]
        public async Task CreateSchoolarityTest()
        {
            var request = new List<Scholarity>()
            {
                new Scholarity()
                {
                    Career = "La carrera prrona",
                    Doccument = new Doccument()
                    {
                        Base64Doccument = TestSettings.FileTestSouurce.PDF64,
                        StoreFileType = Common.Enums.StoreFileType.PDF
                    },
                    Institution = "La institucion uno",
                    Period = "Periodo 2022",
                    SchoolarLevel = Common.Enums.SchoolarLevel.PRIMARIA,
                    TimeOnCareer = "5 a;os",
                    StudySchoolarityId = 3
                },
                new Scholarity()
                {
                    Career = "La carrera prrona 2",
                    Doccument = new Doccument()
                    {
                        Base64Doccument = TestSettings.FileTestSouurce.PDF64,
                        StoreFileType = Common.Enums.StoreFileType.PDF
                    },
                    Institution = "La institucion dos",
                    Period = "Periodo 2022 - 2022",
                    SchoolarLevel = Common.Enums.SchoolarLevel.BACHILLERATO,
                    TimeOnCareer = "3 a;os",
                    StudySchoolarityId = 3
                },
            };

            var response = await ApiClient.StudySchoolarityService.CreateSchoolarity(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateSchoolarityTest()
        {
            var request = new Scholarity()
            {
                Id = 12,
                Career = "La carrera prrona 2",
                Doccument = new Doccument()
                {
                    Base64Doccument = TestSettings.FileTestSouurce.PDF64,
                    StoreFileType = Common.Enums.StoreFileType.PDF
                },
                Institution = "La institucion dos",
                Period = "Periodo 2023",
                SchoolarLevel = Common.Enums.SchoolarLevel.PRIMARIA,
                TimeOnCareer = "44 a;os",
                StudySchoolarityId = 3
            };
            var response = await ApiClient.StudySchoolarityService.UpdateSchoolarity(request);
            Assert.IsTrue(response.Sucess);    
        }

        [TestMethod]
        public async Task GetSchoolarityTest()
        {
            var response = await ApiClient.StudySchoolarityService.GetSchoolarity(new List<long>() { 10, 9, 8, 7 });
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteSchoolarityTest()
        {
            var response = await ApiClient.StudySchoolarityService.DeleteSchoolarity(10);
            Assert.IsTrue(response.Sucess);
        }

    }
}
