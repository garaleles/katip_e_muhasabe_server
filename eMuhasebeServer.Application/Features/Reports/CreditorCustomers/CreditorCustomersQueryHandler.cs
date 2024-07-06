using eMuhasebeServer.Application.Features.Reports.DebtorCustomers;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Reports.CreditorCustomers;

internal sealed class CreditorCustomersQueryHandler(
    ICustomerRepository customerRepository
) : IRequestHandler<CreditorCustomersQuery, Result<List<DebtorCustomersQueryResponse>>>
{
    public async Task<Result<List<DebtorCustomersQueryResponse>>> Handle(CreditorCustomersQuery request, CancellationToken cancellationToken)
    {
        var customersWithNegativeBalance = await customerRepository.GetAll()
            .Select(c => new
            {
                c.Name,
                c.Type,
                DepositAmount = c.DepositAmount,
                WithdrawalAmount = c.WithdrawalAmount
            })
            .Where(c => c.DepositAmount - c.WithdrawalAmount < 0)
            .OrderBy(c => c.Name)
            .Select(c => new DebtorCustomersQueryResponse
            {
                Name = c.Name,
                Type = c.Type,
                DepositAmount = c.DepositAmount,
                WithdrawalAmount = c.WithdrawalAmount
            })
            .ToListAsync(cancellationToken);

        return Result<List<DebtorCustomersQueryResponse>>.Succeed(customersWithNegativeBalance);
    }
}