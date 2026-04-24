using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flagging.Taspen.Web.Migrations
{
    /// <inheritdoc />
    public partial class alterPesertaAddIDs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdKecamatan",
                table: "Peserta",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdKelurahan",
                table: "Peserta",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdKota",
                table: "Peserta",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdProvinsi",
                table: "Peserta",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdKecamatan",
                table: "Peserta");

            migrationBuilder.DropColumn(
                name: "IdKelurahan",
                table: "Peserta");

            migrationBuilder.DropColumn(
                name: "IdKota",
                table: "Peserta");

            migrationBuilder.DropColumn(
                name: "IdProvinsi",
                table: "Peserta");
        }
    }
}
