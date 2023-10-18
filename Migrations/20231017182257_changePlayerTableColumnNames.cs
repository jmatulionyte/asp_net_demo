using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twest2.Migrations
{
    /// <inheritdoc />
    public partial class changePlayerTableColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Wins",
                table: "Players",
                newName: "TournamentWins");

            migrationBuilder.RenameColumn(
                name: "Losses",
                table: "Players",
                newName: "GroupWins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TournamentWins",
                table: "Players",
                newName: "Wins");

            migrationBuilder.RenameColumn(
                name: "GroupWins",
                table: "Players",
                newName: "Losses");
        }
    }
}
