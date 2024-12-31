using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAIRDRESSER2.Migrations
{
    /// <inheritdoc />
    public partial class Seedkas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UzmanlikAlanlari",
                columns: new[] { "Id", "Ad" },
                values: new object[] { 6, "Kaş" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UzmanlikAlanlari",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
