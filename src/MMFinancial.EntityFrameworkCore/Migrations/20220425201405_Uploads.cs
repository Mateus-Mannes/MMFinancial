using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMFinancial.Migrations
{
    public partial class Uploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUploads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUploads", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTrasanctions__DateTime",
                table: "AppTrasanctions",
                column: "_DateTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppUploads_TransactionDate",
                table: "AppUploads",
                column: "TransactionDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUploads");

            migrationBuilder.DropIndex(
                name: "IX_AppTrasanctions__DateTime",
                table: "AppTrasanctions");
        }
    }
}
