using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6c679a1c-d8dc-4c51-9f49-2bf2b37eddcc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8c5f72ab-c714-48f5-87c3-cb0b12de76d2"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("b40bcbe4-35ba-49ed-b850-c4f749ccb37d"), "b9b446eb-a731-4ce5-a7db-eae74867c854", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("dba9c5c7-441f-4098-b731-1215ace9b0ad"), "a29780bc-0b21-4aff-8428-d59948581620", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b40bcbe4-35ba-49ed-b850-c4f749ccb37d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dba9c5c7-441f-4098-b731-1215ace9b0ad"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("6c679a1c-d8dc-4c51-9f49-2bf2b37eddcc"), "cea7f11e-8193-4cf6-9485-091d85794398", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("8c5f72ab-c714-48f5-87c3-cb0b12de76d2"), "f0689c0d-7dcc-4c8e-993a-99e6ccb966e9", "Admin", "ADMIN" });
        }
    }
}
