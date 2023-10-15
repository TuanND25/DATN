using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "WardCode",
                table: "AddressShips");

            migrationBuilder.RenameColumn(
                name: "ProvinceID",
                table: "AddressShips",
                newName: "WardName");

            migrationBuilder.RenameColumn(
                name: "DistrictID",
                table: "AddressShips",
                newName: "Province");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "AddressShips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("11ea7e8c-a162-4882-b00c-58de42f3e3fe"), "8b94e2e7-2bdb-4292-ac20-f7c2f33ee7f2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("b90523d9-1abd-4140-a1a2-97c409429ae6"), "68338d6f-33e9-4c07-9aef-36aec9bd5c47", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11ea7e8c-a162-4882-b00c-58de42f3e3fe"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b90523d9-1abd-4140-a1a2-97c409429ae6"));

            migrationBuilder.DropColumn(
                name: "District",
                table: "AddressShips");

            migrationBuilder.RenameColumn(
                name: "WardName",
                table: "AddressShips",
                newName: "ProvinceID");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "AddressShips",
                newName: "DistrictID");

            migrationBuilder.AddColumn<int>(
                name: "WardCode",
                table: "AddressShips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("04f9bf49-e093-47ca-9a97-3572359f2c07"), "6332a9ab-e297-40c4-9ee4-6a622cbe2ed9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("eb1fd2de-ae3b-487a-bab2-b377bda9ae69"), "151d1b69-d649-403d-8e05-0afbbc26e9ab", "User", "USER" });
        }
    }
}
