using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeServer.Application.Features.Reports.CreditorSuppliers;

internal sealed class CreditorSuppliersQueryResponseHandler(
    ICustomerRepository customerRepository
) : IRequestHandler<CreditorSuppliersQuery, IEnumerable<CreditorSuppliersQueryResponse>>
{
    public async Task<IEnumerable<CreditorSuppliersQueryResponse>> Handle(CreditorSuppliersQuery request, CancellationToken cancellationToken)
    {
        var suppliersWithNegativeBalance = await customerRepository.GetAll()
            .Where(c => c.Type == CustomerTypeEnum.Saticilar && (c.DepositAmount - c.WithdrawalAmount) < 0)
            .Select(c => new CreditorSuppliersQueryResponse
            {
                Name = c.Name,
                Type = c.Type,
                DepositAmount = c.DepositAmount,
                WithdrawalAmount = c.WithdrawalAmount
            })
            .ToListAsync(cancellationToken);

        return suppliersWithNegativeBalance;
    }
}