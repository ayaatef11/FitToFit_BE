using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Clients.Migrations
{
    /// <inheritdoc />
    public partial class init_upd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                schema: "Clients",
                table: "Agents");

            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.RenameTable(
                name: "Agents",
                schema: "Clients",
                newName: "Clients",
                newSchema: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "Users",
                table: "Clients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "Users",
                table: "Clients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                schema: "Users",
                table: "Clients",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "Agents",
                newSchema: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "Clients",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "Clients",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                schema: "Clients",
                table: "Agents",
                column: "Id");
        }
    }
}
