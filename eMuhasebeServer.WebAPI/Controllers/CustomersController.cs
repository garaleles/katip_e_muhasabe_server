using eMuhasebeServer.Application.Features.Customers.CreateCustomer;
using eMuhasebeServer.Application.Features.Customers.DeleteCustomersById;
using eMuhasebeServer.Application.Features.Customers.GetAllCustomers;
using eMuhasebeServer.Application.Features.Customers.UpdateCustomer;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class CustomersController: ApiController
{
    public CustomersController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateCustomerCommand createCustomerCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createCustomerCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Update(UpdateCustomerCommand updateCustomerCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(updateCustomerCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> DeleteCustomerById(DeleteCustomerByIdCommand deleteCustomerByIdCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(deleteCustomerByIdCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}