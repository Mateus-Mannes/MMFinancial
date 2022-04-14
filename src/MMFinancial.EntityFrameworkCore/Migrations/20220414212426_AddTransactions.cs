using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMFinancial.Migrations
{
    public partial class AddTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTrasanctions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgencyFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgencyTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccounTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    _DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTrasanctions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTrasanctions");
        }
    }
}
