using eMuhasebeServer.Application.Features.Banks.CreateBanks;
using eMuhasebeServer.Application.Features.Banks.DeleteBankById;
using eMuhasebeServer.Application.Features.Banks.UpdateBanks;
using eMuhasebeServer.Application.Features.Expenses.CreateExpenses;
using eMuhasebeServer.Application.Features.Expenses.DeleteExpenseById;
using eMuhasebeServer.Application.Features.Expenses.GetAllExpenses;
using eMuhasebeServer.Application.Features.Expenses.UpdateExpenses;
using eMuhasebeServer.Application.Features.GetAllBanks;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public class ExpenseController: ApiController
{
    public ExpenseController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllExpensesQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateExpenseCommand createExpenseCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createExpenseCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Update(UpdateExpenseCommand updateExpenseCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(updateExpenseCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> DeleteExpenseById(DeleteExpenseByIdCommand deleteExpenseByIdCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(deleteExpenseByIdCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    
    
}
