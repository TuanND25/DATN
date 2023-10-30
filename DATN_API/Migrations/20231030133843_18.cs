using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_HistoryConsumerPoints_HistoryConsumerPointID",
                table: "Bills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0f4e6b25-7f30-4635-b7fc-5c12fc866106"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9fcb5e7c-ee74-4630-b0a8-45bb0535933e"));

            migrationBuilder.AlterColumn<int>(
                name: "ShippingFee",
                table: "Bills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HistoryConsumerPointID",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "FinalAmount",
                table: "Bills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("8739c0e1-6d65-4075-9aef-d37f961cbdd6"), "c125c8f4-2d83-462b-bc1c-c0b35958cf23", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("fc37170d-2f35-40bf-bebd-93064eda8d73"), "6ba203b6-ee27-407a-ad1d-90b9e2b3587d", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_HistoryConsumerPoints_HistoryConsumerPointID",
                table: "Bills",
                column: "HistoryConsumerPointID",
                principalTable: "HistoryConsumerPoints",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_HistoryConsumerPoints_HistoryConsumerPointID",
                table: "Bills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8739c0e1-6d65-4075-9aef-d37f961cbdd6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fc37170d-2f35-40bf-bebd-93064eda8d73"));

            migrationBuilder.AlterColumn<int>(
                name: "ShippingFee",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "HistoryConsumerPointID",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FinalAmount",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("0f4e6b25-7f30-4635-b7fc-5c12fc866106"), "4ff3d14f-9e9d-4ea4-a63c-40c7a15f382d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("9fcb5e7c-ee74-4630-b0a8-45bb0535933e"), "5edc1ded-ec65-4bc6-a5ef-d97af19a0a66", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_HistoryConsumerPoints_HistoryConsumerPointID",
                table: "Bills",
                column: "HistoryConsumerPointID",
                principalTable: "HistoryConsumerPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
