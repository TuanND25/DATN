using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("96caab70-e8a2-463b-94cd-5e1a010acdb4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d2e37866-1f2d-43f4-97dd-0cd8f1e9756b"));

            migrationBuilder.AddColumn<int>(
                name: "STT",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("727e74cb-1876-4036-a7a3-d71a42ac8f15"), "55a9f973-a932-492e-a1f5-ed2e3c7855df", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("e9c51cc4-8d5e-4d70-8785-6d3f0db84fba"), "8beabc59-03c0-4953-8998-969fea11cf59", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "STT",
                table: "Images");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("96caab70-e8a2-463b-94cd-5e1a010acdb4"), "2f8715ca-5ebc-47ba-83b1-f0e7bf30ab19", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d2e37866-1f2d-43f4-97dd-0cd8f1e9756b"), "6e40f528-d1b9-48fb-ba3f-02ded75d9195", "User", "USER" });
        }
    }
}
