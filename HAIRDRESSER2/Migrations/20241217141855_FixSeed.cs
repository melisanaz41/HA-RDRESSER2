using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAIRDRESSER2.Migrations
{
    /// <inheritdoc />
    public partial class FixSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uzmanlar_CalismaSaatleri_calismaSaatleriIdId",
                table: "Uzmanlar");

            migrationBuilder.DropColumn(
                name: "calismaSaatleri",
                table: "Uzmanlar");

            migrationBuilder.RenameColumn(
                name: "calismaSaatleriIdId",
                table: "Uzmanlar",
                newName: "CalismaSaatiId");

            migrationBuilder.RenameIndex(
                name: "IX_Uzmanlar_calismaSaatleriIdId",
                table: "Uzmanlar",
                newName: "IX_Uzmanlar_CalismaSaatiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uzmanlar_CalismaSaatleri_CalismaSaatiId",
                table: "Uzmanlar",
                column: "CalismaSaatiId",
                principalTable: "CalismaSaatleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uzmanlar_CalismaSaatleri_CalismaSaatiId",
                table: "Uzmanlar");

            migrationBuilder.RenameColumn(
                name: "CalismaSaatiId",
                table: "Uzmanlar",
                newName: "calismaSaatleriIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Uzmanlar_CalismaSaatiId",
                table: "Uzmanlar",
                newName: "IX_Uzmanlar_calismaSaatleriIdId");

            migrationBuilder.AddColumn<int>(
                name: "calismaSaatleri",
                table: "Uzmanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Uzmanlar_CalismaSaatleri_calismaSaatleriIdId",
                table: "Uzmanlar",
                column: "calismaSaatleriIdId",
                principalTable: "CalismaSaatleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
