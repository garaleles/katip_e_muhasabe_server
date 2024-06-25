using eMuhasebeServer.Application.Features.Checks.GetAllPortfolios;
using eMuhasebeServer.Application.Features.GetAllBanks;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public class ChecksController: ApiController
{
    public ChecksController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAllPortfolios(GetAllPortfoliosQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}