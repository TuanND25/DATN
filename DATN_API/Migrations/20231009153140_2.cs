using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductsId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProductsId",
                table: "Reviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("02b76659-21ca-409b-bc0c-fa8f422bd16c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("45853461-5258-415d-ab47-207c519a15f6"));

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ImagesId",
                table: "ProductItems");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("493ef2c4-c651-4fa1-a046-11076487f5e6"), "ef7c1fcb-55a1-4fa1-845f-926c62cc3d3f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("4cf9a30b-3ab9-4921-aa2f-b9c3f44f16ba"), "62408949-9bf1-442b-a2d0-13d7a1b0c560", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("493ef2c4-c651-4fa1-a046-11076487f5e6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4cf9a30b-3ab9-4921-aa2f-b9c3f44f16ba"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductsId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ImagesId",
                table: "ProductItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("02b76659-21ca-409b-bc0c-fa8f422bd16c"), "be8306d0-70be-46fc-9194-3bb6ace84272", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("45853461-5258-415d-ab47-207c519a15f6"), "41c4c682-2a3c-4136-b922-7e53d75da722", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductsId",
                table: "Reviews",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductsId",
                table: "Reviews",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
