using eMuhasebeServer.Application.Features.Banks.CreateBanks;
using eMuhasebeServer.Application.Features.Banks.DeleteBankById;
using eMuhasebeServer.Application.Features.Banks.UpdateBanks;
using eMuhasebeServer.Application.Features.GetAllBanks;
using eMuhasebeServer.Application.Features.Invoices.CreateInvoice;
using eMuhasebeServer.Application.Features.Invoices.DeleteInvoiceById;
using eMuhasebeServer.Application.Features.Invoices.GetAllInvoiceses;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class InvoicesController: ApiController
{
    public InvoicesController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateInvoiceCommand createInvoiceCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createInvoiceCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
   
    
    [HttpPost]
    public async Task<ActionResult> DeleteInvoiceById(DeleteInvoiceByIdCommand deleteInvoiceByIdCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(deleteInvoiceByIdCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}