using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ChequeissuePayrolls.DeleteChequeissuePayrollById;

public record DeleteChequeissuePayrollByIdCommand(Guid Id): IRequest<Result<string>>;