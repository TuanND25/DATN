using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class addadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5a555dc2-5bef-4dbb-a290-107c7f323c22"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7dd576ab-b6b6-4ccb-9926-4bcdb035b626"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c5fc3e01-4908-46e6-abd1-e7ee42d51c6c"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("039d186b-f613-4652-91bb-fa284a2e7e33"), "cc052bbb-7de3-41e7-8e9d-ee092dd705a7", "Admin", "ADMIN" },
                    { new Guid("7c75ccf4-e022-43a5-9e0e-d0b9a70f23c1"), "779a2d61-6e0b-43af-82dc-ade29cb3ad87", "Staff", "STAFF" },
                    { new Guid("a08c7d96-07d4-45a4-a9b5-0b9e98918d1e"), "72e504b1-be5c-475c-aa6f-6ad246428b49", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "OTP", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Sex", "Status", "TokenCreated", "TokenExpires", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("76671898-a7dd-4d40-a1da-e56639a4dbe4"), 0, "be61ab13-5fa7-46a1-8cdc-4ebc9cb9aa7e", "admin@example.com", true, false, null, "", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "", "AQAAAAEAACcQAAAAEGM9pjZ/rwWv83ZLZGVrZ+MlYiEthIIuwqH4pbkliQqeW0ypM0yv95qOFyAaZWzZhQ==", null, false, "", false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("039d186b-f613-4652-91bb-fa284a2e7e33"), new Guid("76671898-a7dd-4d40-a1da-e56639a4dbe4") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7c75ccf4-e022-43a5-9e0e-d0b9a70f23c1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a08c7d96-07d4-45a4-a9b5-0b9e98918d1e"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("039d186b-f613-4652-91bb-fa284a2e7e33"), new Guid("76671898-a7dd-4d40-a1da-e56639a4dbe4") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("039d186b-f613-4652-91bb-fa284a2e7e33"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("76671898-a7dd-4d40-a1da-e56639a4dbe4"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5a555dc2-5bef-4dbb-a290-107c7f323c22"), "bc62073d-0f16-41aa-b926-7dcc0d080a12", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("7dd576ab-b6b6-4ccb-9926-4bcdb035b626"), "68af33ca-0e7b-42e2-9719-3b6a4cecea15", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("c5fc3e01-4908-46e6-abd1-e7ee42d51c6c"), "708cae63-2122-47b8-9d39-379f895b4493", "Staff", "STAFF" });
        }
    }
}
