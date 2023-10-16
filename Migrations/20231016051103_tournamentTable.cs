using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twest2.Migrations
{
    /// <inheritdoc />
    public partial class tournamentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentCraetionDateTime",
                table: "Groups");

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TournamentEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Place1 = table.Column<int>(type: "int", nullable: false),
                    Place2 = table.Column<int>(type: "int", nullable: false),
                    Place3 = table.Column<int>(type: "int", nullable: false),
                    Place4 = table.Column<int>(type: "int", nullable: false),
                    Place5 = table.Column<int>(type: "int", nullable: false),
                    Place6 = table.Column<int>(type: "int", nullable: false),
                    Place7 = table.Column<int>(type: "int", nullable: false),
                    Place8 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.AddColumn<DateTime>(
                name: "TournamentCraetionDateTime",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
