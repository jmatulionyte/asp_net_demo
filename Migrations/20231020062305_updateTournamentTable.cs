using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twest2.Migrations
{
    /// <inheritdoc />
    public partial class updateTournamentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TournamentOngoing",
                table: "Tournaments",
                newName: "GroupPlaysOngoing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupPlaysOngoing",
                table: "Tournaments",
                newName: "TournamentOngoing");
        }
    }
}
