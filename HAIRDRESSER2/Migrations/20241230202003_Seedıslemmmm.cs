using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAIRDRESSER2.Migrations
{
    /// <inheritdoc />
    public partial class Seedıslemmmm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Islemler_Uzmanlar_UzmanId",
                table: "Islemler");

            migrationBuilder.DropForeignKey(
                name: "FK_Islemler_UzmanlikAlanlari_UzmanlikAlaniId",
                table: "Islemler");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Islemler_IslemId",
                table: "Randevular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Islemler",
                table: "Islemler");

            migrationBuilder.RenameTable(
                name: "Islemler",
                newName: "Islemlerr");

            migrationBuilder.RenameIndex(
                name: "IX_Islemler_UzmanlikAlaniId",
                table: "Islemlerr",
                newName: "IX_Islemlerr_UzmanlikAlaniId");

            migrationBuilder.RenameIndex(
                name: "IX_Islemler_UzmanId",
                table: "Islemlerr",
                newName: "IX_Islemlerr_UzmanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Islemlerr",
                table: "Islemlerr",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "UzmanlikAlanlari",
                keyColumn: "Id",
                keyValue: 6,
                column: "Ad",
                value: "K");

            migrationBuilder.AddForeignKey(
                name: "FK_Islemlerr_Uzmanlar_UzmanId",
                table: "Islemlerr",
                column: "UzmanId",
                principalTable: "Uzmanlar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Islemlerr_UzmanlikAlanlari_UzmanlikAlaniId",
                table: "Islemlerr",
                column: "UzmanlikAlaniId",
                principalTable: "UzmanlikAlanlari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Islemlerr_IslemId",
                table: "Randevular",
                column: "IslemId",
                principalTable: "Islemlerr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Islemlerr_Uzmanlar_UzmanId",
                table: "Islemlerr");

            migrationBuilder.DropForeignKey(
                name: "FK_Islemlerr_UzmanlikAlanlari_UzmanlikAlaniId",
                table: "Islemlerr");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Islemlerr_IslemId",
                table: "Randevular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Islemlerr",
                table: "Islemlerr");

            migrationBuilder.RenameTable(
                name: "Islemlerr",
                newName: "Islemler");

            migrationBuilder.RenameIndex(
                name: "IX_Islemlerr_UzmanlikAlaniId",
                table: "Islemler",
                newName: "IX_Islemler_UzmanlikAlaniId");

            migrationBuilder.RenameIndex(
                name: "IX_Islemlerr_UzmanId",
                table: "Islemler",
                newName: "IX_Islemler_UzmanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Islemler",
                table: "Islemler",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "UzmanlikAlanlari",
                keyColumn: "Id",
                keyValue: 6,
                column: "Ad",
                value: "Kaş");

            migrationBuilder.AddForeignKey(
                name: "FK_Islemler_Uzmanlar_UzmanId",
                table: "Islemler",
                column: "UzmanId",
                principalTable: "Uzmanlar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Islemler_UzmanlikAlanlari_UzmanlikAlaniId",
                table: "Islemler",
                column: "UzmanlikAlaniId",
                principalTable: "UzmanlikAlanlari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Islemler_IslemId",
                table: "Randevular",
                column: "IslemId",
                principalTable: "Islemler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
