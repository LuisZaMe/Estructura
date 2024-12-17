using Estructura.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Test.ClientTest
{
    [TestClass]
    public class AccountTest: TestBase
    {
        [TestMethod]
        public async Task CreateSuperadminTest()
        {
            var user = new Identity()
            {
                //Email = Common.Utilities.EncodingHelper.EncodeTo64(TestSettings.User.EmailAddress),
                Email = Common.Utilities.EncodingHelper.EncodeTo64("luis.zamudio.mercado@gmail.com"), //yasido@hotmail.com
                Name = "SUPER",
                Lastname = "ADMIN",
                Role = Common.Enums.Role.SUPER_ADMINISTRADOR,
                Phone = "1111112222",
                Password = Common.Utilities.EncodingHelper.EncodeTo64(TestSettings.User.Password)
            };
            var response = await ApiClient.AccountService.CreateSuperadmin(user);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task CreateAccountTest()
        {
            UseAuthenticatedAPI = false;
            var user = new Identity()
            {
                //Email = Common.Utilities.EncodingHelper.EncodeTo64(TestSettings.User.EmailAddress),
                Email = Common.Utilities.EncodingHelper.EncodeTo64("cliente@test.com"),
                Name = "CLIENTW",
                Lastname = "CLIENTE",
                Role = Common.Enums.Role.CLIENTES,
                Phone = "1111112222",
                Password = Common.Utilities.EncodingHelper.EncodeTo64(TestSettings.User.Password),
                CompanyInformation = new CompanyInformation()
                {
                    CompanyName = "empresa CLIENTE",
                    CompanyPhone = "3311111111",
                    DireccionFiscal = "asd",
                    Payment = new PaymentMethod()
                    {
                        Id = 1
                    },
                    RazonSocial = "La razon 1",
                    RFC = "123123123123",
                    RegimenFiscal = "Regimen 1",

                }
            };
            var response = await ApiClient.AccountService.Create(user);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task DeleteUserAccountTest()
        {
            var response = await ApiClient.AccountService.Delete(8);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetUserAccountTest()
        {
            //var response = await ApiClient.AccountService.Get(new List<long>() {  }, 0, 10);
            var response = await ApiClient.AccountService.Search("", Common.Enums.Role.NONE, 0, 10);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task GetPendingApprovalUsersTest()
        {
            var response = await ApiClient.AccountService.Pending(0,10);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task ApproveAccountTest()
        {
            var response = await ApiClient.AccountService.Approve(1);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task RejectAccountTest()
        {
            var response = await ApiClient.AccountService.Reject(8);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task SearchUsersTest()
        {
            var response = await ApiClient.AccountService.Search("",currentPage: 0,offset: 10);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task SearchByroleTest()
        {
            var response = await ApiClient.AccountService.Role(Common.Enums.Role.NONE,0,10);
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task SendRecoverPasswordMailTest()
        {
            var response = await ApiClient.AccountService.SendRecoverPasswordMail(new Common.Request.RecoverPasswordRequest() { Email = "elcorreo.com" });
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task UpdateUserInformationTest()
        {
            var response = await ApiClient.AccountService.UpdateUserInformation(new Identity()
            {
                Name = "Ricardo name update",
                Lastname = "Lopez",
                Id = 1
            });
            Assert.IsTrue(response.Sucess);
        }

        [TestMethod]
        public async Task CompleteAccountRegistrationTest()
        {
            var response = await ApiClient.AccountService.CompleteAccountRegistration(new Common.Request.CompleteRegistration()
            {
                Password = Common.Utilities.EncodingHelper.EncodeTo64(TestSettings.User.Password),
                Token = "eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4Q0JDLUhTMjU2IiwiemlwIjoiREVGIn0..PLpytf-ofaJscx-fKofbWw.hUXAG4ExFjl_OjNPQyw0WMW-viQpiY6_8eC83c0_DS_w0xdr6s3Gb64S5k36KZzAWntYgidmOItjk6behWLKqdB7gw-ng9dWU0uwnPE1IGhjkRBbsTg6cEtOkxWM-pH4.zzSp0_UflU67RyQ4DmuvRw"
            });
            Assert.IsTrue(response.Sucess);
        }
    
        [TestMethod]
        public async Task PaginationTest()
        {
            var response = await ApiClient.AccountService.Pagination(10, "RE");
            Assert.IsTrue(response.Sucess);
        }
    }
}
