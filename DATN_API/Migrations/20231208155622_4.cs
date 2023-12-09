using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("145413ea-e319-4008-b124-103abfffaf7f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a31a40bc-35f2-4e02-af32-e400f81fd202"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("acfa5020-3be0-4532-a486-3f33493bf1c1"));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Promotions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelDate",
                table: "Bills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CanelBy",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippingDate",
                table: "Bills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("42327f7f-4673-4cfb-80e0-480743e45d3a"), "20788ab1-2f5d-4462-bff2-99e73ffcc722", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("75eb4888-2f04-4015-b985-b7fdabacbe10"), "0b055a5a-2d7a-4b09-aec0-a9d04e1219b6", "Staff", "STAFF" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("fd350b4f-90cf-4a42-ae1f-28a7ad340203"), "ba0da5d4-d937-48b2-8684-27efa15220b9", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("42327f7f-4673-4cfb-80e0-480743e45d3a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("75eb4888-2f04-4015-b985-b7fdabacbe10"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fd350b4f-90cf-4a42-ae1f-28a7ad340203"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CancelDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CanelBy",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ShippingDate",
                table: "Bills");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("145413ea-e319-4008-b124-103abfffaf7f"), "0d8a5a8a-cd0b-4cc0-9b63-9f03c04ef6c5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("a31a40bc-35f2-4e02-af32-e400f81fd202"), "43a6133f-7d14-46d9-8d0d-17bfba1cb797", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("acfa5020-3be0-4532-a486-3f33493bf1c1"), "416562a0-61db-4d83-873e-2b4bc7c0ab11", "Staff", "STAFF" });
        }
    }
}
