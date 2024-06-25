using eMuhasebeServer.Application.Features.BankDetails.CreateBankDetail;
using eMuhasebeServer.Application.Features.BankDetails.DeleteBankDetailById;
using eMuhasebeServer.Application.Features.BankDetails.GetAllBankDetails;
using eMuhasebeServer.Application.Features.BankDetails.UpdateBankDetail;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public class BankDetailsController: ApiController
{
    public BankDetailsController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllBankDetailsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateBankDetailCommand createBankDetailCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createBankDetailCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<ActionResult> Update(UpdateBankDetailCommand updateBankDetailCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(updateBankDetailCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<ActionResult> DeleteBankDetailById(DeleteBankDetailByIdCommand deleteBankDetailByIdCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(deleteBankDetailByIdCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    
}