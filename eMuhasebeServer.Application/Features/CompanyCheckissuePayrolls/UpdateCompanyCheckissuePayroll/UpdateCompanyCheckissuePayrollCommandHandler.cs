using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CompanyCheckissuePayrolls.UpdateCompanyCheckissuePayroll;

internal sealed class UpdateCompanyCheckissuePayrollCommandHandler(
    ICompanyCheckissuePayrollRepository _companyCheckissuePayrollRepository,
    ICompanyCheckAccountRepository _companyCheckAccountRepository,
    ICustomerRepository _customerRepository,
    ICustomerDetailRepository _customerDetailRepository,
    IUnitOfWorkCompany _unitOfWorkCompany,
    ICacheService _cacheService,
    IMapper _mapper,
    ILogger<UpdateCompanyCheckissuePayrollCommandHandler> _logger
) : IRequestHandler<UpdateCompanyCheckissuePayrollCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCompanyCheckissuePayrollCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Mevcut companyCheckissuePayroll'u getir
            var companyCheckissuePayroll = await _companyCheckissuePayrollRepository.GetByExpressionAsync(
                p => p.Id == request.Id, cancellationToken);
            if (companyCheckissuePayroll == null)
            {
                return Result<string>.Failure("Bordro kaydı bulunamadı.");
            }

            // Mevcut müşteri ve mevduat miktarını al
            var customer = await _customerRepository.GetByExpressionAsync(
                p => p.Id == request.CustomerId, cancellationToken);
            if (customer == null)
            {
                return Result<string>.Failure("Cari bulunamadı.");
            }

            // Mevcut çek hesaplarını getir
            var existingCheckAccounts = await _companyCheckAccountRepository.GetCheckAccountsByPayrollIdAsync(request.Id);

            // Mevcut çek hesaplarını temizle
            _companyCheckAccountRepository.DeleteRange(existingCheckAccounts);
            await _unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            // Yeni çek hesaplarını oluştur ve ilişkilendir
            companyCheckissuePayroll.CheckAccounts = _mapper.Map<ICollection<CompanyCheckAccount>>(request.CheckAccounts);
            foreach (var checkAccount in companyCheckissuePayroll.CheckAccounts)
            {
                if (string.IsNullOrEmpty(checkAccount.AccountNumber)) 
                {
                    _logger.LogError("AccountNumber is null or empty for checkAccount: {@checkAccount}", checkAccount);
                    return Result<string>.Failure("Çek hesap numarası boş olamaz.");
                }
                checkAccount.CompanyCheckissuePayrollId = companyCheckissuePayroll.Id;
                await _companyCheckAccountRepository.AddAsync(checkAccount, cancellationToken);
            }

            // Bordro güncellemesi
            companyCheckissuePayroll.PayrollAmount = companyCheckissuePayroll.CheckAccounts.Sum(ca => ca.Amount);

            // Müşterinin mevduat miktarını bordro tutarı kadar artır
            customer.DepositAmount += request.PayrollAmount;
            _customerRepository.Update(customer);

            // Çek mevduatı için yeni bir CustomerDetail kaydı oluştur
            var customerDetail = new CustomerDetail
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

            // Değişiklikleri kaydet
            await _unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            // Önbelleği temizle
            _cacheService.Remove("companyCheckissuePayrolls");
            _cacheService.Remove("customers");
            _cacheService.Remove("companyCheckAccounts");

            return Result<string>.Succeed("Firma çeki ile ödeme işlemi başarıyla güncellendi");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "Concurrency conflict occurred while handling the company check issue payroll update");
            return Result<string>.Failure("Veritabanında eşzamanlılık hatası oluştu. Lütfen tekrar deneyiniz.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while handling the company check issue payroll update");
            return Result<string>.Failure("İstek işlenirken bir hata oluştu");
        }
    }
}