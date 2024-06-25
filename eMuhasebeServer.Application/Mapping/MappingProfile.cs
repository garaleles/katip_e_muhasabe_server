using AutoMapper;
using AutoMapper.Execution;
using eMuhasebeServer.Application.Features.Banks.CreateBanks;
using eMuhasebeServer.Application.Features.Banks.UpdateBanks;
using eMuhasebeServer.Application.Features.CashRegisters.CreateCashRegister;
using eMuhasebeServer.Application.Features.CashRegisters.UpdateCashRegister;
using eMuhasebeServer.Application.Features.Categories.CreateCategories;
using eMuhasebeServer.Application.Features.Categories.UpdateCategories;
using eMuhasebeServer.Application.Features.Companies.CreateCompany;
using eMuhasebeServer.Application.Features.Companies.UpdateCompany;
using eMuhasebeServer.Application.Features.Customers.CreateCustomer;
using eMuhasebeServer.Application.Features.Customers.UpdateCustomer;
using eMuhasebeServer.Application.Features.Expenses.CreateExpenses;
using eMuhasebeServer.Application.Features.Expenses.UpdateExpenses;
using eMuhasebeServer.Application.Features.Invoices.CreateInvoice;
using eMuhasebeServer.Application.Features.Products.CreateProduct;
using eMuhasebeServer.Application.Features.Products.UpdateProduct;
using eMuhasebeServer.Application.Features.Units.CreateUnits;
using eMuhasebeServer.Application.Features.Units.UpdateUnits;
using eMuhasebeServer.Application.Features.Users.CreateUser;
using eMuhasebeServer.Application.Features.Users.UpdateUser;
using eMuhasebeServer.Domain.DTOs;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;

namespace eMuhasebeServer.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, AppUser>();
            CreateMap<UpdateUserCommand, AppUser>();

            CreateMap<CreateCompanyCommand, Company>();
            CreateMap<UpdateCompanyCommand, Company>();

            CreateMap<CreateCashRegisterCommand, CashRegister>().ForMember(member => member.CurrencyType, options =>
            {
                options.MapFrom(map => CurrencyTypeEnum.FromValue(map.CurrencyTypeValue));
            });
            CreateMap<UpdateCashRegisterCommand, CashRegister>().ForMember(member => member.CurrencyType, options =>
            {
                options.MapFrom(map => CurrencyTypeEnum.FromValue(map.CurrencyTypeValue));
            });
            
            CreateMap<CreateBankCommand, Bank>().ForMember(member=>member.CurrencyType,options =>
            {
                options.MapFrom(map => CurrencyTypeEnum.FromValue(map.CurrencyTypeValue));
            });
            CreateMap<UpdateBankCommand, Bank>().ForMember(member=>member.CurrencyType,options =>
            {
                options.MapFrom(map => CurrencyTypeEnum.FromValue(map.CurrencyTypeValue));
            });

            CreateMap<CreateCustomerCommand, Customer>().ForMember(member=>member.Type,options =>
            {
                options.MapFrom(map => CustomerTypeEnum.FromValue(map.TypeValue));
            });
            CreateMap<UpdateCustomerCommand, Customer>().ForMember(member=>member.Type,options =>
            {
                options.MapFrom(map => CustomerTypeEnum.FromValue(map.TypeValue));
            });

            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();

            CreateMap<CreateUnitCommand, Unit>();
            CreateMap<UpdateUnitCommand, Unit>();
            
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();

             CreateMap<CreateInvoiceCommand, Invoice>()
            .ForMember(member => member.Type, options =>
            {
                options.MapFrom(map => InvoiceTypeEnum.FromValue(map.TypeValue));
            })
            .ForMember(member => member.Details, options =>
            {
                options.MapFrom(map => map.Details.Select(s => new InvoiceDetail()
                {
                    ProductId = s.ProductId,
                    Quantity = s.Quantity,
                    Price = s.Price,
                    DiscountRate = s.DiscountRate,
                    TaxRate = s.TaxRate,
                    BrutTotal = s.Price * s.Quantity,
                    DiscountTotal = s.Price * s.Quantity * s.DiscountRate / 100,
                    NetTotal = (s.Price * s.Quantity) - (s.Price * s.Quantity * s.DiscountRate / 100),
                    TaxTotal = ((s.Price * s.Quantity) - (s.Price * s.Quantity * s.DiscountRate / 100)) * s.TaxRate / 100,
                    GrandTotal = ((s.Price * s.Quantity) - (s.Price * s.Quantity * s.DiscountRate / 100)) + (((s.Price * s.Quantity) - (s.Price * s.Quantity * s.DiscountRate / 100)) * s.TaxRate / 100)
                }).ToList());
            })
            .ForMember(member => member.Amount, options =>
            {
                options.MapFrom(map => map.Details.Sum(detail =>
                  detail.GrandTotal // grandTotal
                ));
            });


             CreateMap<CreateExpenseCommand, Expense>()
                 .ForMember(member => member.CurrencyType, options =>
                 {
                     options.MapFrom(map => CurrencyTypeEnum.FromValue(map.CurrencyTypeValue));
                 });  
                
             CreateMap<UpdateExpenseCommand, Expense>()
                 .ForMember(member => member.CurrencyType, options =>
                 {
                     options.MapFrom(map => CurrencyTypeEnum.FromValue(map.CurrencyTypeValue));
                 });
             
        
            
           
        }

           

        }
        
}
    




