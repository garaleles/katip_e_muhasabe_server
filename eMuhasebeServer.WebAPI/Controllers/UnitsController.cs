using eMuhasebeServer.Application.Features.Units.CreateUnits;
using eMuhasebeServer.Application.Features.Units.DeleteUnitById;
using eMuhasebeServer.Application.Features.Units.GetAllUnits;
using eMuhasebeServer.Application.Features.Units.UpdateUnits;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class UnitsController: ApiController
{
    public UnitsController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllUnitsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateUnitCommand createUnitCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createUnitCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Update(UpdateUnitCommand updateUnitCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(updateUnitCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> DeleteUnitById(DeleteUnitByIdCommand deleteUnitCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(deleteUnitCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}