using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Invoices.GetAllInvoiceses;

public sealed record GetAllInvoicesQuery() : IRequest<Result<List<Invoice>>>;