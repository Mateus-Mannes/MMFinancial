using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMFinancial.Migrations
{
    public partial class UploadRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "AppUploads",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UploadId",
                table: "AppTrasanctions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppTrasanctions_UploadId",
                table: "AppTrasanctions",
                column: "UploadId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTrasanctions_AppUploads_UploadId",
                table: "AppTrasanctions",
                column: "UploadId",
                principalTable: "AppUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTrasanctions_AppUploads_UploadId",
                table: "AppTrasanctions");

            migrationBuilder.DropIndex(
                name: "IX_AppTrasanctions_UploadId",
                table: "AppTrasanctions");

            migrationBuilder.DropColumn(
                name: "UploadId",
                table: "AppTrasanctions");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "AppUploads",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
