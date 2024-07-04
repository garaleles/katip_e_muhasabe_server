using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CompanyCheckissuePayrolls.CreateCompanyCheckissuePayroll;

internal sealed class CompanyCheckissuePayrollHandler(
    ICompanyCheckissuePayrollRepository _companyCheckissuePayrollRepository,
    ICompanyCheckissuePayrollDetailRepository _companyCheckissuePayrollDetailRepository,
    ICompanyCheckAccountRepository _companyCheckAccountRepository,
    ICustomerRepository _customerRepository,
    ICustomerDetailRepository _customerDetailRepository,
    IUnitOfWorkCompany _unitOfWorkCompany,
    ICacheService _cacheService,
    IMapper _mapper,
    ILogger<CompanyCheckissuePayrollHandler> _logger
) : IRequestHandler<CreateCompanyCheckissuePayrollCommand, Result<string>>
{
   

    public async Task<Result<string>> Handle(CreateCompanyCheckissuePayrollCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Convert command to entity
            var companyCheckissuePayroll = _mapper.Map<CompanyCheckissuePayroll>(request);

            // Create company check issue payroll
            await _companyCheckissuePayrollRepository.AddAsync(companyCheckissuePayroll, cancellationToken);

            // Create company check issue payroll details
            foreach (var detail in request.Details)
            {
                detail.CompanyCheckissuePayrollId = companyCheckissuePayroll.Id;
                await _companyCheckissuePayrollDetailRepository.AddAsync(detail, cancellationToken);
            }

            // Get the customer
            Customer? customer = await _customerRepository.GetByExpressionAsync(p => p.Id == request.CustomerId, cancellationToken);
            if (customer is null)
            {
                return Result<string>.Failure("Cari bulunamadı");
            }

            // Increase the customer's deposit amount by the payroll amount
            customer.DepositAmount += request.PayrollAmount;
            _customerRepository.Update(customer);

            // Create a new CustomerDetail entry for the check deposit
            CustomerDetail customerDetail = new()
            {
                CustomerId = customer.Id,
                Date = request.Date,
                ProcessNumber = request.PayrollNumber,
                DepositAmount = request.PayrollAmount,
                Description = "Firma çeki ile ödeme: " + request.PayrollNumber,
                Type = CustomerDetailTypeEnum.Check,
                CompanyCheckissuePayrollId = companyCheckissuePayroll.Id
            };
            await _customerDetailRepository.AddAsync(customerDetail, cancellationToken);

            // Create company check account
            var companyCheckAccount = new CompanyCheckAccount
            {
                DueDate = request.Details.First().DueDate,
                Amount = request.PayrollAmount,
                CheckNumber = request.Details.First().CheckNumber,
                BankName = request.Details.First().BankName,
                BranchName = request.Details.First().BranchName,
                CustomerId = customer.Id,
                CompanyCheckissuePayrollId = companyCheckissuePayroll.Id
            };
            await _companyCheckAccountRepository.AddAsync(companyCheckAccount, cancellationToken);

            // Save changes
            await _unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            // Clear caches
            _cacheService.Remove("companyCheckissuePayrolls");
            _cacheService.Remove("customers");
            _cacheService.Remove("companyCheckAccounts");

            return"Firma çeki ile ödeme işlemi başarıyla oluşturuldu";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while handling the company check issue payroll creation");
            return Result<string>.Failure("An error occurred while processing the request");
        }
    }
}