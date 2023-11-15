using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_PaymentMethods_PaymentMethodId",
                table: "Bills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3eecae25-283b-40d2-9ecd-6209178bbaf6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a75f67b8-014c-4c9e-bd3e-4e8100e21188"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentMethodId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("cb84a0c4-e9e9-4abb-b7cf-6ffd9e6c259f"), "73f4f4d2-e5df-4015-99a5-4d6b88770d58", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("f97f4bf2-9d5d-49b0-80d8-46c736c6172c"), "f023cb54-f4aa-4512-a9ed-bae92fba89d8", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_PaymentMethods_PaymentMethodId",
                table: "Bills",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_PaymentMethods_PaymentMethodId",
                table: "Bills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cb84a0c4-e9e9-4abb-b7cf-6ffd9e6c259f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f97f4bf2-9d5d-49b0-80d8-46c736c6172c"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentMethodId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("3eecae25-283b-40d2-9ecd-6209178bbaf6"), "9178a81c-c2be-42b0-8016-ac970dca02cc", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("a75f67b8-014c-4c9e-bd3e-4e8100e21188"), "8739c2b7-cb2e-43fd-8976-0c153009c7cd", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_PaymentMethods_PaymentMethodId",
                table: "Bills",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
