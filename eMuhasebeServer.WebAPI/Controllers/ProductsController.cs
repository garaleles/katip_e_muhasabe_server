using eMuhasebeServer.Application.Features.Banks.CreateBanks;
using eMuhasebeServer.Application.Features.Banks.DeleteBankById;
using eMuhasebeServer.Application.Features.Banks.UpdateBanks;
using eMuhasebeServer.Application.Features.GetAllBanks;
using eMuhasebeServer.Application.Features.Products.CreateProduct;
using eMuhasebeServer.Application.Features.Products.DeleteProductById;
using eMuhasebeServer.Application.Features.Products.GetAllProducts;
using eMuhasebeServer.Application.Features.Products.UpdateProduct;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

public sealed class ProductsController: ApiController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateProductCommand createProductCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createProductCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Update(UpdateProductCommand updateProductCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(updateProductCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> DeleteProductById(DeleteProductByIdCommand deleteProductCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(deleteProductCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}