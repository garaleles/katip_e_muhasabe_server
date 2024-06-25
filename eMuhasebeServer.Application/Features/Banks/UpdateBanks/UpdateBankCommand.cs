using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Banks.UpdateBanks;

public sealed record UpdateBankCommand(
    Guid Id,
    string Name,
    string IBAN,
    int CurrencyTypeValue
    ) : IRequest<Result<string>>;