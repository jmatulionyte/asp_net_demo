using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twest2.Migrations
{
    /// <inheritdoc />
    public partial class newColumnGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player1",
                table: "NewTournaments");

            migrationBuilder.DropColumn(
                name: "Player2",
                table: "NewTournaments");

            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "Player1",
                table: "NewTournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Player2",
                table: "NewTournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
