using eMuhasebeServer.Application.Exceptions;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class CheckDetailRepository : Repository<CheckDetail, CompanyDbContext>, ICheckDetailRepository
{
    private readonly CompanyDbContext _dbContext;
    public CheckDetailRepository(CompanyDbContext context) : base(context)
    {
        _dbContext = context; // DbContext'i başlatın
    }

    public EntityEntry<CheckDetail> Entry(CheckDetail entity)
    {
        return _dbContext.Entry(entity);
    }

    public CheckDetail FirstOrDefault(Expression<Func<CheckDetail, bool>> predicate)
    {
        var checkDetail = _dbContext.CheckDetails.FirstOrDefault(predicate);

        if (checkDetail == null)
        {


            throw new ChequeissuePayrollDetailNotFoundException("Belirtilen koşula uygun ChequeissuePayrollDetail kaydı bulunamadı.");
        }

        return checkDetail;
    }

    public async Task<CheckDetail> FirstOrDefaultAsync(Expression<Func<CheckDetail, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var checkDetail = await _dbContext.CheckDetails.FirstOrDefaultAsync(predicate, cancellationToken);
        if (checkDetail == null)
        {
            throw new ArgumentException("CheckDetail parametresi null olamaz.", nameof(predicate));
        }

        return checkDetail;


    }

    public async Task<CheckDetail> GetByIdAsync(Guid checkDetailId, CancellationToken cancellationToken)
    {
        var checkDetail = await _dbContext.CheckDetails.FindAsync(new object[] { checkDetailId }, cancellationToken);

        if (checkDetail == null)
        {
            throw new DbUpdateConcurrencyException($"CheckDetail with ID {checkDetailId} not found."); // Entity Framework'ün kendi exception sınıfını kullanın
        }

        return checkDetail;
    }


}
