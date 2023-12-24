using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneOtomasyonSistemi.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Hastaneler_ilId",
                table: "Hastaneler",
                column: "ilId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hastaneler_il_ilId",
                table: "Hastaneler",
                column: "ilId",
                principalTable: "il",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hastaneler_il_ilId",
                table: "Hastaneler");

            migrationBuilder.DropIndex(
                name: "IX_Hastaneler_ilId",
                table: "Hastaneler");
        }
    }
}
