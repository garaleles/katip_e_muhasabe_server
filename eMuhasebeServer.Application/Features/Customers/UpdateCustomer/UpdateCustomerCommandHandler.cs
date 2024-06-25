using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Customers.UpdateCustomer;

internal sealed class UpdateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IMapper mapper,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
        
) : IRequestHandler<UpdateCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer? customer= await customerRepository.GetByExpressionWithTrackingAsync(x=>x.Id==request.Id,cancellationToken);
            
        if(customer==null)
            return Result<string>.Failure("Müşteri bulunamadı");
            
        mapper.Map(request,customer);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("customers");

        return "Müşteri başarıyla güncellendi";

    }
}