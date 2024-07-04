using eMuhasebeServer.Application.Features.ChequeissuePayrolls.CreateChequeissuePayroll;
using eMuhasebeServer.Application.Features.ChequeissuePayrolls.DeleteChequeissuePayrollById;
using eMuhasebeServer.Application.Features.ChequeissuePayrolls.GetAllChequeissuePayroll;
using eMuhasebeServer.Application.Features.CompanyCheckissuePayrolls.CreateCompanyCheckissuePayroll;
using eMuhasebeServer.Application.Features.CompanyCheckissuePayrolls.DeleteCompanyCheckissuePayrollById;
using eMuhasebeServer.Application.Features.CompanyCheckissuePayrolls.GetAllCompanyCheckissuePayroll;
using eMuhasebeServer.Application.Features.CompanyCheckissuePayrolls.GetByIdCompanyCheckissuePayroll;
using eMuhasebeServer.Application.Features.CompanyCheckissuePayrolls.UpdateCompanyCheckissuePayroll;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public class CompanyCheckissuePayrollsController: ApiController
{
    public CompanyCheckissuePayrollsController(IMediator mediator) : base(mediator)
    {
    }
    
     
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllCompanyCheckissuePayrollQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateCompanyCheckissuePayrollCommand createCompanyCheckissuePayrollCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createCompanyCheckissuePayrollCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Update(UpdateCompanyCheckissuePayrollCommand createCompanyCheckissuePayrollCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createCompanyCheckissuePayrollCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
   
    
    [HttpPost]
    public async Task<ActionResult> DeleteCompanyCheckissuePayrollById(DeleteCompanyCheckissuePayrollByIdCommand deleteCompanyCheckissuePayrollByIdCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(deleteCompanyCheckissuePayrollByIdCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetById(GetByIdCompanyCheckissuePayrollQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}