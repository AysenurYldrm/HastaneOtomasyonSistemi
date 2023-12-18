using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneOtomasyonSistemi.Migrations
{
    public partial class HastaneOtomasyonSistemi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hasta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    soyAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    KimlikNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hasta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "poliklinik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoliklinikIsmi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_poliklinik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doktor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    soyAd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KimlikNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    poliklinikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doktor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doktor_poliklinik_poliklinikId",
                        column: x => x.poliklinikId,
                        principalTable: "poliklinik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Randevu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hastaId = table.Column<int>(type: "int", nullable: false),
                    RandevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    doktorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Randevu_Doktor_doktorId",
                        column: x => x.doktorId,
                        principalTable: "Doktor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevu_Hasta_hastaId",
                        column: x => x.hastaId,
                        principalTable: "Hasta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doktor_poliklinikId",
                table: "Doktor",
                column: "poliklinikId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_doktorId",
                table: "Randevu",
                column: "doktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_hastaId",
                table: "Randevu",
                column: "hastaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Randevu");

            migrationBuilder.DropTable(
                name: "Doktor");

            migrationBuilder.DropTable(
                name: "Hasta");

            migrationBuilder.DropTable(
                name: "poliklinik");
        }
    }
}
