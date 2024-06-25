using eMuhasebeServer.Domain.Entities;

namespace eMuhasebeServer.Application.Features.Companies.MigrateAllCompanies;

public interface ICompanyService
{
    void MigrateAll(List<Company> companies);
}