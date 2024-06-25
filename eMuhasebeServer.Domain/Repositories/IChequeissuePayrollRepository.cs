using eMuhasebeServer.Domain.Entities;
using GenericRepository;

namespace eMuhasebeServer.Domain.Repositories;

public interface IChequeissuePayrollRepository : IRepository<ChequeissuePayroll>
{
    IQueryable<Check> Query();
}
