using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HAIRDRESSER2.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Branş",
                table: "Uzmanlar",
                newName: "Telefon");

            migrationBuilder.AddColumn<DateTime>(
                name: "EklenmeTarihi",
                table: "Uzmanlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UzmanlikAlaniId",
                table: "Uzmanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UzmanlikAlanlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UzmanlikAlanlari", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UzmanlikAlanlari",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Saç" },
                    { 2, "Makyaj" },
                    { 3, "Tırnak" },
                    { 4, "Cilt Bakımı" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uzmanlar_UzmanlikAlaniId",
                table: "Uzmanlar",
                column: "UzmanlikAlaniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uzmanlar_UzmanlikAlanlari_UzmanlikAlaniId",
                table: "Uzmanlar",
                column: "UzmanlikAlaniId",
                principalTable: "UzmanlikAlanlari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uzmanlar_UzmanlikAlanlari_UzmanlikAlaniId",
                table: "Uzmanlar");

            migrationBuilder.DropTable(
                name: "UzmanlikAlanlari");

            migrationBuilder.DropIndex(
                name: "IX_Uzmanlar_UzmanlikAlaniId",
                table: "Uzmanlar");

            migrationBuilder.DropColumn(
                name: "EklenmeTarihi",
                table: "Uzmanlar");

            migrationBuilder.DropColumn(
                name: "UzmanlikAlaniId",
                table: "Uzmanlar");

            migrationBuilder.RenameColumn(
                name: "Telefon",
                table: "Uzmanlar",
                newName: "Branş");
        }
    }
}
