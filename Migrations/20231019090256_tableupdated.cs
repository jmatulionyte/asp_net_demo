using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twest2.Migrations
{
    /// <inheritdoc />
    public partial class tableupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place1",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Place2",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Place3",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Place4",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Place5",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Place6",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Place7",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Place8",
                table: "Tournaments");

            migrationBuilder.RenameColumn(
                name: "TournamentWins",
                table: "Players",
                newName: "Rating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Players",
                newName: "TournamentWins");

            migrationBuilder.AddColumn<string>(
                name: "Place1",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place2",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place3",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place4",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place5",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place6",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place7",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place8",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
