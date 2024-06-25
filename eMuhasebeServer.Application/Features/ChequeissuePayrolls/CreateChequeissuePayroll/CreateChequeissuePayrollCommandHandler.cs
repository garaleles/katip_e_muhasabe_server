using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ChequeissuePayrolls.CreateChequeissuePayroll;

internal sealed class CreateChequeissuePayrollCommandHandler
    : IRequestHandler<CreateChequeissuePayrollCommand, Result<string>>
{
    private readonly IChequeissuePayrollRepository _chequeissuePayrollRepository;
  
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerDetailRepository _customerDetailRepository;
    private readonly ICheckRepository _checkRepository;
    private readonly ICheckDetailRepository _checkDetailRepository;
    private readonly ICashRegisterRepository _cashRegisterRepository;
    private readonly ICashRegisterDetailRepository _cashRegisterDetailRepository;
    private readonly IBankRepository _bankRepository;
    private readonly IBankDetailRepository _bankDetailRepository;
    private readonly IUnitOfWorkCompany _unitOfWorkCompany;
    private readonly ILogger<CreateChequeissuePayrollCommandHandler> _logger;
    private readonly ICacheService _cacheService;

    public CreateChequeissuePayrollCommandHandler(
        ILogger<CreateChequeissuePayrollCommandHandler> logger,
        IChequeissuePayrollRepository chequeissuePayrollRepository,
       
        ICustomerRepository customerRepository,
        ICustomerDetailRepository customerDetailRepository,
        ICheckRepository checkRepository,
        ICheckDetailRepository checkDetailRepository,
        ICashRegisterRepository cashRegisterRepository,
        ICashRegisterDetailRepository cashRegisterDetailRepository,
        IBankRepository bankRepository,
        IBankDetailRepository bankDetailRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        ICacheService cacheService
    )
    {
        _chequeissuePayrollRepository = chequeissuePayrollRepository;
    
        _customerRepository = customerRepository;
        _customerDetailRepository = customerDetailRepository;
        _checkRepository = checkRepository;
        _checkDetailRepository = checkDetailRepository;
        _cashRegisterRepository = cashRegisterRepository;
        _cashRegisterDetailRepository = cashRegisterDetailRepository;
        _bankRepository = bankRepository;
        _bankDetailRepository = bankDetailRepository;
        _unitOfWorkCompany = unitOfWorkCompany;
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(
        CreateChequeissuePayrollCommand request,
        CancellationToken cancellationToken
    )
    {
        using var transaction = await _unitOfWorkCompany.BeginTransactionAsync();

        try
        {
            // Log incoming request
            Console.WriteLine($"Received Request: {JsonConvert.SerializeObject(request)}");

            // Bank ve Kasa bilgilerini al
            Bank? bank =
                request.bankId != Guid.Empty
                    ? await _bankRepository.GetByExpressionAsync(
                        p => p.Id == request.bankId,
                        cancellationToken
                    )
                    : null;
            CashRegister? cashRegister =
                request.cashRegisterId != Guid.Empty
                    ? await _cashRegisterRepository.GetByExpressionAsync(
                        p => p.Id == request.cashRegisterId,
                        cancellationToken
                    )
                    : null;

            if (request.bankId != Guid.Empty && bank == null)
                return Result<string>.Failure("Banka bulunamadı");

            if (request.cashRegisterId != Guid.Empty && cashRegister == null)
                return Result<string>.Failure("Kasa bulunamadı");

            // ChequeissuePayroll entity'sini oluştur
            var chequeissuePayroll = new ChequeissuePayroll
            {
                Date = request.Date,
                PayrollNumber = request.PayrollNumber,
                CustomerId = request.CustomerId,
                PayrollAmount = request.PayrollAmount,
                Description = request.Description,
                CheckCount = request.CheckCount,
                AverageMaturityDate = request.AverageMaturityDate,
                Details = request.Details
            };

            // ChequeissuePayroll'u veritabanına ekle
            await _chequeissuePayrollRepository.AddAsync(chequeissuePayroll, cancellationToken);

            // Cari hesabı getir
            Customer? customer = await _customerRepository.GetByExpressionAsync(
                p => p.Id == chequeissuePayroll.CustomerId,
                cancellationToken
            );
            if (customer is null)
                throw new ArgumentException("Cari bulunamadı");

            // Her bir çek detayı için işlem yap
            await ProcessChequeDetailsAsync(
                chequeissuePayroll,
                bank,
                cashRegister,
                cancellationToken
            );

            // Değişiklikleri kaydet ve cache'i temizle
            await _unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            _cacheService.Remove("chequeissuePayrolls");

            await transaction.CommitAsync();
            return Result<string>.Succeed("Bordro başarıyla oluşturuldu.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Hata: {ex.Message}");
            _logger.LogError(ex, "An error occurred while creating the chequeissue payroll.");
            return Result<string>.Failure($"Bir hata oluştu: {ex.Message}");
        }
    }

    private async Task ProcessChequeDetailsAsync(ChequeissuePayroll chequeissuePayroll, Bank? bank, CashRegister? cashRegister, CancellationToken cancellationToken)
    {
        foreach (var detail in chequeissuePayroll.Details)
        {
            var existingCheck = await _checkRepository.GetByExpressionAsync(c => c.CheckNumber == detail.CheckNumber, cancellationToken);

            if (existingCheck != null)
            {
                // Mevcut check kaydını güncelle (sadece statü güncellemesi)
                existingCheck.Status = (CheckStatus)3;
                _checkRepository.Update(existingCheck);

                // CheckDetail bilgisini ayrı bir sorguyla yükle
                var checkDetail = await _checkDetailRepository.GetByExpressionAsync(cd => cd.CheckId == existingCheck.Id, cancellationToken);

                if (checkDetail != null)
                {
                    // CheckDetail güncelleniyor
                    checkDetail.Status = (CheckStatus)3;
                    checkDetail.DueDate = detail.DueDate;
                    checkDetail.BankName = detail.BankName;
                    checkDetail.BranchName = detail.BranchName;
                    checkDetail.AccountNumber = detail.AccountNumber;
                    checkDetail.Amount = detail.Amount;
                    checkDetail.Debtor = detail.Debtor;
                    checkDetail.Creditor = detail.Creditor;
                    checkDetail.Endorser = detail.Endorser;
                    checkDetail.ChequeissuePayrollDetailId = detail.Id;
                    _checkDetailRepository.Update(checkDetail);
                }
                else
                {
                    // Yeni CheckDetail oluşturuluyor
                    checkDetail = new CheckDetail
                    {
                        CheckId = existingCheck.Id,
                        Status = (CheckStatus)3,
                        DueDate = detail.DueDate,
                        CheckNumber = detail.CheckNumber,
                        BankName = detail.BankName,
                        BranchName = detail.BranchName,
                        AccountNumber = detail.AccountNumber,
                        Amount = detail.Amount,
                        Debtor = detail.Debtor,
                        Creditor = detail.Creditor,
                        Endorser = detail.Endorser,
                        ChequeissuePayrollDetailId = detail.Id
                    };

                    await _checkDetailRepository.AddAsync(checkDetail, cancellationToken);
                }
            }
              
            
            else
            {
                // Check kaydı yoksa yeni oluşturulur ve CheckDetail de ona atanır.
                var newCheck = new Check
                {
                    Status = (CheckStatus)3,
                    DueDate = detail.DueDate,
                    CheckNumber = detail.CheckNumber,
                    BankName = detail.BankName,
                    BranchName = detail.BranchName,
                    AccountNumber = detail.AccountNumber,
                    Amount = detail.Amount,
                    Debtor = detail.Debtor,
                    Creditor = detail.Creditor,
                    Endorser = detail.Endorser,
                    ChequeissuePayrollId = chequeissuePayroll.Id
                };
                await _checkRepository.AddAsync(newCheck, cancellationToken);
                await _unitOfWorkCompany.SaveChangesAsync(cancellationToken); // newCheck'i kaydet

                var checkDetail = new CheckDetail
                {
                    CheckId = newCheck.Id, // İlişkilendirme burada yapılıyor
                    Status = (CheckStatus)3,
                    DueDate = detail.DueDate,
                    CheckNumber = detail.CheckNumber,
                    BankName = detail.BankName,
                    BranchName = detail.BranchName,
                    AccountNumber = detail.AccountNumber,
                    Amount = detail.Amount,
                    Debtor = detail.Debtor,
                    Creditor = detail.Creditor,
                    Endorser = detail.Endorser,
                    ChequeissuePayrollDetailId = detail.Id
                };

                await _checkDetailRepository.AddAsync(checkDetail, cancellationToken);

                // detail.CheckId = newCheck.Id; // CheckId atama (artık bu satıra gerek yok)
            }


            // CheckStatus işlemini gerçekleştirin (bu kısım aynı kalabilir)
            await ProcessCheckStatusAsync(existingCheck ?? new Check(), chequeissuePayroll, bank, cashRegister, cancellationToken);
        }
    }



    private async Task ProcessCheckStatusAsync(
        Check check,
        ChequeissuePayroll chequeissuePayroll,
        Bank? bank,
        CashRegister? cashRegister,
        CancellationToken cancellationToken
    )
    {
        // Customer'ı al
        var customer = await _customerRepository.GetByExpressionAsync(
            c => c.Id == chequeissuePayroll.CustomerId,
            cancellationToken
        );
        if (customer == null)
        {
            throw new InvalidOperationException(
                $"Customer with ID {chequeissuePayroll.CustomerId} not found."
            );
        }

        // Mevcut Check ve CheckDetail verilerini veritabanından çekin
        var existingCheck = await _checkRepository.GetByIdAsync(check.Id, cancellationToken);
        if (existingCheck == null)
        {
            throw new InvalidOperationException($"Check with ID {check.Id} not found.");
        }

        // CheckDetail null kontrolü eklendi
        if (existingCheck.CheckDetail == null)
        {
            throw new InvalidOperationException(
                $"CheckDetail for Check with ID {check.Id} not found."
            );
        }

        var existingCheckDetail = existingCheck.CheckDetail; // CheckDetail nesnesini existingCheck üzerinden alın

        switch (check.Status)
        {
            case CheckStatus.Endorsed:
                // Increase customer's deposit amount
                customer.DepositAmount += chequeissuePayroll.PayrollAmount;
                _customerRepository.Update(customer);

                // Clear the cache after updating the customer balance
                _cacheService.Remove("customers");

                // Create customer detail entry
                var customerDetail = new CustomerDetail
                {
                    CustomerId = customer.Id,
                    Date = chequeissuePayroll.Date,
                    ProcessNumber = chequeissuePayroll.PayrollNumber,
                    DepositAmount = chequeissuePayroll.PayrollAmount,
                    Description = $"Çek ile ödeme: {chequeissuePayroll.PayrollNumber}",
                    Type = CustomerDetailTypeEnum.Check,
                    ChequeissuePayrollId = chequeissuePayroll.Id
                };
                await _customerDetailRepository.AddAsync(customerDetail, cancellationToken);

                // Check ve CheckDetail durumlarını güncelleyin
                existingCheck.Status = CheckStatus.Endorsed;
                existingCheckDetail.Status = CheckStatus.Endorsed;
                existingCheckDetail.CheckId = existingCheck.Id; // CheckId'yi CheckDetail'e atayın

                // Güncellenmiş verileri veritabanına kaydedin
                _checkRepository.Update(existingCheck);
                _checkDetailRepository.Update(existingCheckDetail);

                break;

            case CheckStatus.Banked:
            case CheckStatus.SendToBankForCollateral:
                if (bank != null)
                {
                    // Increase bank's deposit amount
                    bank.DepositAmount += chequeissuePayroll.PayrollAmount;
                    _bankRepository.Update(bank);

                    // Create bank detail entry
                    var bankDetail = new BankDetail
                    {
                        BankId = bank.Id,
                        Date = chequeissuePayroll.Date,
                        ProcessNumber = chequeissuePayroll.PayrollNumber,
                        DepositAmount = chequeissuePayroll.PayrollAmount,
                        Description = $"Çek çıkışı: {chequeissuePayroll.PayrollNumber}",
                        ChequeissuePayrollId = chequeissuePayroll.Id
                    };
                    await _bankDetailRepository.AddAsync(bankDetail, cancellationToken);
                }

                break;

            case CheckStatus.Paid:
                if (cashRegister != null)
                {
                    // Increase cash register's deposit amount
                    cashRegister.DepositAmount += chequeissuePayroll.PayrollAmount;
                    _cashRegisterRepository.Update(cashRegister);

                    // Create cash register detail entry
                    var cashRegisterDetail = new CashRegisterDetail
                    {
                        CashRegisterId = cashRegister.Id,
                        Date = chequeissuePayroll.Date,
                        ProcessNumber = chequeissuePayroll.PayrollNumber,
                        DepositAmount = chequeissuePayroll.PayrollAmount,
                        Description = $"Çek tahsilatı: {chequeissuePayroll.PayrollNumber}",
                        ChequeissuePayrollId = chequeissuePayroll.Id
                    };
                    await _cashRegisterDetailRepository.AddAsync(
                        cashRegisterDetail,
                        cancellationToken
                    );
                }

                break;

            case CheckStatus.Unpaid:
            case CheckStatus.Returned:
                // Increase customer's deposit amount
                customer.DepositAmount += chequeissuePayroll.PayrollAmount;
                _customerRepository.Update(customer);

                // Create customer detail entry
                var unpaidReturnedDescription =
                    check.Status == CheckStatus.Unpaid
                        ? "Karşılıksız çek"
                        : $"İade edilen çek: {chequeissuePayroll.PayrollNumber}";
                var customerDetailUnpaidReturned = new CustomerDetail
                {
                    CustomerId = customer.Id,
                    Date = chequeissuePayroll.Date,
                    ProcessNumber = chequeissuePayroll.PayrollNumber,
                    DepositAmount = chequeissuePayroll.PayrollAmount,
                    Description = unpaidReturnedDescription,
                    Type = CustomerDetailTypeEnum.Check,
                    ChequeissuePayrollId = chequeissuePayroll.Id
                };
                await _customerDetailRepository.AddAsync(
                    customerDetailUnpaidReturned,
                    cancellationToken
                );
                break;

            default:
                throw new ArgumentException($"Geçersiz çek durumu: {check.Status}");
        }
    }
}
