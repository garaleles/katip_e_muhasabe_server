using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Customers.DeleteCustomersById;

public sealed record DeleteCustomerByIdCommand(Guid Id): IRequest<Result<string>>;