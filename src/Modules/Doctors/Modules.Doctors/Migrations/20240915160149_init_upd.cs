using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Doctors.Migrations
{
    /// <inheritdoc />
    public partial class init_upd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                schema: "Doctors",
                table: "Agents");

            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.RenameTable(
                name: "Agents",
                schema: "Doctors",
                newName: "Doctors",
                newSchema: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "DrNameEn",
                schema: "Users",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DrNameAr",
                schema: "Users",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                schema: "Users",
                table: "Doctors",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "Agents",
                newSchema: "Doctors");

            migrationBuilder.AlterColumn<string>(
                name: "DrNameEn",
                schema: "Doctors",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DrNameAr",
                schema: "Doctors",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                schema: "Doctors",
                table: "Agents",
                column: "Id");
        }
    }
}
