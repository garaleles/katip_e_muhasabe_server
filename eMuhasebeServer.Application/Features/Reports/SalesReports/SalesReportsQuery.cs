using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Reports.SalesReports;

public sealed record SalesReportsQuery(): IRequest<Result<object>>;