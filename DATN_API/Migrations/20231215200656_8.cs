using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("039d186b-f613-4652-91bb-fa284a2e7e33"),
                column: "ConcurrencyStamp",
                value: "0c34decc-24fa-4dcf-a6e6-d312796ded05");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1048eea1-29e3-4139-9b25-29a4fb2562d7"),
                column: "ConcurrencyStamp",
                value: "3939c5f8-acfc-4963-88b8-a970821e464b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e1813d8e-9ab3-47b6-bb17-cbb48f114d36"),
                column: "ConcurrencyStamp",
                value: "482900a8-6683-4076-b028-63ef2fbe66da");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ac306cf3-e94c-4198-8a04-21bfc3885ac5", "AQAAAAEAACcQAAAAENhjz8HDPULVEG/KOunh7AqtuoyfudbuWAZ9Cc4gBsePDu/fmAJV82vN2XsTVGsY6g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5fcc1c98-da29-4d88-b088-f921528142a2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9fc326d4-ab24-4812-8b0f-d4439a37c5e8", "AQAAAAEAACcQAAAAEMi2V70tCND0ZyOsocEyTM9RSvjdZxVTOTfhZUbdZK00+9kl7fY2T+3NdP7gt0pziQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("76671898-a7dd-4d40-a1da-e56639a4dbe4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9384faf2-56ac-4cdb-aa20-dea016737184", "AQAAAAEAACcQAAAAEAaleE0YRJVGUZ/3TOji92XjW4EP6T6ek2RohfcLWS69Mas3NnoM8FeijwNczVd6DQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f1dded95-3dae-4568-a66e-66f47fbc4ffd"),
                column: "ConcurrencyStamp",
                value: "19bda709-2181-4c6d-b69f-0ecaf6b18df4");

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "UserId", "Description", "Status" },
                values: new object[,]
                {
                    { new Guid("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"), "", 1 },
                    { new Guid("f1dded95-3dae-4568-a66e-66f47fbc4ffd"), "", 1 }
                });

            migrationBuilder.InsertData(
                table: "ConsumerPoints",
                columns: new[] { "UserID", "Point", "Status" },
                values: new object[,]
                {
                    { new Guid("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"), "0", 1 },
                    { new Guid("f1dded95-3dae-4568-a66e-66f47fbc4ffd"), "0", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "UserId",
                keyValue: new Guid("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"));

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "UserId",
                keyValue: new Guid("f1dded95-3dae-4568-a66e-66f47fbc4ffd"));

            migrationBuilder.DeleteData(
                table: "ConsumerPoints",
                keyColumn: "UserID",
                keyValue: new Guid("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"));

            migrationBuilder.DeleteData(
                table: "ConsumerPoints",
                keyColumn: "UserID",
                keyValue: new Guid("f1dded95-3dae-4568-a66e-66f47fbc4ffd"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("039d186b-f613-4652-91bb-fa284a2e7e33"),
                column: "ConcurrencyStamp",
                value: "7302250a-afa8-437f-a680-ae4d74371b2e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1048eea1-29e3-4139-9b25-29a4fb2562d7"),
                column: "ConcurrencyStamp",
                value: "dfc32673-67aa-432b-b439-e68d39f0cb4c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e1813d8e-9ab3-47b6-bb17-cbb48f114d36"),
                column: "ConcurrencyStamp",
                value: "814e1c9e-1638-4047-a761-16144b51a2c8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "db80694a-1be9-4856-b96a-59c7a61fef50", "AQAAAAEAACcQAAAAENFJF4rSR9lJMRVsxbLBf3agESSOmD2KOEVGN6xk7OySW3cjW/xv+kZFPovyZQJcWw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5fcc1c98-da29-4d88-b088-f921528142a2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7f7392de-67dc-4353-8560-42ac894704b5", "AQAAAAEAACcQAAAAENhlpYwB/QvP4rgCGQN6LIMV0j4rbhcsKAnJ+e9P5Yyd4pGU6aYoZAtqkL2baeQ8dA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("76671898-a7dd-4d40-a1da-e56639a4dbe4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "35fe7821-4344-4ea3-ad03-2b9fd1ef9da9", "AQAAAAEAACcQAAAAEEsoJt5VRJcUFi1vHPeHWUTr0ZB6v9GxSCgOeSRDbTWmVMLgNuRG4/zEVP0nwxnDkQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f1dded95-3dae-4568-a66e-66f47fbc4ffd"),
                column: "ConcurrencyStamp",
                value: "db06b6c1-4404-415f-9f8c-554063427650");
        }
    }
}
