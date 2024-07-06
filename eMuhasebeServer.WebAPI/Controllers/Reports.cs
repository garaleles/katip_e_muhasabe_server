using eMuhasebeServer.Application.Features.Reports.CheckReports;
using eMuhasebeServer.Application.Features.Reports.CompanyCheckReports;
using eMuhasebeServer.Application.Features.Reports.CreditorCustomers;
using eMuhasebeServer.Application.Features.Reports.CreditorSuppliers;
using eMuhasebeServer.Application.Features.Reports.DebtorCustomers;
using eMuhasebeServer.Application.Features.Reports.DebtorSuppliers;
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
    
    [HttpGet]
    public async Task<IActionResult> ChecksInPortfolio(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new ChecksInPortfolioQuery(), cancellationToken));
    }
    
    [HttpGet]
    public async Task<IActionResult> CompanyChecks(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new CompanyCheckReportsQuery(), cancellationToken));
    }
    
    [HttpGet]
    public async Task<IActionResult> DebtorCustomers(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DebtorCustomersQuery(), cancellationToken));
    }
    
    [HttpGet]
    public async Task<IActionResult> CreditorCustomers(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new CreditorCustomersQuery(), cancellationToken));
    }
    
    [HttpGet]
    public async Task<IActionResult> CreditorSuppliers(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new CreditorSuppliersQuery(), cancellationToken));
    }
    [HttpGet]
    public async Task<IActionResult> DebtorSuppliers(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DebtorSuppliersQuery(), cancellationToken));
    }
}