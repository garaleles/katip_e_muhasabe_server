using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Categories.GetAllCategories;

public sealed record GetAllCategoriesQuery(): IRequest<Result<List<Category>>>;