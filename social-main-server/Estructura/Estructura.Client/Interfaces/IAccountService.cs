using Estructura.Common.Models;
using Estructura.Common.Request;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IAccountService
    {
        Task<GenericResponse<Identity>> CreateSuperadmin(Identity request);
        Task<GenericResponse<Identity>> Create(Identity request);
        Task<GenericResponse<Identity>> Delete(long Id);
        Task<GenericResponse<List<Identity>>> Get(List<long> Id, int currentPage, int offset);
        Task<GenericResponse<List<Identity>>> Pending(int currentPage, int offset);
        Task<GenericResponse<Identity>> Approve(long Id);
        Task<GenericResponse<Identity>> Reject(long Id);
        Task<GenericResponse<List<Identity>>> Search(string key, Common.Enums.Role role = Common.Enums.Role.NONE, int currentPage = 0, int offset = 10);
        Task<GenericResponse<List<Identity>>> Role(Common.Enums.Role role, int currentPage, int offset);
        Task<GenericResponse<bool>> SendRecoverPasswordMail(RecoverPasswordRequest request);
        Task<GenericResponse<bool>> RecoverPasswordByMail(CompleteRecoverPasswordRequest request);
        Task<GenericResponse<Identity>> UpdateUserInformation(Identity request);
        Task<GenericResponse<Identity>> CompleteAccountRegistration(CompleteRegistration request);
        Task<GenericResponse<int>> Pagination(int splitBy, string key, Common.Enums.Role role = Common.Enums.Role.NONE);
    }
}
