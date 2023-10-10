using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherBills_AspNetUsers_UserId",
                table: "VoucherBills");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherBills_Vouchers_VoucherId",
                table: "VoucherBills");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherBills",
                table: "VoucherBills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("60027d90-0ef0-436b-bb33-d3642218d06f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("60a894c1-288d-48c4-b9c2-5df292a929a1"));

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "VoucherBills",
                newName: "VoucherUsers");

            migrationBuilder.RenameIndex(
                name: "IX_VoucherBills_VoucherId",
                table: "VoucherUsers",
                newName: "IX_VoucherUsers_VoucherId");

            migrationBuilder.RenameIndex(
                name: "IX_VoucherBills_UserId",
                table: "VoucherUsers",
                newName: "IX_VoucherUsers_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "ProductItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherUsers",
                table: "VoucherUsers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("871a3f86-c01a-4458-8b32-4479749bf594"), "73835938-7443-444a-8802-c0abfe018165", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("cd58c8fd-a214-4b7f-a383-cf55de1ce453"), "d08921b9-4136-4795-b83d-9aa58ee0a3cc", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_CategoryId",
                table: "ProductItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_Categories_CategoryId",
                table: "ProductItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsers_AspNetUsers_UserId",
                table: "VoucherUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsers_Vouchers_VoucherId",
                table: "VoucherUsers",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_Categories_CategoryId",
                table: "ProductItems");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsers_AspNetUsers_UserId",
                table: "VoucherUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsers_Vouchers_VoucherId",
                table: "VoucherUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_CategoryId",
                table: "ProductItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherUsers",
                table: "VoucherUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("871a3f86-c01a-4458-8b32-4479749bf594"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cd58c8fd-a214-4b7f-a383-cf55de1ce453"));

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductItems");

            migrationBuilder.RenameTable(
                name: "VoucherUsers",
                newName: "VoucherBills");

            migrationBuilder.RenameIndex(
                name: "IX_VoucherUsers_VoucherId",
                table: "VoucherBills",
                newName: "IX_VoucherBills_VoucherId");

            migrationBuilder.RenameIndex(
                name: "IX_VoucherUsers_UserId",
                table: "VoucherBills",
                newName: "IX_VoucherBills_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherBills",
                table: "VoucherBills",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("60027d90-0ef0-436b-bb33-d3642218d06f"), "27859a6d-ea3c-479a-bd72-32185ca52e41", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("60a894c1-288d-48c4-b9c2-5df292a929a1"), "66c872b5-d71f-4bd8-b3af-69558d4b6bce", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherBills_AspNetUsers_UserId",
                table: "VoucherBills",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherBills_Vouchers_VoucherId",
                table: "VoucherBills",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
