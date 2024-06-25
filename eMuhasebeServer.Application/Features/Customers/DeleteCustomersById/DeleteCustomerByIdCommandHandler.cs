using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Customers.DeleteCustomersById;

internal sealed class DeleteCustomerByIdCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
) : IRequestHandler<DeleteCustomerByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
    {
        Customer? customer= await customerRepository.GetByExpressionWithTrackingAsync(x=>x.Id==request.Id,cancellationToken);
            
        if(customer==null)
            return Result<string>.Failure("Müşteri bulunamadı");
        
        customer.IsDeleted=true;
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("customers");
        
        return "Müşteri başarıyla silindi";
    }
}