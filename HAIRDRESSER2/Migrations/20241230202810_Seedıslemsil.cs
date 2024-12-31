using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HAIRDRESSER2.Migrations
{
    /// <inheritdoc />
    public partial class Seedıslemsil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValue: 25);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Islemler",
                columns: new[] { "Id", "Ad", "Fiyat", "Sure", "UzmanId", "UzmanlikAlaniId" },
                values: new object[,]
                {
                    { 1, "Saç Kesimi", 200m, 60, null, 1 },
                    { 2, "Tüm Saç Boyama", 800m, 180, null, 1 },
                    { 3, "Ombre", 1500m, 240, null, 1 },
                    { 4, "Saç Düzleştirme", 1000m, 120, null, 1 },
                    { 5, "Saç Bakım Maskesi", 300m, 45, null, 1 },
                    { 6, "Keratin Bakımı", 1200m, 150, null, 1 },
                    { 7, "Fön", 200m, 30, null, 1 },
                    { 8, "Kahkül Kesimi", 150m, 30, null, 1 },
                    { 9, "Maşa Yapma", 300m, 45, null, 1 },
                    { 10, "Gelin Makyajı", 4000m, 120, null, 2 },
                    { 11, "Özel Gün Makyajı", 2000m, 120, null, 2 },
                    { 12, "Kalıcı Makyaj", 1500m, 120, null, 2 },
                    { 13, "Göz Makyajı", 800m, 60, null, 2 },
                    { 14, "Doğal Günlük Makyaj", 1000m, 90, null, 2 },
                    { 15, "Manikür", 150m, 45, null, 3 },
                    { 16, "Pedikür", 200m, 60, null, 3 },
                    { 17, "Jel Tırnak", 400m, 90, null, 3 },
                    { 18, "Tırnak Dekorasyonu", 500m, 90, null, 3 },
                    { 19, "Yüz Bakımı", 500m, 60, null, 4 },
                    { 20, "Anti-Aging Bakımı", 800m, 90, null, 4 },
                    { 21, "Akne Tedavisi", 700m, 75, null, 4 },
                    { 22, "Cilt Leke Tedavisi", 1200m, 120, null, 4 },
                    { 23, "Göz Çevresi Bakımı", 900m, 90, null, 4 },
                    { 24, "Lazer Epilasyon - Küçük Bölge", 300m, 30, null, 5 },
                    { 25, "Lazer Epilasyon - Büyük Bölge", 600m, 60, null, 5 }
                });
        }
    }
}
