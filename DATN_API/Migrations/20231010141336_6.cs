using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("10668196-d9f8-4c21-8d25-c2345e71e236"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("776fcedf-a9e1-4690-80d6-ec04210ed160"));

            migrationBuilder.RenameColumn(
                name: "Transport_Fee",
                table: "Bills",
                newName: "ReducedAmount");

            migrationBuilder.RenameColumn(
                name: "ShipCode",
                table: "Bills",
                newName: "Type");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Bills",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BillCode",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Cash",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "Bills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmationDate",
                table: "Bills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CustomerPayment",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinalAmount",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VoucherId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "WardCode",
                table: "AddressShips",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("590b0193-95ba-48ed-98d9-a7adb86dde73"), "4227fdd4-84b4-4a6a-b5ed-b75df2173446", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("ecdbe129-76cd-4f7f-83f7-0a589e2addae"), "33db813e-fc25-4f08-9aff-9663a9b6b571", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PaymentMethodId",
                table: "Bills",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_VoucherId",
                table: "Bills",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_PaymentMethods_PaymentMethodId",
                table: "Bills",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Vouchers_VoucherId",
                table: "Bills",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_PaymentMethods_PaymentMethodId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Vouchers_VoucherId",
                table: "Bills");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_Bills_PaymentMethodId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_VoucherId",
                table: "Bills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("590b0193-95ba-48ed-98d9-a7adb86dde73"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ecdbe129-76cd-4f7f-83f7-0a589e2addae"));

            migrationBuilder.DropColumn(
                name: "BillCode",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Cash",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ConfirmationDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CustomerPayment",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "FinalAmount",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Bills",
                newName: "ShipCode");

            migrationBuilder.RenameColumn(
                name: "ReducedAmount",
                table: "Bills",
                newName: "Transport_Fee");

            migrationBuilder.AlterColumn<string>(
                name: "CreateDate",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "WardCode",
                table: "AddressShips",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("10668196-d9f8-4c21-8d25-c2345e71e236"), "9aa9c8b8-e952-4131-a4a1-9f6a4f1561fc", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("776fcedf-a9e1-4690-80d6-ec04210ed160"), "8d300059-8693-403d-8877-9cc279740c9c", "Admin", "ADMIN" });
        }
    }
}
