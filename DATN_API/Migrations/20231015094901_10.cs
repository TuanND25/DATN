using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("36fc8f31-c1d4-46fd-ac34-5bffb8773812"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("42d67248-2cf9-449c-a0e7-14a3c4b28887"));

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("04f9bf49-e093-47ca-9a97-3572359f2c07"), "6332a9ab-e297-40c4-9ee4-6a622cbe2ed9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("eb1fd2de-ae3b-487a-bab2-b377bda9ae69"), "151d1b69-d649-403d-8e05-0afbbc26e9ab", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("04f9bf49-e093-47ca-9a97-3572359f2c07"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eb1fd2de-ae3b-487a-bab2-b377bda9ae69"));

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Bills");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("36fc8f31-c1d4-46fd-ac34-5bffb8773812"), "2950e60f-8260-4511-8124-730711e3a221", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("42d67248-2cf9-449c-a0e7-14a3c4b28887"), "516b1921-65a9-47f6-aa25-a017ab2f6c8e", "User", "USER" });
        }
    }
}
