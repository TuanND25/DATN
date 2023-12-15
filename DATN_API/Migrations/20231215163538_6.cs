using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryConsumerPoints_Formulas_FormulaId",
                table: "HistoryConsumerPoints");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "FormulaId",
                table: "HistoryConsumerPoints",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryConsumerPoints_Formulas_FormulaId",
                table: "HistoryConsumerPoints",
                column: "FormulaId",
                principalTable: "Formulas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryConsumerPoints_Formulas_FormulaId",
                table: "HistoryConsumerPoints");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "FormulaId",
                table: "HistoryConsumerPoints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryConsumerPoints_Formulas_FormulaId",
                table: "HistoryConsumerPoints",
                column: "FormulaId",
                principalTable: "Formulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
