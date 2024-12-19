using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAIRDRESSER2.Migrations
{
    /// <inheritdoc />
    public partial class YeniMigrationAdi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UzmanlikAlanlari",
                columns: new[] { "Id", "Ad" },
                values: new object[] { 5, "Lazer" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UzmanlikAlanlari",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
