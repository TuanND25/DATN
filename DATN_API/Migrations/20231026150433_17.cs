using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("303973ee-f86b-4d0d-ba69-13bf9f681fb5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8fe48042-32a6-4d96-8dc5-490371fbe942"));

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartItems");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("0f4e6b25-7f30-4635-b7fc-5c12fc866106"), "4ff3d14f-9e9d-4ea4-a63c-40c7a15f382d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("9fcb5e7c-ee74-4630-b0a8-45bb0535933e"), "5edc1ded-ec65-4bc6-a5ef-d97af19a0a66", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0f4e6b25-7f30-4635-b7fc-5c12fc866106"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9fcb5e7c-ee74-4630-b0a8-45bb0535933e"));

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("303973ee-f86b-4d0d-ba69-13bf9f681fb5"), "725441a3-464f-437f-b21b-efe9f084edda", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("8fe48042-32a6-4d96-8dc5-490371fbe942"), "acc38b80-e7b1-4065-adba-01f5929a92bf", "Admin", "ADMIN" });
        }
    }
}
