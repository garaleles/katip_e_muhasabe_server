using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;


namespace eMuhasebeServer.Application.Features.CheckRegisterPayrolls.GetAllCheckRegisterPayroll;

public sealed record GetAllCheckRegisterPayrollQuery() : IRequest<Result<List<CheckRegisterPayroll>>>;