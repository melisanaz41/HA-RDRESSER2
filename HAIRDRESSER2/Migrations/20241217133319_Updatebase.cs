using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAIRDRESSER2.Migrations
{
    /// <inheritdoc />
    public partial class Updatebase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalismaSaatleri_Uzmanlar_UzmanId",
                table: "CalismaSaatleri");

            migrationBuilder.DropIndex(
                name: "IX_CalismaSaatleri_UzmanId",
                table: "CalismaSaatleri");

            migrationBuilder.DropColumn(
                name: "UzmanId",
                table: "CalismaSaatleri");

            migrationBuilder.AddColumn<int>(
                name: "calismaSaatleri",
                table: "Uzmanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "calismaSaatleriIdId",
                table: "Uzmanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Uzmanlar_calismaSaatleriIdId",
                table: "Uzmanlar",
                column: "calismaSaatleriIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uzmanlar_CalismaSaatleri_calismaSaatleriIdId",
                table: "Uzmanlar",
                column: "calismaSaatleriIdId",
                principalTable: "CalismaSaatleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uzmanlar_CalismaSaatleri_calismaSaatleriIdId",
                table: "Uzmanlar");

            migrationBuilder.DropIndex(
                name: "IX_Uzmanlar_calismaSaatleriIdId",
                table: "Uzmanlar");

            migrationBuilder.DropColumn(
                name: "calismaSaatleri",
                table: "Uzmanlar");

            migrationBuilder.DropColumn(
                name: "calismaSaatleriIdId",
                table: "Uzmanlar");

            migrationBuilder.AddColumn<int>(
                name: "UzmanId",
                table: "CalismaSaatleri",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CalismaSaatleri",
                keyColumn: "Id",
                keyValue: 1,
                column: "UzmanId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CalismaSaatleri",
                keyColumn: "Id",
                keyValue: 2,
                column: "UzmanId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CalismaSaatleri",
                keyColumn: "Id",
                keyValue: 3,
                column: "UzmanId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_CalismaSaatleri_UzmanId",
                table: "CalismaSaatleri",
                column: "UzmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalismaSaatleri_Uzmanlar_UzmanId",
                table: "CalismaSaatleri",
                column: "UzmanId",
                principalTable: "Uzmanlar",
                principalColumn: "Id");
        }
    }
}
