using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("42327f7f-4673-4cfb-80e0-480743e45d3a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("75eb4888-2f04-4015-b985-b7fdabacbe10"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fd350b4f-90cf-4a42-ae1f-28a7ad340203"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5e969bc9-05d8-4685-9ea3-7e341bf9dfc5"), "9628ef5c-755c-4cec-949d-7544f49c194f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d1e642db-e277-4a38-ac12-9220dcdafa77"), "8f101a09-017b-4ae7-a3d1-a6ad528a1d58", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("fc0c8c93-9bed-46f3-8c72-23084477c6de"), "b628e796-a47a-4b49-8f31-00a9b3320360", "Staff", "STAFF" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5e969bc9-05d8-4685-9ea3-7e341bf9dfc5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d1e642db-e277-4a38-ac12-9220dcdafa77"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fc0c8c93-9bed-46f3-8c72-23084477c6de"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("42327f7f-4673-4cfb-80e0-480743e45d3a"), "20788ab1-2f5d-4462-bff2-99e73ffcc722", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("75eb4888-2f04-4015-b985-b7fdabacbe10"), "0b055a5a-2d7a-4b09-aec0-a9d04e1219b6", "Staff", "STAFF" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("fd350b4f-90cf-4a42-ae1f-28a7ad340203"), "ba0da5d4-d937-48b2-8684-27efa15220b9", "User", "USER" });
        }
    }
}
