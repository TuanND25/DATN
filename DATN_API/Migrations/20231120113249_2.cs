using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7b219729-fffe-4d24-bc87-53215edb3254"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a48afc7a-b90b-44e1-811d-503f702cc3b2"));

            migrationBuilder.RenameColumn(
                name: "PurchasePrice",
                table: "ProductItems",
                newName: "PriceAfterReduction");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("6c679a1c-d8dc-4c51-9f49-2bf2b37eddcc"), "cea7f11e-8193-4cf6-9485-091d85794398", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("8c5f72ab-c714-48f5-87c3-cb0b12de76d2"), "f0689c0d-7dcc-4c8e-993a-99e6ccb966e9", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6c679a1c-d8dc-4c51-9f49-2bf2b37eddcc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8c5f72ab-c714-48f5-87c3-cb0b12de76d2"));

            migrationBuilder.RenameColumn(
                name: "PriceAfterReduction",
                table: "ProductItems",
                newName: "PurchasePrice");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("7b219729-fffe-4d24-bc87-53215edb3254"), "108e36c6-c107-47c4-8ba6-b440eb00bfc1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("a48afc7a-b90b-44e1-811d-503f702cc3b2"), "445d330d-60bd-4ffc-8ce3-2ff7d226040f", "User", "USER" });
        }
    }
}
