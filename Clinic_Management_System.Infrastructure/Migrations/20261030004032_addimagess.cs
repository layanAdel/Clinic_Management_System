using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic_Management_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addimagess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentImage",
                table: "Departments");

            migrationBuilder.AddColumn<string>(
                name: "CVimage",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVimage",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentImage",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
