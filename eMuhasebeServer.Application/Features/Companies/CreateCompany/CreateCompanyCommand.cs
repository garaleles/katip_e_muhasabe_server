using eMuhasebeServer.Domain.Events.ValueObjects;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Companies.CreateCompany
{
    public sealed record CreateCompanyCommand(
        string Name,
        string FullAddress,
        string TaxOffice,
        string TaxNumber,
        Database Database
        ) : IRequest<Result<string>>;



}
