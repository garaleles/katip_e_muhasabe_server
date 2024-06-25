using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Categories.UpdateCategories;

public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string Description
    ): IRequest<Result<string>>;