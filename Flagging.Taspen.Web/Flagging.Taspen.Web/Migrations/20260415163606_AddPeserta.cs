using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flagging.Taspen.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddPeserta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peserta",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    NIP = table.Column<string>(type: "TEXT", nullable: false),
                    Notas = table.Column<string>(type: "TEXT", nullable: false),
                    NoKPE = table.Column<string>(type: "TEXT", nullable: false),
                    Nama = table.Column<string>(type: "TEXT", nullable: false),
                    TanggalLahir = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Instansi = table.Column<string>(type: "TEXT", nullable: false),
                    Provinsi = table.Column<string>(type: "TEXT", nullable: false),
                    Kota = table.Column<string>(type: "TEXT", nullable: false),
                    Kecamatan = table.Column<string>(type: "TEXT", nullable: false),
                    Kelurahan = table.Column<string>(type: "TEXT", nullable: false),
                    Alamat = table.Column<string>(type: "TEXT", nullable: false),
                    RekKredit = table.Column<string>(type: "TEXT", nullable: false),
                    RekTabungan = table.Column<string>(type: "TEXT", nullable: false),
                    NIK = table.Column<string>(type: "TEXT", nullable: false),
                    Surat = table.Column<string>(type: "TEXT", nullable: false),
                    TMTKredit = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TATKredit = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peserta", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peserta");
        }
    }
}
