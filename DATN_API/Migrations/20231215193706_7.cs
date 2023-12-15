using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7c75ccf4-e022-43a5-9e0e-d0b9a70f23c1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a08c7d96-07d4-45a4-a9b5-0b9e98918d1e"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("039d186b-f613-4652-91bb-fa284a2e7e33"),
                column: "ConcurrencyStamp",
                value: "7302250a-afa8-437f-a680-ae4d74371b2e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1048eea1-29e3-4139-9b25-29a4fb2562d7"), "dfc32673-67aa-432b-b439-e68d39f0cb4c", "Staff", "STAFF" },
                    { new Guid("e1813d8e-9ab3-47b6-bb17-cbb48f114d36"), "814e1c9e-1638-4047-a761-16144b51a2c8", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("76671898-a7dd-4d40-a1da-e56639a4dbe4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Status" },
                values: new object[] { "35fe7821-4344-4ea3-ad03-2b9fd1ef9da9", "AQAAAAEAACcQAAAAEEsoJt5VRJcUFi1vHPeHWUTr0ZB6v9GxSCgOeSRDbTWmVMLgNuRG4/zEVP0nwxnDkQ==", 1 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "OTP", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Sex", "Status", "TokenCreated", "TokenExpires", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"), 0, "db80694a-1be9-4856-b96a-59c7a61fef50", "user@example.com", true, false, null, "", "user@EXAMPLE.COM", "user@EXAMPLE.COM", "", "AQAAAAEAACcQAAAAENFJF4rSR9lJMRVsxbLBf3agESSOmD2KOEVGN6xk7OySW3cjW/xv+kZFPovyZQJcWw==", null, false, "", false, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "user" },
                    { new Guid("5fcc1c98-da29-4d88-b088-f921528142a2"), 0, "7f7392de-67dc-4353-8560-42ac894704b5", "nhanvien@example.com", true, false, null, "", "nhanvien@EXAMPLE.COM", "NHANVIEN@EXAMPLE.COM", "", "AQAAAAEAACcQAAAAENhlpYwB/QvP4rgCGQN6LIMV0j4rbhcsKAnJ+e9P5Yyd4pGU6aYoZAtqkL2baeQ8dA==", null, false, "", false, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "nhanvien" },
                    { new Guid("f1dded95-3dae-4568-a66e-66f47fbc4ffd"), 0, "db06b6c1-4404-415f-9f8c-554063427650", "khachvanglai@example.com", true, false, null, "", "khachvanglai@EXAMPLE.COM", "khachvanglai@EXAMPLE.COM", "", null, null, false, "", false, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "khachvanglai" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("e1813d8e-9ab3-47b6-bb17-cbb48f114d36"), new Guid("2e8676b5-daa0-4b9e-bc06-06d5b06f7078") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1048eea1-29e3-4139-9b25-29a4fb2562d7"), new Guid("5fcc1c98-da29-4d88-b088-f921528142a2") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e1813d8e-9ab3-47b6-bb17-cbb48f114d36"), new Guid("2e8676b5-daa0-4b9e-bc06-06d5b06f7078") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1048eea1-29e3-4139-9b25-29a4fb2562d7"), new Guid("5fcc1c98-da29-4d88-b088-f921528142a2") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f1dded95-3dae-4568-a66e-66f47fbc4ffd"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1048eea1-29e3-4139-9b25-29a4fb2562d7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e1813d8e-9ab3-47b6-bb17-cbb48f114d36"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5fcc1c98-da29-4d88-b088-f921528142a2"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("039d186b-f613-4652-91bb-fa284a2e7e33"),
                column: "ConcurrencyStamp",
                value: "cc052bbb-7de3-41e7-8e9d-ee092dd705a7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7c75ccf4-e022-43a5-9e0e-d0b9a70f23c1"), "779a2d61-6e0b-43af-82dc-ade29cb3ad87", "Staff", "STAFF" },
                    { new Guid("a08c7d96-07d4-45a4-a9b5-0b9e98918d1e"), "72e504b1-be5c-475c-aa6f-6ad246428b49", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("76671898-a7dd-4d40-a1da-e56639a4dbe4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Status" },
                values: new object[] { "be61ab13-5fa7-46a1-8cdc-4ebc9cb9aa7e", "AQAAAAEAACcQAAAAEGM9pjZ/rwWv83ZLZGVrZ+MlYiEthIIuwqH4pbkliQqeW0ypM0yv95qOFyAaZWzZhQ==", 0 });
        }
    }
}
