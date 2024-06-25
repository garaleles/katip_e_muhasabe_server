using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CheckRegisterPayrolls.CreateCheckRegisterPayroll;

public sealed record CreateCheckRegisterPayrollCommand(
    DateOnly Date,
    string PayrollNumber,
    Guid? CustomerId,
    decimal PayrollAmount,
    string? Description,
    int CheckCount,
    DateOnly AverageMaturityDate,
    List<CheckRegisterPayrollDetail> Details
) : IRequest<Result<string>>;
