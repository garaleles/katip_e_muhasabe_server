using eMuhasebeServer.Application.Features.CompanyCheckissuePayrolls.CreateCompanyCheckissuePayroll;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CompanyCheckissuePayrolls.DeleteCompanyCheckissuePayrollById;

internal sealed class
    DeleteCompanyCheckissuePayrollByIdCommandHandler(
        ICompanyCheckissuePayrollRepository _companyCheckissuePayrollRepository,
        ICompanyCheckAccountRepository _companyCheckAccountRepository,
        ICustomerRepository _customerRepository,
        ICustomerDetailRepository _customerDetailRepository,
        IUnitOfWorkCompany _unitOfWorkCompany,
        ICacheService _cacheService,
        ILogger<CreateCompanyCheckissuePayrollCommandHandler> _logger
    ) : IRequestHandler<DeleteCompanyCheckissuePayrollByIdCommand,
    Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCompanyCheckissuePayrollByIdCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWorkCompany.BeginTransactionAsync();

        try
        {
            // Retrieve companyCheckissuePayroll, customer
            var companyCheckissuePayroll = await _companyCheckissuePayrollRepository
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (companyCheckissuePayroll == null)
            {
                _logger.LogWarning("Company check issue payroll not found, ID: {Id}", request.Id);
                return Result<string>.Failure("Bordro bulunamadı");
            }

            var customer = await _customerRepository.GetByExpressionAsync(
                p => p.Id == companyCheckissuePayroll.CustomerId,
                cancellationToken
            );
            _logger.LogInformation("Customer retrieved: {Customer}", customer?.ToString() ?? "null");

            if (companyCheckissuePayroll.CustomerId == null)
            {
                _logger.LogError("CustomerId cannot be null.");
                return Result<string>.Failure("Cari boş olamaz.");
            }

            // Load all related companyCheckAccounts ONCE
            var companyCheckAccounts = await _companyCheckAccountRepository.GetAll().Where(c => c.CompanyCheckissuePayrollId == companyCheckissuePayroll.Id).AsNoTracking().ToListAsync(cancellationToken);

            if (customer != null)
            {
                customer.DepositAmount -= companyCheckissuePayroll.PayrollAmount;
                _customerRepository.Update(customer);
            }

            var customerDetails = await _customerDetailRepository
                .GetAll()
                .Where(p => p.CompanyCheckissuePayrollId == companyCheckissuePayroll.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            _customerDetailRepository.DeleteRange(customerDetails);

            if (companyCheckAccounts != null)
            {
                foreach (var companyCheckAccount in companyCheckAccounts)
                {
                    _companyCheckAccountRepository.Delete(companyCheckAccount);
                }

                await _unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }

       
            _companyCheckissuePayrollRepository.Delete(companyCheckissuePayroll);

            await _unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("CompanyCheckissuePayroll with id: {Id} deleted successfully.", request.Id);

            _cacheService.Remove("companyCheckissuePayrolls");
            _cacheService.Remove("customers");
            _cacheService.Remove("companyCheckAccounts");

            await transaction.CommitAsync();
            _logger.LogInformation("Transaction committed successfully.");
            return Result<string>.Succeed("Bordro başarıyla silindi.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "An error occurred while deleting the company check issue payroll.");
            return Result<string>.Failure($"Bir hata oluştu: {ex.Message}");
        }
    }
}