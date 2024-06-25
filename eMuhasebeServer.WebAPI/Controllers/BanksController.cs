using eMuhasebeServer.Application.Features.Banks.CreateBanks;
using eMuhasebeServer.Application.Features.Banks.DeleteBankById;
using eMuhasebeServer.Application.Features.Banks.UpdateBanks;
using eMuhasebeServer.Application.Features.GetAllBanks;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public class BanksController: ApiController
{
    public BanksController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllBanksQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateBankCommand createBankCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createBankCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Update(UpdateBankCommand updateBankCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(updateBankCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> DeleteBankById(DeleteBankByIdCommand deleteBankCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(deleteBankCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    
    
}

