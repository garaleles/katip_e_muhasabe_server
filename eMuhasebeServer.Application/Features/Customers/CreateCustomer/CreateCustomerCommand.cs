using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(
    string Name,
    string District,
    string City,
    string FullAddress,
    string Phone,
    string TaxOffice,
    string TaxNumber,
    int TypeValue
    ): IRequest<Result<string>>;