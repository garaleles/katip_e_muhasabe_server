using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyType = table.Column<int>(type: "int", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "money", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "money", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashRegisters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyType = table.Column<int>(type: "int", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "money", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "money", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRegisters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCheckAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCheckAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxOffice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "money", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "money", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyType = table.Column<int>(type: "int", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "money", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "money", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "money", nullable: false),
                    BankDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashRegisterDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpenseDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChequeissuePayrollId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankDetails_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashRegisterDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashRegisterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "money", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "money", nullable: false),
                    CashRegisterDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpenseDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChequeissuePayrollId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRegisterDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashRegisterDetails_CashRegisters_CashRegisterId",
                        column: x => x.CashRegisterId,
                        principalTable: "CashRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "money", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "money", nullable: false),
                    BankDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashRegisterDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CheckRegisterPayrollId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChequeissuePayrollId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDetails_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "money", nullable: false),
                    ExpenseDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashRegisterDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseDetails_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    Withdrawal = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    DiscountRate = table.Column<int>(type: "int", nullable: false),
                    PurchaseDiscountRate = table.Column<int>(type: "int", nullable: false),
                    TaxRate = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountRate = table.Column<int>(type: "int", nullable: false),
                    TaxRate = table.Column<int>(type: "int", nullable: false),
                    BrutTotal = table.Column<decimal>(type: "money", nullable: false),
                    DiscountTotal = table.Column<decimal>(type: "money", nullable: false),
                    NetTotal = table.Column<decimal>(type: "money", nullable: false),
                    TaxTotal = table.Column<decimal>(type: "money", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "money", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    Withdrawal = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    DiscountRate = table.Column<int>(type: "int", nullable: false),
                    TaxRate = table.Column<int>(type: "int", nullable: false),
                    BrutTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetails_InvoiceDetails_InvoiceDetailId",
                        column: x => x.InvoiceDetailId,
                        principalTable: "InvoiceDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductDetails_ProductDetails_ProductDetailId",
                        column: x => x.ProductDetailId,
                        principalTable: "ProductDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IssuedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CollectedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CheckNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debtor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creditor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endorser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckRegisterPayrollDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChequeissuePayrollDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckRegisterPayrollDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckRegisterPayrollId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debtor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creditor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endorser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckRegisterPayrollDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckRegisterPayrolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    PayrollNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayrollAmount = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckCount = table.Column<int>(type: "int", nullable: false),
                    AverageMaturityDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckRegisterPayrolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckRegisterPayrolls_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Checks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Debtor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creditor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endorser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CheckRegisterPayrollDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChequeissuePayrollId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChequeissuePayrollDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checks_CheckRegisterPayrollDetails_CheckRegisterPayrollDetailId",
                        column: x => x.CheckRegisterPayrollDetailId,
                        principalTable: "CheckRegisterPayrollDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChequeissuePayrolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    PayrollNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayrollAmount = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckCount = table.Column<int>(type: "int", nullable: false),
                    AverageMaturityDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashRegisterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CashRegisterDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequeissuePayrolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChequeissuePayrolls_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChequeissuePayrolls_CashRegisterDetails_CashRegisterDetailId",
                        column: x => x.CashRegisterDetailId,
                        principalTable: "CashRegisterDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChequeissuePayrolls_CashRegisters_CashRegisterId",
                        column: x => x.CashRegisterId,
                        principalTable: "CashRegisters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChequeissuePayrolls_Checks_CheckId",
                        column: x => x.CheckId,
                        principalTable: "Checks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChequeissuePayrolls_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChequeissuePayrollDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChequeissuePayrollId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debtor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creditor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endorser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BankDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashRegisterDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequeissuePayrollDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChequeissuePayrollDetails_BankDetails_BankDetailId",
                        column: x => x.BankDetailId,
                        principalTable: "BankDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChequeissuePayrollDetails_CashRegisterDetails_CashRegisterDetailId",
                        column: x => x.CashRegisterDetailId,
                        principalTable: "CashRegisterDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChequeissuePayrollDetails_ChequeissuePayrolls_ChequeissuePayrollId",
                        column: x => x.ChequeissuePayrollId,
                        principalTable: "ChequeissuePayrolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_BankId",
                table: "BankDetails",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_CashRegisterId",
                table: "CashRegisterDetails",
                column: "CashRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckDetails_CheckId",
                table: "CheckDetails",
                column: "CheckId",
                unique: true,
                filter: "[CheckId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CheckDetails_CheckRegisterPayrollDetailId",
                table: "CheckDetails",
                column: "CheckRegisterPayrollDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckDetails_ChequeissuePayrollDetailId",
                table: "CheckDetails",
                column: "ChequeissuePayrollDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckRegisterPayrollDetails_CheckRegisterPayrollId",
                table: "CheckRegisterPayrollDetails",
                column: "CheckRegisterPayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckRegisterPayrolls_CheckId",
                table: "CheckRegisterPayrolls",
                column: "CheckId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckRegisterPayrolls_CustomerId",
                table: "CheckRegisterPayrolls",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_CheckRegisterPayrollDetailId",
                table: "Checks",
                column: "CheckRegisterPayrollDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_ChequeissuePayrollDetailId",
                table: "Checks",
                column: "ChequeissuePayrollDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_ChequeissuePayrollId",
                table: "Checks",
                column: "ChequeissuePayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeissuePayrollDetails_BankDetailId",
                table: "ChequeissuePayrollDetails",
                column: "BankDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeissuePayrollDetails_CashRegisterDetailId",
                table: "ChequeissuePayrollDetails",
                column: "CashRegisterDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeissuePayrollDetails_ChequeissuePayrollId",
                table: "ChequeissuePayrollDetails",
                column: "ChequeissuePayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeissuePayrolls_BankId",
                table: "ChequeissuePayrolls",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeissuePayrolls_CashRegisterDetailId",
                table: "ChequeissuePayrolls",
                column: "CashRegisterDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeissuePayrolls_CashRegisterId",
                table: "ChequeissuePayrolls",
                column: "CashRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeissuePayrolls_CheckId",
                table: "ChequeissuePayrolls",
                column: "CheckId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeissuePayrolls_CustomerId",
                table: "ChequeissuePayrolls",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_CustomerId",
                table: "CustomerDetails",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDetails_ExpenseId",
                table: "ExpenseDetails",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_ProductId",
                table: "InvoiceDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_InvoiceDetailId",
                table: "ProductDetails",
                column: "InvoiceDetailId",
                unique: true,
                filter: "[InvoiceDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_InvoiceId",
                table: "ProductDetails",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductDetailId",
                table: "ProductDetails",
                column: "ProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckDetails_CheckRegisterPayrollDetails_CheckRegisterPayrollDetailId",
                table: "CheckDetails",
                column: "CheckRegisterPayrollDetailId",
                principalTable: "CheckRegisterPayrollDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckDetails_Checks_CheckId",
                table: "CheckDetails",
                column: "CheckId",
                principalTable: "Checks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckDetails_ChequeissuePayrollDetails_ChequeissuePayrollDetailId",
                table: "CheckDetails",
                column: "ChequeissuePayrollDetailId",
                principalTable: "ChequeissuePayrollDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckRegisterPayrollDetails_CheckRegisterPayrolls_CheckRegisterPayrollId",
                table: "CheckRegisterPayrollDetails",
                column: "CheckRegisterPayrollId",
                principalTable: "CheckRegisterPayrolls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckRegisterPayrolls_Checks_CheckId",
                table: "CheckRegisterPayrolls",
                column: "CheckId",
                principalTable: "Checks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_ChequeissuePayrollDetails_ChequeissuePayrollDetailId",
                table: "Checks",
                column: "ChequeissuePayrollDetailId",
                principalTable: "ChequeissuePayrollDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_ChequeissuePayrolls_ChequeissuePayrollId",
                table: "Checks",
                column: "ChequeissuePayrollId",
                principalTable: "ChequeissuePayrolls",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankDetails_Banks_BankId",
                table: "BankDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ChequeissuePayrolls_Banks_BankId",
                table: "ChequeissuePayrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_CashRegisterDetails_CashRegisters_CashRegisterId",
                table: "CashRegisterDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ChequeissuePayrolls_CashRegisters_CashRegisterId",
                table: "ChequeissuePayrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_Checks_CheckRegisterPayrollDetails_CheckRegisterPayrollDetailId",
                table: "Checks");

            migrationBuilder.DropForeignKey(
                name: "FK_ChequeissuePayrolls_Checks_CheckId",
                table: "ChequeissuePayrolls");

            migrationBuilder.DropTable(
                name: "CheckDetails");

            migrationBuilder.DropTable(
                name: "CompanyCheckAccounts");

            migrationBuilder.DropTable(
                name: "CustomerDetails");

            migrationBuilder.DropTable(
                name: "ExpenseDetails");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "CashRegisters");

            migrationBuilder.DropTable(
                name: "CheckRegisterPayrollDetails");

            migrationBuilder.DropTable(
                name: "CheckRegisterPayrolls");

            migrationBuilder.DropTable(
                name: "Checks");

            migrationBuilder.DropTable(
                name: "ChequeissuePayrollDetails");

            migrationBuilder.DropTable(
                name: "BankDetails");

            migrationBuilder.DropTable(
                name: "ChequeissuePayrolls");

            migrationBuilder.DropTable(
                name: "CashRegisterDetails");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
