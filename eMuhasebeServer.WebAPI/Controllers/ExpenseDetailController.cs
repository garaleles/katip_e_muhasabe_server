using eMuhasebeServer.Application.Features.ExpenseDetails.CreateExpenseDetails;
using eMuhasebeServer.Application.Features.ExpenseDetails.DeleteExpenseDetailById;
using eMuhasebeServer.Application.Features.ExpenseDetails.GetAllExpenseDetails;
using eMuhasebeServer.Application.Features.ExpenseDetails.UpdateExpenses;
using eMuhasebeServer.Application.Features.Expenses.CreateExpenses;
using eMuhasebeServer.Application.Features.Expenses.DeleteExpenseById;
using eMuhasebeServer.Application.Features.Expenses.UpdateExpenses;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public class ExpenseDetailController: ApiController
{
    public ExpenseDetailController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllExpenseDetailsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateExpenseDetailCommand createExpenseCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createExpenseCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Update(UpdateExpenseDetailCommand updateExpenseCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(updateExpenseCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> DeleteExpenseDetailById(DeleteExpenseDetailByIdCommand deleteExpenseByIdCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(deleteExpenseByIdCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}