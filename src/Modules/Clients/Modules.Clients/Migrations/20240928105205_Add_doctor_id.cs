using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Clients.Migrations
{
    /// <inheritdoc />
    public partial class Add_doctor_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                schema: "Clients",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                schema: "Clients",
                table: "Users");
        }
    }
}
