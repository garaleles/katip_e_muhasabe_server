using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.BankDetails.GetAllBankDetails;

sealed class GetAllBankDetailsQueryHandler(
    IBankRepository bankRepository
) : IRequestHandler<GetAllBankDetailsQuery, Result<Bank>>
{
    public async Task<Result<Bank>> Handle(GetAllBankDetailsQuery request, CancellationToken cancellationToken)
    {
        var query = bankRepository.Where(p => p.Id == request.BankId);

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

        Bank? bank = await query.FirstOrDefaultAsync(cancellationToken);

        if (bank is null)
        {
            return Result<Bank>.Failure("Banka hareketi bulunamadı");
        }

        return bank;
    }



}