using AutoMapper;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.BankDetails.CreateBankDetail;

public record CreateBankDetailCommand(
    Guid BankId,
    string ProcessNumber,
    DateOnly Date,
    int Type,
    decimal Amount,
    Guid? OppositeBankId,
    Guid? OppositeCashRegisterId,
    Guid? OppositeCustomerId,
    decimal OppositeAmount,
    string Description
    ): IRequest<Result<string>>;