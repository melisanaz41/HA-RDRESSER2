using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAIRDRESSER2.Migrations
{
    /// <inheritdoc />
    public partial class YeniMi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UzmanlikAlanlari",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ad",
                value: "Tirnak");

            migrationBuilder.UpdateData(
                table: "UzmanlikAlanlari",
                keyColumn: "Id",
                keyValue: 4,
                column: "Ad",
                value: "Cilt Bakimi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UzmanlikAlanlari",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ad",
                value: "Tırnak");

            migrationBuilder.UpdateData(
                table: "UzmanlikAlanlari",
                keyColumn: "Id",
                keyValue: 4,
                column: "Ad",
                value: "Cilt Bakımı");
        }
    }
}
