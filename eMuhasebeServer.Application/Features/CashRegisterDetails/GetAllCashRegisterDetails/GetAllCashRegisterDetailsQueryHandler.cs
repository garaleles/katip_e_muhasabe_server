using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CashRegisterDetails.GetAllCashRegisterDetails;

internal sealed class GetAllCashRegisterDetailsQueryHandler(
    ICashRegisterRepository cashRegisterRepository) : IRequestHandler<GetAllCashRegisterDetailsQuery, Result<CashRegister>>
{
    public async Task<Result<CashRegister>> Handle(GetAllCashRegisterDetailsQuery request, CancellationToken cancellationToken)
    {
        var query = cashRegisterRepository.Where(p => p.Id == request.CashRegisterId);

        // Tarih filtrelemesi (daha sağlam)
        if (!request.StartDate.Equals(default(DateOnly)) && !request.EndDate.Equals(default(DateOnly)))
        {
            query = query.Include(p => p.Details!
                .OrderBy(d => d.Date)
                .Where(d => d.Date >= request.StartDate && d.Date <= request.EndDate));
        }
        else
        {
            query = query.Include(p => p.Details!.OrderBy(d => d.Date)); // Artan tarihe göre sıralama
        }

        CashRegister? cashRegister = await query.FirstOrDefaultAsync(cancellationToken);

        if (cashRegister is null)
        {
            return Result<CashRegister>.Failure("Kasa hareketi bulunamadı");
        }

        return cashRegister;
    }
}