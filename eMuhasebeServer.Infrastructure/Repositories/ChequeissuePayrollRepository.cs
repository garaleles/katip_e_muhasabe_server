using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public class ChequeissuePayrollRepository : Repository<ChequeissuePayroll, CompanyDbContext>, IChequeissuePayrollRepository
{
    private readonly CompanyDbContext _dbContext; // DbContext'i özel olarak tanımlayın
    public ChequeissuePayrollRepository(CompanyDbContext context) : base(context)
    {
        _dbContext = context; // DbContext'i başlatın
    }

    public IQueryable<Check> Query()
    {
        return _dbContext.Checks; // DbSet'e doğrudan erişim
    }
}
