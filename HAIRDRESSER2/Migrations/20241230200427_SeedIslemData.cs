using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAIRDRESSER2.Migrations
{
    public partial class SeedIslemData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Burada seed verisi ekliyorsunuz
            migrationBuilder.InsertData(
                table: "Islemler",
                columns: new[] { "Id", "Ad", "Fiyat", "UzmanlikAlaniId", "Sure" },
                values: new object[,]
                {
                    { 1, "Saç Kesimi", 200m, 1, 60 },
                    { 2, "Tüm Saç Boyama", 800m, 1, 180 },
                    { 3, "Ombre", 1500m, 1, 240 },
                    { 4, "Saç Düzleştirme", 1000m, 1, 120 },
                    { 5, "Saç Bakım Maskesi", 300m, 1, 45 },
                    { 6, "Keratin Bakımı", 1200m, 1, 150 },
                    { 7, "Fön", 200m, 1, 30 },
                    { 8, "Kahkül Kesimi", 150m, 1, 30 },
                    { 9, "Maşa Yapma", 300m, 1, 45 },
                    { 10, "Gelin Makyajı", 4000m, 2, 120 }
                    // Diğer verileri de ekleyebilirsiniz
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Verileri geri almak için Down metodunu yazabilirsiniz
            migrationBuilder.DeleteData(
                table: "Islemler",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        }
    }
}
