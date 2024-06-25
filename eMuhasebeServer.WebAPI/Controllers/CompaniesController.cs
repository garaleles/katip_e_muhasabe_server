using eMuhasebeServer.Application.Features.Companies.CreateCompany;
using eMuhasebeServer.Application.Features.Companies.DeleteCompanyById;
using eMuhasebeServer.Application.Features.Companies.GetAllCompanies;
using eMuhasebeServer.Application.Features.Companies.MigrateAllCompanies;
using eMuhasebeServer.Application.Features.Companies.UpdateCompany;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers
{
    public sealed class CompaniesController : ApiController
    {
        public CompaniesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> GetAll(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCompanyCommand createCompanyCommand, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(createCompanyCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<ActionResult> Update(UpdateCompanyCommand updateCompanyCommand, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(updateCompanyCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteCompanyById(DeleteCompanyByIdCommand deleteCompanyByIdCommand, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(deleteCompanyByIdCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult> MigrateAll(MigrateAllCompaniesCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
