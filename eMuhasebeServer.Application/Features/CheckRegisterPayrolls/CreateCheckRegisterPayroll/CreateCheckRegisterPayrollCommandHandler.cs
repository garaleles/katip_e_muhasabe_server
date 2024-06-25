using Ardalis.SmartEnum;
using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Newtonsoft.Json;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CheckRegisterPayrolls.CreateCheckRegisterPayroll;

internal sealed class
    CreateCheckRegisterPayrollCommandHandler : IRequestHandler<CreateCheckRegisterPayrollCommand, Result<string>>
{
    private readonly ICheckRegisterPayrollRepository _checkRegisterPayrollRepository;
    private readonly ICheckRegisterPayrollDetailRepository _checkRegisterPayrollDetailRepository;
    private readonly IUnitOfWorkCompany _unitOfWorkCompany;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerDetailRepository _customerDetailRepository;
    private readonly ICheckRepository _checkRepository;
    private readonly ICheckDetailRepository _checkDetailRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public CreateCheckRegisterPayrollCommandHandler(
        ICheckRegisterPayrollRepository checkRegisterPayrollRepository,
        ICheckRegisterPayrollDetailRepository checkRegisterPayrollDetailRepository,
        ICustomerRepository customerRepository,
        ICustomerDetailRepository customerDetailRepository,
        ICheckRepository checkRepository,
        ICheckDetailRepository checkDetailRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        IMapper mapper,
        ICacheService cacheService)
    {
        _checkRegisterPayrollRepository = checkRegisterPayrollRepository;
        _checkRegisterPayrollDetailRepository = checkRegisterPayrollDetailRepository;
        _customerRepository = customerRepository;
        _customerDetailRepository = customerDetailRepository;
        _checkRepository = checkRepository;
        _checkDetailRepository = checkDetailRepository;
        _unitOfWorkCompany = unitOfWorkCompany;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<Result<string>> Handle(CreateCheckRegisterPayrollCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Log incoming request
            Console.WriteLine($"Received Request: {JsonConvert.SerializeObject(request)}");

          

            // Convert command to entity
            var checkRegisterPayroll = new CheckRegisterPayroll
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

            // Add payroll to repository
            await _checkRegisterPayrollRepository.AddAsync(checkRegisterPayroll, cancellationToken);
            await _unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            // Optionally update cache
            _cacheService.Remove("checkRegisterPayrolls");

            // Get the customer
            Customer? customer =
                await _customerRepository.GetByExpressionAsync(p => p.Id == request.CustomerId, cancellationToken);
            if (customer is null)
            {
                return Result<string>.Failure("Cari bulunamadı");
            }

            // Increase the customer's deposit amount by the payroll amount
            customer.WithdrawalAmount += request.PayrollAmount;
            _customerRepository.Update(customer);

            // Create a new CustomerDetail entry for the check deposit
            CustomerDetail customerDetail = new()
            {
                CustomerId = customer.Id,
                Date = request.Date,
                ProcessNumber = request.PayrollNumber,
                WithdrawalAmount = request.PayrollAmount,
                Description = "Çek Girişi: " + request.PayrollNumber,
                Type = CustomerDetailTypeEnum.Check,
                CheckRegisterPayrollId = checkRegisterPayroll.Id
            };
            await _customerDetailRepository.AddAsync(customerDetail, cancellationToken);

            // Check and CheckDetail entities creation
            foreach (var detail in checkRegisterPayroll.Details)
            {
                var check = new Check
                {
                    CheckType = CheckType.Inward,
                    Status = CheckStatus.InPortfolio,
                    DueDate = detail.DueDate,
                    CheckNumber = detail.CheckNumber,
                    BankName = detail.BankName,
                    BranchName = detail.BranchName,
                    AccountNumber = detail.AccountNumber,
                    Amount = detail.Amount,
                    Debtor = detail.Debtor,
                    Creditor = detail.Creditor,
                    Endorser = detail.Endorser,
                    CheckRegisterPayrollDetailId = detail.Id
                };
                await _checkRepository.AddAsync(check, cancellationToken);
                await _unitOfWorkCompany.SaveChangesAsync(cancellationToken);

                var checkDetail = new CheckDetail
                {
                    CheckId = check.Id,
                    Status = CheckStatus.InPortfolio,
                    DueDate = detail.DueDate,
                    CheckNumber = detail.CheckNumber,
                    BankName = detail.BankName,
                    BranchName = detail.BranchName,
                    AccountNumber = detail.AccountNumber,
                    Amount = detail.Amount,
                    Debtor = detail.Debtor,
                    Creditor = detail.Creditor,
                    Endorser = detail.Endorser,
                    CheckRegisterPayrollDetailId = detail.Id,
                };
                await _checkDetailRepository.AddAsync(checkDetail, cancellationToken);

                check.CheckDetail = checkDetail;
                _checkRepository.Update(check);
            }

            // Save changes to the database
            await _unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            // Optionally update cache
            _cacheService.Remove("customers");
            _cacheService.Remove("customerDetails");
            _cacheService.Remove("checks");
            _cacheService.Remove("checkDetails");

            return Result<string>.Succeed("Bordro başarıyla oluşturuldu.");
        }
        catch (ArgumentException ex)
        {
            // Log argument errors
            Console.WriteLine($"Argument Error: {ex.Message}");
            return Result<string>.Failure($"Geçersiz parametre: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Log the detailed error
            var detailedError = ex.InnerException?.Message ?? ex.Message;
            Console.WriteLine($"Error: {detailedError}");
            throw new InvalidOperationException($"An error occurred while saving the entity changes: {detailedError}",
                ex);
        }
    }
}


