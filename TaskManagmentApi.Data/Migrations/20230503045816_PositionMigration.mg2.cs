using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagmentApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class PositionMigrationmg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "Developers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "Developers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
