using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("025f2a17-a535-4221-89de-d2438e9420f2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ba834970-748d-4268-a7ae-e4a685d852dc"));

            migrationBuilder.AddColumn<int>(
                name: "ShippingFee",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("96caab70-e8a2-463b-94cd-5e1a010acdb4"), "2f8715ca-5ebc-47ba-83b1-f0e7bf30ab19", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d2e37866-1f2d-43f4-97dd-0cd8f1e9756b"), "6e40f528-d1b9-48fb-ba3f-02ded75d9195", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("96caab70-e8a2-463b-94cd-5e1a010acdb4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d2e37866-1f2d-43f4-97dd-0cd8f1e9756b"));

            migrationBuilder.DropColumn(
                name: "ShippingFee",
                table: "Bills");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("025f2a17-a535-4221-89de-d2438e9420f2"), "3afef3f9-0510-4659-ab34-6c1e10fb53c8", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("ba834970-748d-4268-a7ae-e4a685d852dc"), "eb8df620-9ad0-4ef7-b556-2f95ff849eab", "Admin", "ADMIN" });
        }
    }
}
