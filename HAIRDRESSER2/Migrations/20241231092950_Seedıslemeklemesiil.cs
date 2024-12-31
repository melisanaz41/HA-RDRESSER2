using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAIRDRESSER2.Migrations
{
    /// <inheritdoc />
    public partial class Seedıslemeklemesiil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Islemler_Uzmanlar_UzmanId",
                table: "Islemler");

            migrationBuilder.AddForeignKey(
                name: "FK_Islemler_Uzmanlar_UzmanId",
                table: "Islemler",
                column: "UzmanId",
                principalTable: "Uzmanlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Islemler_Uzmanlar_UzmanId",
                table: "Islemler");

            migrationBuilder.AddForeignKey(
                name: "FK_Islemler_Uzmanlar_UzmanId",
                table: "Islemler",
                column: "UzmanId",
                principalTable: "Uzmanlar",
                principalColumn: "Id");
        }
    }
}
