using Estructura.Common.Models;
using Estructura.Common.Request;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Services
{
    public interface IAccountService
    {
        Task<GenericResponse<Identity>> CreateSuperAdmin(Identity request);
        Task<GenericResponse<Identity>> CreateAccount(Identity request);
        Task<GenericResponse<Identity>> DeleteUserAccount(long UserID);
        Task<GenericResponse<List<Identity>>> GetPendingApprovalUsers(int currentPage, int offset);
        Task<GenericResponse<Identity>> ApproveAccount(Identity user);
        Task<GenericResponse<Identity>> UnapproveAccount(Identity user);
        Task<GenericResponse<List<Identity>>> GetActiveUsers(List<long> Id, int currentPage, int offset);
        Task<GenericResponse<List<Identity>>> SearchUsers(string key, bool showSuperAdmin = false, Common.Enums.Role role = Common.Enums.Role.NONE, int currentPage = 0, int offset = 10);
        Task<GenericResponse<List<Identity>>> GetUsersByRole(Common.Enums.Role Id, int currentPage, int offset);
        Task<GenericResponse<bool>> SendRecoverPasswordMail(RecoverPasswordRequest request);
        Task<GenericResponse<Identity>> RecoverPasswordByMail(CompleteRecoverPasswordRequest request);
        Task<GenericResponse<Identity>> UpdateUserInformation(Identity request);
        Task<GenericResponse<Identity>> CompleteAccountRegistration(CompleteRegistration request);
        Task<GenericResponse<Identity>> UpdateAccountPassword(CompleteRegistration request);
        Task<GenericResponse<int>> Pagination(int splitBy, string key, bool showSuperAdmin = false, Common.Enums.Role role = Common.Enums.Role.NONE);
    }
}
