using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagmentApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaskTableMigrationmg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tasks");
        }
    }
}
