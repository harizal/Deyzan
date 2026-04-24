using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flagging.Taspen.Web.Migrations
{
    /// <inheritdoc />
    public partial class addProvinsiKotaKecamatanKelurahan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provinsi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kode = table.Column<string>(type: "TEXT", nullable: false),
                    Nama = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinsi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kota",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kode = table.Column<string>(type: "TEXT", nullable: false),
                    Nama = table.Column<string>(type: "TEXT", nullable: false),
                    Tipe = table.Column<string>(type: "TEXT", nullable: false),
                    ProvinsiId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kota_Provinsi_ProvinsiId",
                        column: x => x.ProvinsiId,
                        principalTable: "Provinsi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kecamatan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kode = table.Column<string>(type: "TEXT", nullable: false),
                    Nama = table.Column<string>(type: "TEXT", nullable: false),
                    KotaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kecamatan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kecamatan_Kota_KotaId",
                        column: x => x.KotaId,
                        principalTable: "Kota",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kelurahan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kode = table.Column<string>(type: "TEXT", nullable: false),
                    Nama = table.Column<string>(type: "TEXT", nullable: false),
                    KodePos = table.Column<string>(type: "TEXT", nullable: false),
                    Tipe = table.Column<string>(type: "TEXT", nullable: false),
                    KecamatanId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kelurahan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kelurahan_Kecamatan_KecamatanId",
                        column: x => x.KecamatanId,
                        principalTable: "Kecamatan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kecamatan_KotaId",
                table: "Kecamatan",
                column: "KotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kelurahan_KecamatanId",
                table: "Kelurahan",
                column: "KecamatanId");

            migrationBuilder.CreateIndex(
                name: "IX_Kota_ProvinsiId",
                table: "Kota",
                column: "ProvinsiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kelurahan");

            migrationBuilder.DropTable(
                name: "Kecamatan");

            migrationBuilder.DropTable(
                name: "Kota");

            migrationBuilder.DropTable(
                name: "Provinsi");
        }
    }
}
