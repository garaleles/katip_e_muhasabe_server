using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.BankDetails.UpdateBankDetail;

public sealed record UpdateBankDetailCommand(
    Guid Id,
    Guid BankId,
    int Type,
    decimal Amount,
    string Description,
    DateOnly Date
    ): IRequest<Result<string>>;