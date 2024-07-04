using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyCheckAccounts_CompanyCheckissuePayrollId",
                table: "CompanyCheckAccounts");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyCheckAccountId",
                table: "CompanyCheckAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCheckAccounts_CompanyCheckAccountId",
                table: "CompanyCheckAccounts",
                column: "CompanyCheckAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCheckAccounts_CompanyCheckissuePayrollId",
                table: "CompanyCheckAccounts",
                column: "CompanyCheckissuePayrollId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCheckAccounts_CompanyCheckAccounts_CompanyCheckAccountId",
                table: "CompanyCheckAccounts",
                column: "CompanyCheckAccountId",
                principalTable: "CompanyCheckAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCheckAccounts_CompanyCheckAccounts_CompanyCheckAccountId",
                table: "CompanyCheckAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CompanyCheckAccounts_CompanyCheckAccountId",
                table: "CompanyCheckAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CompanyCheckAccounts_CompanyCheckissuePayrollId",
                table: "CompanyCheckAccounts");

            migrationBuilder.DropColumn(
                name: "CompanyCheckAccountId",
                table: "CompanyCheckAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCheckAccounts_CompanyCheckissuePayrollId",
                table: "CompanyCheckAccounts",
                column: "CompanyCheckissuePayrollId",
                unique: true,
                filter: "[CompanyCheckissuePayrollId] IS NOT NULL");
        }
    }
}
