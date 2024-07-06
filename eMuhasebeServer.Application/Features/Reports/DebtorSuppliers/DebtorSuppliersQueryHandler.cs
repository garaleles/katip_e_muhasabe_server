using eMuhasebeServer.Application.Features.Reports.CreditorSuppliers;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeServer.Application.Features.Reports.DebtorSuppliers;

internal sealed class DebtorSuppliersQueryHandler : IRequestHandler<DebtorSuppliersQuery, List<CreditorSuppliersQueryResponse>>
{
    private readonly ICustomerRepository _customerRepository;

    public DebtorSuppliersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<List<CreditorSuppliersQueryResponse>> Handle(DebtorSuppliersQuery request, CancellationToken cancellationToken)
    {
        var suppliersWithPositiveBalance = await _customerRepository.GetAll()
            .Where(c => c.Type == CustomerTypeEnum.Saticilar && (c.DepositAmount - c.WithdrawalAmount) > 0)
            .Select(c => new CreditorSuppliersQueryResponse
            {
                Name = c.Name,
                Type = c.Type,
                DepositAmount = c.DepositAmount,
                WithdrawalAmount = c.WithdrawalAmount
            })
            .ToListAsync(cancellationToken);

        return suppliersWithPositiveBalance;
    }
}