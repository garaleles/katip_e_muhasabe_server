using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Categories.CreateCategories;

public sealed record CreateCategoryCommand(
    string Name,
    string Description
    ): IRequest<Result<string>>;