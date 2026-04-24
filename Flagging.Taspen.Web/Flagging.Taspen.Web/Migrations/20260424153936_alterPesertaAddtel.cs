using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flagging.Taspen.Web.Migrations
{
    /// <inheritdoc />
    public partial class alterPesertaAddtel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NoTel",
                table: "Peserta",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoTel",
                table: "Peserta");
        }
    }
}
