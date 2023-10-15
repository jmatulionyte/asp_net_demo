using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twest2.Migrations
{
    /// <inheritdoc />
    public partial class groupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "NewTournaments");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player1Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player2Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TournamentCraetionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendeeFirst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttendeeSecond = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TournamentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TournamentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewTournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewTournaments", x => x.Id);
                });
        }
    }
}
