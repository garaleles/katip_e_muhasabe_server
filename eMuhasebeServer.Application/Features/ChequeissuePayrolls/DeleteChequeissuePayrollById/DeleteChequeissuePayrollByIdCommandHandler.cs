using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ChequeissuePayrolls.DeleteChequeissuePayrollById
{
    internal sealed class DeleteChequeissuePayrollByIdCommandHandler
        : IRequestHandler<DeleteChequeissuePayrollByIdCommand, Result<string>>
    {
        private readonly IChequeissuePayrollRepository _chequeissuePayrollRepository;
        private readonly IChequeissuePayrollDetailRepository _chequeissuePayrollDetailRepository;
        private readonly IUnitOfWorkCompany _unitOfWorkCompany;
        private readonly ICacheService _cacheService;
        private readonly ICheckDetailRepository _chequeDetailRepository;
        private readonly ICheckRepository _chequeRepository;
        private readonly ICustomerDetailRepository _customerDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBankDetailRepository _bankDetailRepository;
        private readonly ICashRegisterDetailRepository _cashRegisterDetailRepository;
        private readonly ILogger<DeleteChequeissuePayrollByIdCommandHandler> _logger;

        public DeleteChequeissuePayrollByIdCommandHandler(
            IChequeissuePayrollRepository chequeissuePayrollRepository,
            IUnitOfWorkCompany unitOfWorkCompany,
            ICacheService cacheService,
            ICheckDetailRepository chequeDetailRepository,
            ICustomerDetailRepository customerDetailRepository,
            ICustomerRepository customerRepository,
            ICheckRepository chequeRepository,
            IBankDetailRepository bankDetailRepository,
            ICashRegisterDetailRepository cashRegisterDetailRepository,
            ILogger<DeleteChequeissuePayrollByIdCommandHandler> logger,
            IChequeissuePayrollDetailRepository chequeissuePayrollDetailRepository
        )
        {
            _chequeissuePayrollRepository = chequeissuePayrollRepository;
            _unitOfWorkCompany = unitOfWorkCompany;
            _cacheService = cacheService;
            _chequeDetailRepository = chequeDetailRepository;
            _customerDetailRepository = customerDetailRepository;
            _customerRepository = customerRepository;
            _chequeRepository = chequeRepository;
            _bankDetailRepository = bankDetailRepository;
            _cashRegisterDetailRepository = cashRegisterDetailRepository;
            _chequeissuePayrollDetailRepository = chequeissuePayrollDetailRepository;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(
            DeleteChequeissuePayrollByIdCommand request,
            CancellationToken cancellationToken
        )
        {
            using var transaction = await _unitOfWorkCompany.BeginTransactionAsync();

            try
            {
                var chequeissuePayroll = await _chequeissuePayrollRepository
                    .GetAll()
                    .Include(p => p.Details) // ChequeissuePayroll detaylarını dahil et
                    .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                if (chequeissuePayroll == null)
                {
                    _logger.LogWarning("Cheque issue payroll not found, ID: {Id}", request.Id);
                    return Result<string>.Failure("Bordro bulunamadı");
                }

                var customer = await _customerRepository.GetByExpressionAsync(
                    p => p.Id == chequeissuePayroll.CustomerId,
                    cancellationToken
                );
                if (customer == null)
                {
                    _logger.LogWarning("Customer not found, ID: {Id}", chequeissuePayroll.CustomerId);
                    return Result<string>.Failure("Cari bulunamadı");
                }

                customer.DepositAmount -= chequeissuePayroll.PayrollAmount;
                _customerRepository.Update(customer);

                var customerDetails = await _customerDetailRepository
                    .GetAll()
                    .Where(p => p.ChequeissuePayrollId == chequeissuePayroll.Id)
                    .ToListAsync(cancellationToken);
                _customerDetailRepository.DeleteRange(customerDetails);

                // ChequeissuePayrollDetail'den CheckNumber'ları çıkar
                var checkNumbers = chequeissuePayroll.Details.Select(d => d.CheckNumber).ToList();

                // CheckNumber kullanarak ilgili çekleri bul
                var checks = await _chequeRepository
                    .GetAll()
                    .Where(c => checkNumbers.Contains(c.CheckNumber))
                    .ToListAsync(cancellationToken);

                foreach (var check in checks)
                {
                    check.Status = CheckStatus.InPortfolio;
                    _chequeRepository.Update(check);
                    _unitOfWorkCompany.Entry(check).State = EntityState.Modified;

                    var checkDetails = await _chequeDetailRepository
                        .GetAll()
                        .Where(cd => cd.CheckId == check.Id)
                        .ToListAsync(cancellationToken);

                    foreach (var checkDetail in checkDetails)
                    {
                        checkDetail.Status = CheckStatus.InPortfolio;
                        _chequeDetailRepository.Update(checkDetail);
                        _unitOfWorkCompany.Entry(checkDetail).State = EntityState.Modified;
                    }
                }

                // Değişikliklerin kaydedilmesi
                await _unitOfWorkCompany.SaveChangesAsync(cancellationToken);

                var bankDetails = await _bankDetailRepository
                    .GetAll()
                    .Where(bd => bd.ChequeissuePayrollId == chequeissuePayroll.Id)
                    .ToListAsync(cancellationToken);
                _bankDetailRepository.DeleteRange(bankDetails);

                var cashRegisterDetails = await _cashRegisterDetailRepository
                    .GetAll()
                    .Where(crd => crd.ChequeissuePayrollId == chequeissuePayroll.Id)
                    .ToListAsync(cancellationToken);
                _cashRegisterDetailRepository.DeleteRange(cashRegisterDetails);

                _chequeissuePayrollDetailRepository.DeleteRange(chequeissuePayroll.Details);
                _chequeissuePayrollRepository.Delete(chequeissuePayroll);

                await _unitOfWorkCompany.SaveChangesAsync(cancellationToken);

                _cacheService.Remove("chequeissuePayrolls");
                _cacheService.Remove("customers");

                await transaction.CommitAsync();
                _logger.LogInformation("Transaction committed successfully.");
                return Result<string>.Succeed("Bordro başarıyla silindi.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while deleting the cheque issue payroll.");
                return Result<string>.Failure($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
