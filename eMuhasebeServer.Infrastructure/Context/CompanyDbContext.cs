using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Claims;

namespace eMuhasebeServer.Infrastructure.Context;

public sealed class CompanyDbContext : DbContext, IUnitOfWorkCompany
{
    public async Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await Database.BeginTransactionAsync(cancellationToken);
    }

    #region connection
    private string connectionString = String.Empty;

    public CompanyDbContext(Company company)
    {
        CreateConnectionStringWithCompany(company);
    }

    public CompanyDbContext(
        IHttpContextAccessor contextAccessor,
        ApplicationDbContext applicationDbContext
    )
    {
        CreateConnectionString(contextAccessor, applicationDbContext);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    private void CreateConnectionString(
        IHttpContextAccessor contextAccessor,
        ApplicationDbContext applicationDbContext
    )
    {
        if (contextAccessor.HttpContext is null)
            return;
        string? companyId = contextAccessor.HttpContext.User.FindFirstValue("CompanyId");
        if (string.IsNullOrEmpty(companyId))
            return;
        Company? company = applicationDbContext.Companies.FirstOrDefault(x =>
            x.Id == Guid.Parse(companyId)
        );
        if (company is null)
            return;

        CreateConnectionStringWithCompany(company);
    }

    private void CreateConnectionStringWithCompany(Company company)
    {
        if (string.IsNullOrEmpty(company.Database.UserId))
        {
            connectionString =
                $"Data Source={company.Database.Server};"
                + $"Initial Catalog={company.Database.DatabaseName};"
                + "Integrated Security=True;"
                + "Connect Timeout=30;"
                + "Encrypt=True;"
                + "Trust Server Certificate=true;"
                + "Application Intent=ReadWrite;"
                + "Multi Subnet Failover=False";
        }
        else
        {
            connectionString =
                $"Data Source={company.Database.Server};"
                + $"Initial Catalog={company.Database.DatabaseName};"
                + "Integrated Security=False;"
                + $"User Id={company.Database.UserId};"
                + $"Password={company.Database.Password};"
                + "Connect Timeout=30;"
                + "Encrypt=True;"
                + "Trust Server Certificate=true;"
                + "Application Intent=ReadWrite;"
                + "Multi Subnet Failover=False";
        }
    }

    #endregion

        
    public DbSet<CashRegister> CashRegisters { get; set; }
    public DbSet<CashRegisterDetail> CashRegisterDetails { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<BankDetail> BankDetails { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerDetail> CustomerDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductDetail> ProductDetails { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseDetail> ExpenseDetails { get; set; }
    public DbSet<Check> Checks { get; set; }
    public DbSet<CheckDetail> CheckDetails { get; set; }
    public DbSet<CompanyCheckAccount> CompanyCheckAccounts { get; set; }
    public DbSet<CheckRegisterPayroll> CheckRegisterPayrolls { get; set; }
    public DbSet<CheckRegisterPayrollDetail> CheckRegisterPayrollDetails { get; set; }
    public DbSet<ChequeissuePayrollDetail> ChequeissuePayrollDetails { get; set; }
    public DbSet<ChequeissuePayroll> ChequeissuePayrolls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region cashRegister
        modelBuilder.Entity<CashRegister>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder
            .Entity<CashRegister>()
            .Property(p => p.WithdrawalAmount)
            .HasColumnType("money");
        modelBuilder
            .Entity<CashRegister>()
            .Property(p => p.CurrencyType)
            .HasConversion(type => type.Value, value => CurrencyTypeEnum.FromValue(value));
        modelBuilder.Entity<CashRegister>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder
            .Entity<CashRegister>()
            .HasMany(p => p.Details)
            .WithOne()
            .HasForeignKey(p => p.CashRegisterId);
        #endregion

        #region CashRegisterDetail
        modelBuilder
            .Entity<CashRegisterDetail>()
            .Property(p => p.DepositAmount)
            .HasColumnType("money");
        modelBuilder
            .Entity<CashRegisterDetail>()
            .Property(p => p.WithdrawalAmount)
            .HasColumnType("money");
        modelBuilder
            .Entity<CashRegisterDetail>()
            .Property(p => p.ChequeissuePayrollId)
            .IsRequired(false);
        #endregion

        #region Bank

        modelBuilder.Entity<Bank>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<Bank>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder
            .Entity<Bank>()
            .Property(p => p.CurrencyType)
            .HasConversion(type => type.Value, value => CurrencyTypeEnum.FromValue(value));
        modelBuilder.Entity<Bank>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<Bank>().HasMany(p => p.Details).WithOne().HasForeignKey(p => p.BankId);

        #endregion

        #region BankDetail
        modelBuilder.Entity<BankDetail>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<BankDetail>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder.Entity<BankDetail>().Property(p => p.ChequeissuePayrollId).IsRequired(false);
        #endregion

        #region Customer
        modelBuilder.Entity<Customer>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<Customer>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder
            .Entity<Customer>()
            .Property(p => p.Type)
            .HasConversion(type => type.Value, value => CustomerTypeEnum.FromValue(value));

        modelBuilder.Entity<Customer>().HasQueryFilter(filter => !filter.IsDeleted);
        #endregion

        #region CustomerDetail
        modelBuilder.Entity<CustomerDetail>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder
            .Entity<CustomerDetail>()
            .Property(p => p.WithdrawalAmount)
            .HasColumnType("money");
        modelBuilder
            .Entity<CustomerDetail>()
            .Property(p => p.Type)
            .HasConversion(type => type.Value, value => CustomerDetailTypeEnum.FromValue(value));
        #endregion

        #region Product
        modelBuilder.Entity<Product>().Property(p => p.SellingPrice).HasColumnType("decimal(18,2)");
        modelBuilder
            .Entity<Product>()
            .Property(p => p.PurchasePrice)
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Product>().Property(p => p.Deposit).HasColumnType("decimal(7,2)");
        modelBuilder.Entity<Product>().Property(p => p.Withdrawal).HasColumnType("decimal(7,2)");
        modelBuilder.Entity<Product>().Property(p => p.DiscountRate).HasColumnType("int");
        modelBuilder.Entity<Product>().Property(p => p.PurchaseDiscountRate).HasColumnType("int");
        modelBuilder.Entity<Product>().Property(p => p.TaxRate).HasColumnType("int");
        modelBuilder
            .Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId);
        modelBuilder.Entity<Product>().HasOne(p => p.Unit).WithMany().HasForeignKey(p => p.UnitId);
        modelBuilder.Entity<Product>().HasQueryFilter(filter => !filter.IsDeleted);
        #endregion

        #region ProductDetail
        modelBuilder.Entity<ProductDetail>().Property(p => p.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<ProductDetail>().Property(p => p.Deposit).HasColumnType("decimal(7,2)");
        modelBuilder
            .Entity<ProductDetail>()
            .Property(p => p.Withdrawal)
            .HasColumnType("decimal(7,2)");
        modelBuilder.Entity<ProductDetail>().Property(p => p.DiscountRate).HasColumnType("int");
        modelBuilder.Entity<ProductDetail>().Property(p => p.TaxRate).HasColumnType("int");
        modelBuilder
            .Entity<ProductDetail>()
            .Property(p => p.BrutTotal)
            .HasColumnType("decimal(18,2)");
        modelBuilder
            .Entity<ProductDetail>()
            .Property(p => p.DiscountTotal)
            .HasColumnType("decimal(18,2)");
        modelBuilder
            .Entity<ProductDetail>()
            .Property(p => p.NetTotal)
            .HasColumnType("decimal(18,2)");
        modelBuilder
            .Entity<ProductDetail>()
            .Property(p => p.TaxTotal)
            .HasColumnType("decimal(18,2)");
        modelBuilder
            .Entity<ProductDetail>()
            .Property(p => p.GrandTotal)
            .HasColumnType("decimal(18,2)");

        #endregion

        #region Unit
        modelBuilder.Entity<Unit>().HasQueryFilter(filter => !filter.IsDeleted);
        #endregion

        #region Category
        modelBuilder.Entity<Category>().HasQueryFilter(filter => !filter.IsDeleted);
        #endregion

        #region Invoice
        modelBuilder.Entity<Invoice>().Property(p => p.Amount).HasColumnType("money");
        modelBuilder
            .Entity<Invoice>()
            .Property(p => p.Type)
            .HasConversion(type => type.Value, value => InvoiceTypeEnum.FromValue(value));
        modelBuilder.Entity<Invoice>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<Invoice>().HasQueryFilter(filter => !filter.Customer!.IsDeleted);
        #endregion

        #region InvoiceDetail
        modelBuilder.Entity<InvoiceDetail>().Property(p => p.Price).HasColumnType("decimal(18,2)");
        modelBuilder
            .Entity<InvoiceDetail>()
            .Property(p => p.Quantity)
            .HasColumnType("decimal(7,2)");
        modelBuilder.Entity<InvoiceDetail>().Property(p => p.BrutTotal).HasColumnType("money");
        modelBuilder.Entity<InvoiceDetail>().Property(p => p.DiscountTotal).HasColumnType("money");
        modelBuilder.Entity<InvoiceDetail>().Property(p => p.NetTotal).HasColumnType("money");
        modelBuilder.Entity<InvoiceDetail>().Property(p => p.TaxTotal).HasColumnType("money");
        modelBuilder.Entity<InvoiceDetail>().Property(p => p.GrandTotal).HasColumnType("money");
        modelBuilder.Entity<InvoiceDetail>().Property(p => p.DiscountRate).HasColumnType("int");
        modelBuilder.Entity<InvoiceDetail>().Property(p => p.TaxRate).HasColumnType("int");

        modelBuilder.Entity<InvoiceDetail>().HasQueryFilter(filter => !filter.Product!.IsDeleted);
        #endregion

        #region Expense
        modelBuilder.Entity<Expense>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder
            .Entity<Expense>()
            .Property(p => p.CurrencyType)
            .HasConversion(type => type.Value, value => CurrencyTypeEnum.FromValue(value));
        modelBuilder
            .Entity<Expense>()
            .HasMany(p => p.Details)
            .WithOne()
            .HasForeignKey(p => p.ExpenseId);
        modelBuilder.Entity<Expense>().HasQueryFilter(filter => !filter.IsDeleted);
        #endregion

        #region ExpenseDetail
        modelBuilder
            .Entity<ExpenseDetail>()
            .Property(p => p.WithdrawalAmount)
            .HasColumnType("money");
        #endregion

        #region Check
        modelBuilder.Entity<Check>(entity =>
        {
            entity.Property(p => p.Amount)
                .HasColumnType("money");

            entity.HasQueryFilter(filter => !filter.IsDeleted);

            // CheckDetail ile birebir ilişki (SADECE BURADA TANIMLIYORUZ)
            entity.HasOne(c => c.CheckDetail)
                .WithOne(cd => cd.Check)
                .HasForeignKey<CheckDetail>(cd => cd.CheckId) // CheckDetail'da foreign key
                .OnDelete(DeleteBehavior.Restrict); // veya DeleteBehavior.NoAction

            // ChequeissuePayroll ile ilişki
            entity.HasOne(c => c.ChequeissuePayroll)
                .WithMany()
                .HasForeignKey(c => c.ChequeissuePayrollId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.Property(e => e.CheckType)
                .HasConversion(v => v.Value, v => CheckType.FromValue(v));

            entity.Property(e => e.Status)
                .HasConversion<int>();
        });
        #endregion
        
        #region CheckDetail
        modelBuilder.Entity<CheckDetail>(entity =>
        {
            entity.Property(p => p.Amount).HasColumnType("money");

            // CheckRegisterPayrollDetail ilişkisi
            entity.HasOne(cd => cd.CheckRegisterPayrollDetail)
                .WithMany(d => d.CheckDetails)
                .HasForeignKey(cd => cd.CheckRegisterPayrollDetailId)
                .OnDelete(DeleteBehavior.SetNull);

            // ChequeissuePayrollDetail ilişkisi
            entity.HasOne(cd => cd.ChequeissuePayrollDetail)
                .WithMany(d => d.CheckDetails)
                .HasForeignKey(cd => cd.ChequeissuePayrollDetailId)
                .OnDelete(DeleteBehavior.SetNull); 

        });
        #endregion

        #region CheckRegisterPayroll
        modelBuilder.Entity<CheckRegisterPayroll>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.PayrollAmount)
                .HasColumnType("money");

            entity.Property(p => p.AverageMaturityDate)
                .HasColumnType("date");

            entity.HasQueryFilter(filter => !filter.IsDeleted); // Soft-delete için filtre
            
        });
        #endregion 

        #region CheckRegisterPayrollDetail
        modelBuilder
            .Entity<CheckRegisterPayrollDetail>()
            .Property(p => p.Amount)
            .HasColumnType("money");
        modelBuilder
            .Entity<CheckRegisterPayrollDetail>()
            .Property(p => p.Amount)
            .HasColumnType("money");
        modelBuilder
            .Entity<CheckRegisterPayrollDetail>()
            .HasMany(d => d.Checks)
            .WithOne(c => c.CheckRegisterPayrollDetail)
            .HasForeignKey(c => c.CheckRegisterPayrollDetailId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade kullanarak silme işlemleri kontrolü
        modelBuilder
            .Entity<ChequeissuePayrollDetail>()
            .Property<Guid?>("CheckId") // Shadow property olarak CheckId tanımlayın veya var olan bir property kullanın.
            .IsRequired(false); // İlişkiyi isteğe bağlı yapmak için IsRequired(false) kullanın.
        
        #endregion

        #region CompanyCheckAccount
        modelBuilder.Entity<CheckRegisterPayrollDetail>().HasKey(p => p.Id);
        modelBuilder
            .Entity<CompanyCheckAccount>()
            .Property(p => p.Amount)
            .HasColumnType("decimal(18, 2)");
        


        #endregion

        #region ChequeissuePayrollDetail
        modelBuilder.Entity<ChequeissuePayrollDetail>(entity =>
        {
            entity.Property(p => p.Amount)
                .HasColumnType("money");


            entity.HasOne(d => d.BankDetail)
                .WithMany(b => b.ChequeissuePayrollDetails)
                .HasForeignKey(d => d.BankDetailId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.CashRegisterDetail)
                .WithMany(c => c.ChequeissuePayrollDetails)
                .HasForeignKey(d => d.CashRegisterDetailId)
                .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion
        
        #region ChequeissuePayroll
        modelBuilder.Entity<ChequeissuePayroll>().HasKey(p => p.Id);
        modelBuilder
            .Entity<ChequeissuePayroll>()
            .Property(p => p.PayrollAmount)
            .HasColumnType("money");
        modelBuilder
            .Entity<ChequeissuePayroll>()
            .Property(p => p.AverageMaturityDate)
            .HasColumnType("date");

        modelBuilder
            .Entity<ChequeissuePayroll>()
            .HasOne(crp => crp.Check)
            .WithMany()
            .HasForeignKey(crp => crp.CheckId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<ChequeissuePayroll>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder
            .Entity<ChequeissuePayroll>()
            .HasOne(p => p.Bank)
            .WithMany(b => b.ChequeissuePayrolls)
            .HasForeignKey(p => p.BankId);

        modelBuilder
            .Entity<ChequeissuePayroll>()
            .HasOne(p => p.CashRegister)
            .WithMany(c => c.ChequeissuePayrolls)
            .HasForeignKey(p => p.CashRegisterId);
        
        #endregion
    }
}
