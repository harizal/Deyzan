using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flagging.Taspen.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingFieldsToPeserta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBooking",
                table: "Peserta",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "IsBookingDate",
                table: "Peserta",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBooking",
                table: "Peserta");

            migrationBuilder.DropColumn(
                name: "IsBookingDate",
                table: "Peserta");
        }
    }
}
