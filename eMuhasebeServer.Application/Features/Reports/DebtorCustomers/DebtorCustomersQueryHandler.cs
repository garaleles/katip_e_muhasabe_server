using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Reports.DebtorCustomers;

internal sealed class DebtorCustomersQueryHandler(
    ICustomerRepository customerRepository
) : IRequestHandler<DebtorCustomersQuery, Result<List<DebtorCustomersQueryResponse>>>
{
    public async Task<Result<List<DebtorCustomersQueryResponse>>> Handle(DebtorCustomersQuery request, CancellationToken cancellationToken)
    {
        var customersWithPositiveBalance = await customerRepository.GetAll()
            .Select(c => new
            {
                c.Name,
                c.Type,
                DepositAmount = c.DepositAmount,
                WithdrawalAmount = c.WithdrawalAmount
            })
            .Where(c => c.DepositAmount - c.WithdrawalAmount > 0)
            .OrderBy(c => c.Name)
            .Select(c => new DebtorCustomersQueryResponse
            {
                Name = c.Name,
                Type = c.Type,
                DepositAmount = c.DepositAmount,
                WithdrawalAmount = c.WithdrawalAmount
            })
            .ToListAsync(cancellationToken);

        return Result<List<DebtorCustomersQueryResponse>>.Succeed(customersWithPositiveBalance);
    }
}