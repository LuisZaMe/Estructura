using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Repositories
{
    public interface ICompanyRepository
    {
        Task<GenericResponse<CompanyInformation>> AddCompanyInformation(CompanyInformation request);
        Task<GenericResponse<List<CompanyInformation>>> GetCompanyInformation(List<long>Id, int currentPage, int offset);
        Task<GenericResponse<List<CompanyInformation>>> GetCompanyInformation(List<long> userId);
        Task<GenericResponse<CompanyInformation>> UpdateComanyInformation(CompanyInformation request);
        //Task<GenericResponse<CompanyInformation>> DeleteComanyInformation(int Id);

    }
}
