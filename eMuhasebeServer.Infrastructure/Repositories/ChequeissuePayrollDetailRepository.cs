using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public class ChequeissuePayrollDetailRepository : Repository<ChequeissuePayrollDetail, CompanyDbContext>, IChequeissuePayrollDetailRepository
{
    private readonly CompanyDbContext _dbContext; // DbContext'i özel olarak tanımlayın
    public ChequeissuePayrollDetailRepository(CompanyDbContext context) : base(context)
    {
        _dbContext = context; // DbContext'i başlatın
    }


}
