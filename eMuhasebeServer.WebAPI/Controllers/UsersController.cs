using eMuhasebeServer.Application.Features.Users.CreateUser;
using eMuhasebeServer.Application.Features.Users.DeleteUserById;
using eMuhasebeServer.Application.Features.Users.GetAllUsers;
using eMuhasebeServer.Application.Features.Users.UpdateUser;
using eMuhasebeServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers
{
    public sealed class UsersController : ApiController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> GetAll(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(createUserCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(updateUserCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUserById(DeleteUserByIdCommand deleteUserByIdCommand, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(deleteUserByIdCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
