using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CheckRegisterPayrolls.DeleteChecRegisterPayrollById;

public sealed record DeleteChecRegisterPayrollByIdCommand(Guid Id): IRequest<Result<string>>;