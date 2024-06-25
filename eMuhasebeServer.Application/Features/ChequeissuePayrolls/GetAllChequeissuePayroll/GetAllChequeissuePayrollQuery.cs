using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ChequeissuePayrolls.GetAllChequeissuePayroll;

public record GetAllChequeissuePayrollQuery(): IRequest<Result<List<ChequeissuePayroll>>>;