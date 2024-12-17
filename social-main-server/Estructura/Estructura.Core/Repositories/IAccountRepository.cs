using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Repositories
{
    public interface IAccountRepository 
    {
        GenericResponse<AppUser> GetCurrentUser();
        GenericResponse<Core.ConfigurationReflection.Environment> GetEnvironment();
        Task<GenericResponse<Identity>> CreateAccount(Identity request);
        Task<GenericResponse<Identity>> DeleteUserAccount(long UserID, bool hardDelete = false);
        Task<GenericResponse<List<Identity>>> GetPendingApprovalUsers(int currentPage, int offse);
        Task<GenericResponse<Identity>> ApproveAccount(Identity user);
        Task<GenericResponse<Identity>> UnapproveAccount(Identity user);
        Task<GenericResponse<Identity>> UpdatePassword(SilentData request, string password);
        Task<GenericResponse<bool>> VerifyEmailExists(string email);
        Task<GenericResponse<List<Identity>>> GetActiveUsers(int currentPage, int offset);
        Task<GenericResponse<List<Identity>>> GetUsersById(List<long> users, bool applySuperadminFilter = true);
        Task<GenericResponse<List<Identity>>> SearchUsers(string key, bool showSuperAdmin = false, Common.Enums.Role role = Common.Enums.Role.NONE, int currentPage = 0, int offset = 10);
        Task<GenericResponse<List<Identity>>> GetUsersByRole(Common.Enums.Role Id, int currentPage = 0, int offset = 10);
        Task<GenericResponse<List<Identity>>> GetUsersByEmail(string email, int currentPage, int offset);
        Task<GenericResponse<Identity>> UpdateUserInformation(Identity request);
        Task<GenericResponse<int>> Pagination(int splitBy, string key, bool showSuperAdmin = false, Common.Enums.Role role = Common.Enums.Role.NONE);
        Task<GenericResponse<UserIdentitySearchPermission>> GetAdminForCurrentUser();
    }
}
