using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Banks.CreateBanks;

public sealed record CreateBankCommand(
    string Name,
    string IBAN,
    int CurrencyTypeValue
    ) : IRequest<Result<string>>;