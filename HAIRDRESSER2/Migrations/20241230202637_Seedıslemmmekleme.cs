using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HAIRDRESSER2.Migrations
{
    /// <inheritdoc />
    public partial class Seedıslemmmekleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 27);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Islemler",
                columns: new[] { "Id", "Ad", "Fiyat", "Sure", "UzmanId", "UzmanlikAlaniId" },
                values: new object[,]
                {
                    { 26, "Lazer Epilasyon - Tüm Vücut", 2000m, 120, null, 5 },
                    { 27, "Lazer Cilt Yenileme", 1500m, 90, null, 5 }
                });
        }
    }
}
