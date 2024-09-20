using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Doctors.Migrations
{
    /// <inheritdoc />
    public partial class init_upd1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                schema: "Users",
                table: "Doctors");

            migrationBuilder.EnsureSchema(
                name: "Doctors");

            migrationBuilder.RenameTable(
                name: "Doctors",
                schema: "Users",
                newName: "Users",
                newSchema: "Doctors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "Doctors",
                table: "Users",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "Doctors",
                table: "Users");

            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Doctors",
                newName: "Doctors",
                newSchema: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                schema: "Users",
                table: "Doctors",
                column: "Id");
        }
    }
}
