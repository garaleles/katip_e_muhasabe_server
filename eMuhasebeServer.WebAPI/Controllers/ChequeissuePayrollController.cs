using eMuhasebeServer.Application.Features.CheckRegisterPayrolls.CreateCheckRegisterPayroll;
using eMuhasebeServer.Application.Features.CheckRegisterPayrolls.DeleteChecRegisterPayrollById;
using eMuhasebeServer.Application.Features.CheckRegisterPayrolls.GetAllCheckRegisterPayroll;
using eMuhasebeServer.Application.Features.ChequeissuePayrolls.CreateChequeissuePayroll;
using eMuhasebeServer.Application.Features.ChequeissuePayrolls.DeleteChequeissuePayrollById;
using eMuhasebeServer.Application.Features.ChequeissuePayrolls.GetAllChequeissuePayroll;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class ChequeissuePayrollController: ApiController
{
    public ChequeissuePayrollController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllChequeissuePayrollQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateChequeissuePayrollCommand createChequeissuePayrollCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createChequeissuePayrollCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
   
    
    [HttpPost]
    public async Task<ActionResult> DeleteChequeissuePayrollById(DeleteChequeissuePayrollByIdCommand deleteChequeissuePayrollByIdCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(deleteChequeissuePayrollByIdCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}