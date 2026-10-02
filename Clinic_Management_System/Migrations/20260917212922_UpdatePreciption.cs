using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePreciption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientName",
                table: "Prescriptions",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "DoctorName",
                table: "Prescriptions",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "DoctorFirstName",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoctorLastName",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorFirstName",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "DoctorLastName",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Prescriptions",
                newName: "PatientName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Prescriptions",
                newName: "DoctorName");
        }
    }
}
