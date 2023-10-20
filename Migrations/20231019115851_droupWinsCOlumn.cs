using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twest2.Migrations
{
    /// <inheritdoc />
    public partial class droupWinsCOlumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentCreationDate",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TournamentEndDate",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "GroupWins",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "GroupWins",
                table: "GroupResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupWins",
                table: "GroupResults");

            migrationBuilder.AddColumn<DateTime>(
                name: "TournamentCreationDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TournamentEndDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GroupWins",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
