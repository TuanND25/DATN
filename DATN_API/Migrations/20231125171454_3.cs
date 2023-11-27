using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3a659ac0-897d-4347-9b8e-14740552bbea"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("515b85c6-d7d2-4ec1-91c7-7fa053402b1d"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("145413ea-e319-4008-b124-103abfffaf7f"), "0d8a5a8a-cd0b-4cc0-9b63-9f03c04ef6c5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("a31a40bc-35f2-4e02-af32-e400f81fd202"), "43a6133f-7d14-46d9-8d0d-17bfba1cb797", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("acfa5020-3be0-4532-a486-3f33493bf1c1"), "416562a0-61db-4d83-873e-2b4bc7c0ab11", "Staff", "STAFF" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("145413ea-e319-4008-b124-103abfffaf7f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a31a40bc-35f2-4e02-af32-e400f81fd202"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("acfa5020-3be0-4532-a486-3f33493bf1c1"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("3a659ac0-897d-4347-9b8e-14740552bbea"), "fa247d8e-83c4-4e14-9f85-1c865b4e09c9", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("515b85c6-d7d2-4ec1-91c7-7fa053402b1d"), "4e0b35e0-1de0-4693-9c52-1826d40db40e", "Admin", "ADMIN" });
        }
    }
}
