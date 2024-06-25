using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class CheckRepository : Repository<Check, CompanyDbContext>, ICheckRepository
{
    private readonly CompanyDbContext _dbContext; // DbContext'i özel olarak tanımlayın

    public CheckRepository(CompanyDbContext context) : base(context)
    {
        _dbContext = context; // DbContext'i başlatın
    }

    public async Task<Check> GetByIdAsync(Guid checkId, CancellationToken cancellationToken, params Expression<Func<Check, object>>[] includes)
    {
        var query = _dbContext.Checks.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        var check = await query.FirstOrDefaultAsync(c => c.Id == checkId, cancellationToken);

        if (check == null)
        {
            throw new InvalidOperationException($"Check with ID {checkId} not found.");
        }

        return check;
    }

    public async Task<Check> GetByIdAsync(Guid checkId, CancellationToken cancellationToken)
    {
        var check = await _dbContext.Checks.FindAsync(new object[] { checkId }, cancellationToken);

        if (check == null)
        {
            throw new InvalidOperationException($"Check with ID {checkId} not found.");
        }

        return check;
    }

    public IQueryable<Check> Query()
    {
        return _dbContext.Checks; // DbSet'e doğrudan erişim
    }

    public void UpdateCheckAndDetail(Check check, CheckDetail checkDetail)
    {
        // Entity'leri yeniden yükle
        _dbContext.Entry(check).Reload();
        _dbContext.Entry(checkDetail).Reload();

        // Değişiklikleri uygula
        check.ChequeissuePayrollId = null;
        checkDetail.Status = CheckStatus.InPortfolio;

        _dbContext.SaveChanges(); // Veya _unitOfWorkCompany.SaveChanges()
    }

}
