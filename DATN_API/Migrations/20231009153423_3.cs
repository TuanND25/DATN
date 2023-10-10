using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherBills_Bills_BillId",
                table: "VoucherBills");

            migrationBuilder.DropIndex(
                name: "IX_VoucherBills_BillId",
                table: "VoucherBills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("493ef2c4-c651-4fa1-a046-11076487f5e6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4cf9a30b-3ab9-4921-aa2f-b9c3f44f16ba"));

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "VoucherBills");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("60027d90-0ef0-436b-bb33-d3642218d06f"), "27859a6d-ea3c-479a-bd72-32185ca52e41", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("60a894c1-288d-48c4-b9c2-5df292a929a1"), "66c872b5-d71f-4bd8-b3af-69558d4b6bce", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("60027d90-0ef0-436b-bb33-d3642218d06f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("60a894c1-288d-48c4-b9c2-5df292a929a1"));

            migrationBuilder.AddColumn<Guid>(
                name: "BillId",
                table: "VoucherBills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("493ef2c4-c651-4fa1-a046-11076487f5e6"), "ef7c1fcb-55a1-4fa1-845f-926c62cc3d3f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("4cf9a30b-3ab9-4921-aa2f-b9c3f44f16ba"), "62408949-9bf1-442b-a2d0-13d7a1b0c560", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherBills_BillId",
                table: "VoucherBills",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherBills_Bills_BillId",
                table: "VoucherBills",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");
        }
    }
}
