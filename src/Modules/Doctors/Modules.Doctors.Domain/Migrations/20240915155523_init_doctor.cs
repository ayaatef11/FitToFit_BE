using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Doctors.Migrations
{
    /// <inheritdoc />
    public partial class init_doctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Doctors");

            migrationBuilder.CreateTable(
                name: "Agents",
                schema: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agents",
                schema: "Doctors");
        }
    }
}
