using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneOtomasyonSistemi.Migrations
{
    public partial class hastane : Migration
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
                name: "il",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ilAd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_il", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ilce",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ilceAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ilId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ilce", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ilce_il_ilId",
                        column: x => x.ilId,
                        principalTable: "il",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hastaneler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HastaneAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ilId = table.Column<int>(type: "int", nullable: false),
                    ilceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hastaneler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hastaneler_il_ilId",
                        column: x => x.ilId,
                        principalTable: "il",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hastaneler_ilce_ilceId",
                        column: x => x.ilceId,
                        principalTable: "ilce",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "poliklinik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoliklinikIsmi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ilId = table.Column<int>(type: "int", nullable: false),
                    ilceId = table.Column<int>(type: "int", nullable: false),
                    hastaneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_poliklinik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_poliklinik_Hastaneler_hastaneId",
                        column: x => x.hastaneId,
                        principalTable: "Hastaneler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_poliklinik_il_ilId",
                        column: x => x.ilId,
                        principalTable: "il",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_poliklinik_ilce_ilceId",
                        column: x => x.ilceId,
                        principalTable: "ilce",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
                    ilId = table.Column<int>(type: "int", nullable: false),
                    ilceId = table.Column<int>(type: "int", nullable: false),
                    hastaneId = table.Column<int>(type: "int", nullable: false),
                    RandevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RandevuDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doktorId = table.Column<int>(type: "int", nullable: false),
                    poliklinikId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Randevu_Hastaneler_hastaneId",
                        column: x => x.hastaneId,
                        principalTable: "Hastaneler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Randevu_il_ilId",
                        column: x => x.ilId,
                        principalTable: "il",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Randevu_ilce_ilceId",
                        column: x => x.ilceId,
                        principalTable: "ilce",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Randevu_poliklinik_poliklinikId",
                        column: x => x.poliklinikId,
                        principalTable: "poliklinik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doktor_poliklinikId",
                table: "Doktor",
                column: "poliklinikId");

            migrationBuilder.CreateIndex(
                name: "IX_Hastaneler_ilceId",
                table: "Hastaneler",
                column: "ilceId");

            migrationBuilder.CreateIndex(
                name: "IX_Hastaneler_ilId",
                table: "Hastaneler",
                column: "ilId");

            migrationBuilder.CreateIndex(
                name: "IX_ilce_ilId",
                table: "ilce",
                column: "ilId");

            migrationBuilder.CreateIndex(
                name: "IX_poliklinik_hastaneId",
                table: "poliklinik",
                column: "hastaneId");

            migrationBuilder.CreateIndex(
                name: "IX_poliklinik_ilceId",
                table: "poliklinik",
                column: "ilceId");

            migrationBuilder.CreateIndex(
                name: "IX_poliklinik_ilId",
                table: "poliklinik",
                column: "ilId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_doktorId",
                table: "Randevu",
                column: "doktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_hastaId",
                table: "Randevu",
                column: "hastaId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_hastaneId",
                table: "Randevu",
                column: "hastaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_ilceId",
                table: "Randevu",
                column: "ilceId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_ilId",
                table: "Randevu",
                column: "ilId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_poliklinikId",
                table: "Randevu",
                column: "poliklinikId");
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

            migrationBuilder.DropTable(
                name: "Hastaneler");

            migrationBuilder.DropTable(
                name: "ilce");

            migrationBuilder.DropTable(
                name: "il");
        }
    }
}
