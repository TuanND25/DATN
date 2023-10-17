using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11ea7e8c-a162-4882-b00c-58de42f3e3fe"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b90523d9-1abd-4140-a1a2-97c409429ae6"));

            migrationBuilder.RenameColumn(
                name: "Reduced_Value",
                table: "Vouchers",
                newName: "Percent");

            migrationBuilder.AlterColumn<int>(
                name: "Discount_Conditions",
                table: "Vouchers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Maximum_Reduction",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PurchasePrice",
                table: "ProductItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CostPrice",
                table: "ProductItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AvaiableQuantity",
                table: "ProductItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("025f2a17-a535-4221-89de-d2438e9420f2"), "3afef3f9-0510-4659-ab34-6c1e10fb53c8", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("ba834970-748d-4268-a7ae-e4a685d852dc"), "eb8df620-9ad0-4ef7-b556-2f95ff849eab", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("025f2a17-a535-4221-89de-d2438e9420f2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ba834970-748d-4268-a7ae-e4a685d852dc"));

            migrationBuilder.DropColumn(
                name: "Maximum_Reduction",
                table: "Vouchers");

            migrationBuilder.RenameColumn(
                name: "Percent",
                table: "Vouchers",
                newName: "Reduced_Value");

            migrationBuilder.AlterColumn<string>(
                name: "Discount_Conditions",
                table: "Vouchers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PurchasePrice",
                table: "ProductItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CostPrice",
                table: "ProductItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AvaiableQuantity",
                table: "ProductItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("11ea7e8c-a162-4882-b00c-58de42f3e3fe"), "8b94e2e7-2bdb-4292-ac20-f7c2f33ee7f2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("b90523d9-1abd-4140-a1a2-97c409429ae6"), "68338d6f-33e9-4c07-9aef-36aec9bd5c47", "Admin", "ADMIN" });
        }
    }
}
