using eMuhasebeServer.Application.Features.Categories.CreateCategories;
using eMuhasebeServer.Application.Features.Categories.DeleteCategoryById;
using eMuhasebeServer.Application.Features.Categories.GetAllCategories;
using eMuhasebeServer.Application.Features.Categories.UpdateCategories;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class CategoriesController: ApiController
{
    public CategoriesController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateCategoryCommand createCategoryCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createCategoryCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Update(UpdateCategoryCommand updateCategoryCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(updateCategoryCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> DeleteCategoryById(DeleteCategoryByIdCommand deleteCategoryCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(deleteCategoryCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}