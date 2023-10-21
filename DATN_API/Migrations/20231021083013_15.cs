using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("727e74cb-1876-4036-a7a3-d71a42ac8f15"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e9c51cc4-8d5e-4d70-8785-6d3f0db84fba"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Bills");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumberPhone",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Recipient",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToAddress",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WardName",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("2b5ee318-61e5-4658-ae62-68a1426d88ea"), "77cf1d3b-5033-4f4f-888d-cb7f9ba01a60", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("eab9a92b-d222-44e9-9a8a-68962843cfaa"), "63be7198-601a-49f5-8d8e-3149520e36c5", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2b5ee318-61e5-4658-ae62-68a1426d88ea"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eab9a92b-d222-44e9-9a8a-68962843cfaa"));

            migrationBuilder.DropColumn(
                name: "District",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "NumberPhone",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Recipient",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ToAddress",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "WardName",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Promotions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("727e74cb-1876-4036-a7a3-d71a42ac8f15"), "55a9f973-a932-492e-a1f5-ed2e3c7855df", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("e9c51cc4-8d5e-4d70-8785-6d3f0db84fba"), "8beabc59-03c0-4953-8998-969fea11cf59", "User", "USER" });
        }
    }
}
