using eMuhasebeServer.Domain.Events.ValueObjects;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Companies.UpdateCompany
{
    public sealed record UpdateCompanyCommand(
        Guid Id,
        string Name,
        string FullAddress,
        string TaxNumber,
        string TaxOffice,
        Database Database
        ) : IRequest<Result<string>>;

}
