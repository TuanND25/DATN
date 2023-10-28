using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionsProducts_ProductItems_ProductItemsId",
                table: "PromotionsProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionsProducts_Promotions_PromotionsId",
                table: "PromotionsProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromotionsProducts",
                table: "PromotionsProducts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2b5ee318-61e5-4658-ae62-68a1426d88ea"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eab9a92b-d222-44e9-9a8a-68962843cfaa"));

            migrationBuilder.RenameTable(
                name: "PromotionsProducts",
                newName: "PromotionsItem");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionsProducts_PromotionsId",
                table: "PromotionsItem",
                newName: "IX_PromotionsItem_PromotionsId");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionsProducts_ProductItemsId",
                table: "PromotionsItem",
                newName: "IX_PromotionsItem_ProductItemsId");

            migrationBuilder.AlterColumn<int>(
                name: "Percent",
                table: "Promotions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromotionsItem",
                table: "PromotionsItem",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("303973ee-f86b-4d0d-ba69-13bf9f681fb5"), "725441a3-464f-437f-b21b-efe9f084edda", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("8fe48042-32a6-4d96-8dc5-490371fbe942"), "acc38b80-e7b1-4065-adba-01f5929a92bf", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionsItem_ProductItems_ProductItemsId",
                table: "PromotionsItem",
                column: "ProductItemsId",
                principalTable: "ProductItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionsItem_Promotions_PromotionsId",
                table: "PromotionsItem",
                column: "PromotionsId",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionsItem_ProductItems_ProductItemsId",
                table: "PromotionsItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionsItem_Promotions_PromotionsId",
                table: "PromotionsItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromotionsItem",
                table: "PromotionsItem");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("303973ee-f86b-4d0d-ba69-13bf9f681fb5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8fe48042-32a6-4d96-8dc5-490371fbe942"));

            migrationBuilder.RenameTable(
                name: "PromotionsItem",
                newName: "PromotionsProducts");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionsItem_PromotionsId",
                table: "PromotionsProducts",
                newName: "IX_PromotionsProducts_PromotionsId");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionsItem_ProductItemsId",
                table: "PromotionsProducts",
                newName: "IX_PromotionsProducts_ProductItemsId");

            migrationBuilder.AlterColumn<string>(
                name: "Percent",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromotionsProducts",
                table: "PromotionsProducts",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("2b5ee318-61e5-4658-ae62-68a1426d88ea"), "77cf1d3b-5033-4f4f-888d-cb7f9ba01a60", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("eab9a92b-d222-44e9-9a8a-68962843cfaa"), "63be7198-601a-49f5-8d8e-3149520e36c5", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionsProducts_ProductItems_ProductItemsId",
                table: "PromotionsProducts",
                column: "ProductItemsId",
                principalTable: "ProductItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionsProducts_Promotions_PromotionsId",
                table: "PromotionsProducts",
                column: "PromotionsId",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
