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
                name: "FK_Bills_HistoryConsumerPoints_HistoryConsumerPointID",
                table: "Bills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8f088514-9ee9-4cb9-9d00-8a8e3684bc51"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ebfceab9-3057-4d66-a621-43dfe839ab00"));

            migrationBuilder.RenameColumn(
                name: "HistoryConsumerPointID",
                table: "Bills",
                newName: "HistoryConsumerPointId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_HistoryConsumerPointID",
                table: "Bills",
                newName: "IX_Bills_HistoryConsumerPointId");

            migrationBuilder.AddColumn<Guid>(
                name: "BillId",
                table: "HistoryConsumerPoints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("3a659ac0-897d-4347-9b8e-14740552bbea"), "fa247d8e-83c4-4e14-9f85-1c865b4e09c9", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("515b85c6-d7d2-4ec1-91c7-7fa053402b1d"), "4e0b35e0-1de0-4693-9c52-1826d40db40e", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryConsumerPoints_BillId",
                table: "HistoryConsumerPoints",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_HistoryConsumerPoints_HistoryConsumerPointId",
                table: "Bills",
                column: "HistoryConsumerPointId",
                principalTable: "HistoryConsumerPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryConsumerPoints_Bills_BillId",
                table: "HistoryConsumerPoints",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_HistoryConsumerPoints_HistoryConsumerPointId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryConsumerPoints_Bills_BillId",
                table: "HistoryConsumerPoints");

            migrationBuilder.DropIndex(
                name: "IX_HistoryConsumerPoints_BillId",
                table: "HistoryConsumerPoints");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3a659ac0-897d-4347-9b8e-14740552bbea"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("515b85c6-d7d2-4ec1-91c7-7fa053402b1d"));

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "HistoryConsumerPoints");

            migrationBuilder.RenameColumn(
                name: "HistoryConsumerPointId",
                table: "Bills",
                newName: "HistoryConsumerPointID");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_HistoryConsumerPointId",
                table: "Bills",
                newName: "IX_Bills_HistoryConsumerPointID");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("8f088514-9ee9-4cb9-9d00-8a8e3684bc51"), "c0d7f809-cbb1-4ab6-a0d7-912ca98ba52b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("ebfceab9-3057-4d66-a621-43dfe839ab00"), "93f45ade-43ff-4714-b49a-010489330f7e", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_HistoryConsumerPoints_HistoryConsumerPointID",
                table: "Bills",
                column: "HistoryConsumerPointID",
                principalTable: "HistoryConsumerPoints",
                principalColumn: "Id");
        }
    }
}
