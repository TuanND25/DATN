using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("039d186b-f613-4652-91bb-fa284a2e7e33"),
                column: "ConcurrencyStamp",
                value: "2bda5f12-174f-4635-b86b-5702bcd4f3b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1048eea1-29e3-4139-9b25-29a4fb2562d7"),
                column: "ConcurrencyStamp",
                value: "353319ff-d1e9-4e82-9ffd-8a60ba3b6cfa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e1813d8e-9ab3-47b6-bb17-cbb48f114d36"),
                column: "ConcurrencyStamp",
                value: "84325c41-0140-4199-9c3f-68f861a92154");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8738edd1-3a68-4f13-a29c-730e06402d77", "AQAAAAEAACcQAAAAEJHFEl0Aw3jI9i6NdbnUrNq9cE9JobpI8EiY7M4/AC+SlvEXASUhxRfOsl7XTiAfkg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5fcc1c98-da29-4d88-b088-f921528142a2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d793df21-0d9b-4b4a-8e73-2a47b285140f", "AQAAAAEAACcQAAAAEJNGkfbPpbb6Yryuk70+W6PUXBuL5AFyLhQPGsaaWH2KhllpxtxcIYC2lKblmwU64g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("76671898-a7dd-4d40-a1da-e56639a4dbe4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0b223fa4-41dd-4b4f-87a7-bbebf700c881", "AQAAAAEAACcQAAAAEN+1NkgtRb4402RYcKBLcqPEttNGy3UDwqGFRiuSaG1bwbXlyrNjVyo83tnQ5FcmNg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f1dded95-3dae-4568-a66e-66f47fbc4ffd"),
                column: "ConcurrencyStamp",
                value: "47b13c04-e8f6-42e6-a0b6-82363451b3eb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Bills");

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
        }
    }
}
