using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HAIRDRESSER2.Migrations
{
    /// <inheritdoc />
    public partial class AddUzmanlarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalismaSaatleri_Uzmanlar_UzmanId",
                table: "CalismaSaatleri");

            migrationBuilder.DropColumn(
                name: "Gun",
                table: "CalismaSaatleri");

            migrationBuilder.AlterColumn<int>(
                name: "UzmanId",
                table: "CalismaSaatleri",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "CalismaSaatleri",
                columns: new[] { "Id", "BaslangicSaati", "BitisSaati", "UzmanId" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 13, 0, 0, 0), null },
                    { 2, new TimeSpan(0, 13, 0, 0, 0), new TimeSpan(0, 20, 0, 0, 0), null },
                    { 3, new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 20, 0, 0, 0), null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CalismaSaatleri_Uzmanlar_UzmanId",
                table: "CalismaSaatleri",
                column: "UzmanId",
                principalTable: "Uzmanlar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalismaSaatleri_Uzmanlar_UzmanId",
                table: "CalismaSaatleri");

            migrationBuilder.DeleteData(
                table: "CalismaSaatleri",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CalismaSaatleri",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CalismaSaatleri",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "UzmanId",
                table: "CalismaSaatleri",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gun",
                table: "CalismaSaatleri",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CalismaSaatleri_Uzmanlar_UzmanId",
                table: "CalismaSaatleri",
                column: "UzmanId",
                principalTable: "Uzmanlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
