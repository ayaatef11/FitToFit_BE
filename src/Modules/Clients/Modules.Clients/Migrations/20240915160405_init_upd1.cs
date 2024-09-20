using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Clients.Migrations
{
    /// <inheritdoc />
    public partial class init_upd1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                schema: "Users",
                table: "Clients");

            migrationBuilder.EnsureSchema(
                name: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                schema: "Users",
                newName: "Users",
                newSchema: "Clients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "Clients",
                table: "Users",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "Clients",
                table: "Users");

            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Clients",
                newName: "Clients",
                newSchema: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                schema: "Users",
                table: "Clients",
                column: "Id");
        }
    }
}
