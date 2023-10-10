using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("871a3f86-c01a-4458-8b32-4479749bf594"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cd58c8fd-a214-4b7f-a383-cf55de1ce453"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("10668196-d9f8-4c21-8d25-c2345e71e236"), "9aa9c8b8-e952-4131-a4a1-9f6a4f1561fc", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("776fcedf-a9e1-4690-80d6-ec04210ed160"), "8d300059-8693-403d-8877-9cc279740c9c", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("10668196-d9f8-4c21-8d25-c2345e71e236"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("776fcedf-a9e1-4690-80d6-ec04210ed160"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("871a3f86-c01a-4458-8b32-4479749bf594"), "73835938-7443-444a-8802-c0abfe018165", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("cd58c8fd-a214-4b7f-a383-cf55de1ce453"), "d08921b9-4136-4795-b83d-9aa58ee0a3cc", "Admin", "ADMIN" });
        }
    }
}
