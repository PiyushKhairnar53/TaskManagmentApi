using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagmentApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManagerDeveloperMigrationmg3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_AspNetUsers_UserId",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_AspNetUsers_UserId",
                table: "Managers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Managers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Developers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_AspNetUsers_UserId",
                table: "Developers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_AspNetUsers_UserId",
                table: "Managers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_AspNetUsers_UserId",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_AspNetUsers_UserId",
                table: "Managers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Managers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Developers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_AspNetUsers_UserId",
                table: "Developers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_AspNetUsers_UserId",
                table: "Managers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
