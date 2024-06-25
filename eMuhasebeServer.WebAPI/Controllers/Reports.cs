using eMuhasebeServer.Application.Features.Reports.ProductProfitabilityReports;
using eMuhasebeServer.Application.Features.Reports.SalesReports;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class Reports: ApiController
{
    public Reports(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> ProductProfitabilityReports(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new ProductProfitabilityReportsQuery(), cancellationToken));
    }
    
    [HttpGet]
    public async Task<IActionResult> SalesReports(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new SalesReportsQuery(), cancellationToken));
    }
}