using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Vouchers_VoucherId",
                table: "Bills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("357a3e1f-0748-4f5d-a345-ad200a1db26f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b69f5bf3-90e5-49f5-b1c9-0371de3036ef"));

            migrationBuilder.AlterColumn<Guid>(
                name: "VoucherId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("37c9e51f-cbce-4b0b-a3e8-fc74ce64a003"), "5e37fc2d-84a0-432b-9630-09de5acdb622", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5126938f-24ce-4029-958e-32c592eb2f45"), "0554c88a-0492-4a5a-84e5-855d41b7107a", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Vouchers_VoucherId",
                table: "Bills",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Vouchers_VoucherId",
                table: "Bills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("37c9e51f-cbce-4b0b-a3e8-fc74ce64a003"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5126938f-24ce-4029-958e-32c592eb2f45"));

            migrationBuilder.AlterColumn<Guid>(
                name: "VoucherId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("357a3e1f-0748-4f5d-a345-ad200a1db26f"), "c4518079-f71b-4f32-a273-6679856f1da2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("b69f5bf3-90e5-49f5-b1c9-0371de3036ef"), "727843ab-2f08-43e2-a31f-ffbbfebda252", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Vouchers_VoucherId",
                table: "Bills",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
