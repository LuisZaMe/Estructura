using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class StudyLaboralTrayectoryTest:TestBase
    {
        [TestMethod]
        public async Task CreateStudyLaboralTrayectoryTest()
        {
            var request = new List<StudyLaboralTrayectory>()
            {
                new StudyLaboralTrayectory()
                {
                    CandidateAddress = "direccion candidato",
                    CandidateBenefits = "Beneficios candidato",
                    CandidateBusinessName = "Candidato business name",
                    CandidateDirectBoss = "Candidato jefe",
                    CandidateEndDate = DateTime.UtcNow,
                    CandidateEndSalary = 40000,
                    CandidateFinalRole = "Dev",
                    CandidateInitialRole = "dev",
                    CandidateLaborUnion = "El sindicato",
                    CandidatePhone = "3322112233",
                    CandidateResignationReason = "Mejorar",
                    CandidateRole = "El rol del candidato",
                    CandidateStartDate = DateTime.UtcNow.AddYears(-10),
                    CandidateStartSalary = 25000,
                    CompanyAddress = "direccion Company",
                    CompanyBenefits = "Beneficios Company",
                    CompanyBusinessName = "Company business name",
                    CompanyDirectBoss = "Company jefe",
                    CompanyEndDate = DateTime.UtcNow,
                    CompanyEndSalary = 40000,
                    CompanyFinalRole = "Dev",
                    CompanyInitialRole = "dev",
                    CompanyLaborUnion = "El sindicato",
                    CompanyPhone = "3322112233",
                    CompanyResignationReason = "Mejorar",
                    CompanyRole = "El rol del Company",
                    CompanyStartDate = DateTime.UtcNow.AddYears(-10),
                    CompanyStartSalary = 25000,
                    CompanyName = "La compania",
                    Media1 = new Media()
                    {
                        Base64Image = TestSettings.ImageTestSource.imageLopez64
                    },
                    //Media2 = new Media()
                    //{
                    //    Base64Image = TestSettings.ImageTestSource.image64
                    //},
                    Notes = "Las notas!",
                    Observations = "Todo chido",
                    Recommended = "Si, recomendado",
                    RecommendedReasonWhy = "Se ve chido",
                    Rehire = "Si",
                    RehireReason = "Trabaja chido",
                    StudyId = 19,
                    TrayectoryName= "Trayectoria 1"
                },
                new StudyLaboralTrayectory()
                {
                    CandidateAddress = "direccion candidato 2",
                    CandidateBenefits = "Beneficios candidato2",
                    CandidateBusinessName = "Candidato business name 2",
                    CandidateDirectBoss = "Candidato jefe 2",
                    CandidateEndDate = DateTime.UtcNow,
                    CandidateEndSalary = 50000,
                    CandidateFinalRole = "Dev 2",
                    CandidateInitialRole = "dev 2",
                    CandidateLaborUnion = "El sindicato 2",
                    CandidatePhone = "33221122332",
                    CandidateResignationReason = "Mejorar2",
                    CandidateRole = "El rol del candidato2",
                    CandidateStartDate = DateTime.UtcNow.AddYears(-10),
                    CandidateStartSalary = 25000,
                    CompanyAddress = "direccion Company2",
                    CompanyBenefits = "Beneficios Company2",
                    CompanyBusinessName = "Company business name2",
                    CompanyDirectBoss = "Company jefe2",
                    CompanyEndDate = DateTime.UtcNow,
                    CompanyEndSalary = 50000,
                    CompanyFinalRole = "Dev 2",
                    CompanyInitialRole = "dev 2",
                    CompanyLaborUnion = "El sindicato2",
                    CompanyPhone = "33221122332",
                    CompanyResignationReason = "Mejorar 2",
                    CompanyRole = "El rol del Company 2",
                    CompanyStartDate = DateTime.UtcNow.AddYears(-10),
                    CompanyStartSalary = 30000,
                    CompanyName = "La compania 2",
                    //Media1 = new Media()
                    //{
                    //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                    //},
                    Media2 = new Media()
                    {
                        Base64Image = TestSettings.ImageTestSource.image64
                    },
                    Notes = "Las notas! 2",
                    Observations = "Todo chido 2",
                    Recommended = "Si, recomendado 2",
                    RecommendedReasonWhy = "Se ve chido 2",
                    Rehire = "Si 2",
                    RehireReason = "Trabaja chido 2",
                    StudyId = 20,
                    TrayectoryName= "Trayectoria 1 2"
                }
            };
            var response = await ApiClient.StudyLaboralTrayectoryService.CreateStudyLaboralTrayectory(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetStudyLaboralTrayectoryTest()
        {
            var response = await ApiClient.StudyLaboralTrayectoryService.GetStudyLaboralTrayectory(new List<long>() { 1, 2, 3});
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateStudyLaboralTrayectoryTest()
        {
            var request =  new StudyLaboralTrayectory()
                {
                    CandidateAddress = "direccion candidato 22",
                    CandidateBenefits = "Beneficios candidato22",
                    CandidateBusinessName = "Candidato business name 22",
                    CandidateDirectBoss = "Candidato jefe 22",
                    CandidateEndDate = DateTime.UtcNow,
                    CandidateEndSalary = 50000,
                    CandidateFinalRole = "Dev 22",
                    CandidateInitialRole = "de22v 22",
                    CandidateLaborUnion = "El si22ndicato 22",
                    CandidatePhone = "3322112222332",
                    CandidateResignationReason = "Mejorar22",
                    CandidateRole = "El rol del candidato22",
                    CandidateStartDate = DateTime.UtcNow.AddYears(-10),
                    CandidateStartSalary = 25000,
                    CompanyAddress = "direccion Company22",
                    CompanyBenefits = "Beneficios Company2",
                    CompanyBusinessName = "Company business name22",
                    CompanyDirectBoss = "Company jefe22",
                    CompanyEndDate = DateTime.UtcNow,
                    CompanyEndSalary = 50000,
                    CompanyFinalRole = "Dev 22",
                    CompanyInitialRole = "dev 22",
                    CompanyLaborUnion = "El sindicato22",
                    CompanyPhone = "332211223322",
                    CompanyResignationReason = "Mejorar 22",
                    CompanyRole = "El rol del Company 22",
                    CompanyStartDate = DateTime.UtcNow.AddYears(-10),
                    CompanyStartSalary = 30000,
                    CompanyName = "La compania 22",
                    //Media1 = new Media()
                    //{
                    //    Base64Image = TestSettings.ImageTestSource.imageLopez64
                    //},
                    Media2 = new Media()
                    {
                        Base64Image = TestSettings.ImageTestSource.image64
                    },
                    Notes = "Las notas! 2",
                    Observations = "Todo chido 22",
                    Recommended = "Si, recomendado 22",
                    RecommendedReasonWhy = "Se ve chido 22",
                    Rehire = "Si 2",
                    RehireReason = "Trabaja chido 22",
                    StudyId = 17,
                    TrayectoryName= "Trayectoria 1 22222",
                    Id = 26
                };

            var response = await ApiClient.StudyLaboralTrayectoryService.UpdateStudyLaboralTrayectory(request);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteStudyLaboralTrayectoryTest()
        {
            var response = await ApiClient.StudyLaboralTrayectoryService.DeleteStudyLaboralTrayectory(1);
            Assert.IsTrue(response.Sucess);
        }
    }
}
