using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ChequeissuePayrolls.CreateChequeissuePayroll;

public record CreateChequeissuePayrollCommand(
    DateOnly Date,
    string PayrollNumber,
    Guid? CustomerId,
    decimal PayrollAmount,
    string? Description,
    int CheckCount,
    int Status,
    Guid bankId,
    Guid cashRegisterId,
    DateOnly AverageMaturityDate,
     List<ChequeissuePayrollDetail> Details
    ) : IRequest<Result<string>>;
