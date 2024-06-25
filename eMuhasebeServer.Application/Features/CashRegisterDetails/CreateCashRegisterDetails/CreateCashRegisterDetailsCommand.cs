using MediatR;
using System;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CashRegisterDetails.CreateCashRegisterDetails;

public sealed record CreateCashRegisterDetailCommand(
    Guid CashRegisterId,
    string ProcessNumber,
    DateOnly Date,
    int Type,
    decimal Amount,
    Guid? OppositeCashRegisterId,
    Guid? OppositeBankId,
    Guid? OppositeCustomerId,
    decimal OppositeAmount,
    string Description
) : IRequest<Result<string>>;