using eMuhasebeServer.Application.Features.DashboardQuerys.GetAllSales;
using eMuhasebeServer.Application.Features.DashBoardsCustomers.DashboardBanks;
using eMuhasebeServer.Application.Features.DashBoardsCustomers.DashboardCustomersWithDrawalAll;
using eMuhasebeServer.Application.Features.DashBoardsCustomers.DashboardSafes;
using eMuhasebeServer.Application.Features.DashBoardsCustomers.DashBoardsCustomersAll;
using eMuhasebeServer.Application.Features.DashBoardsCustomers.TotalProducts;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class DashBoardsCustomersController: ApiController
{
    public DashBoardsCustomersController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAllDebts(DashBoardsCustomersAllQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAllWhits(DashboardCustomersWithDrawalAllQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAllStocks(TotalProductsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAllSafes(DashboardSafesQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAllBanks(DashboardBanksQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
}