﻿using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.CreateUser
{
    public sealed record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string UserName,
        string Password,
        bool IsAdmin,
        List<Guid> CompanyIds) : IRequest<Result<string>>;

}
