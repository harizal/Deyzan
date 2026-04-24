using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flagging.Taspen.Web.Migrations
{
    /// <inheritdoc />
    public partial class alterPesertaAddFlagingFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFlaging",
                table: "Peserta",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "IsFlagingDate",
                table: "Peserta",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFlaging",
                table: "Peserta");

            migrationBuilder.DropColumn(
                name: "IsFlagingDate",
                table: "Peserta");
        }
    }
}
