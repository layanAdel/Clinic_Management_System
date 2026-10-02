using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientName",
                table: "Appointments",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "DoctorName",
                table: "Appointments",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "DoctorFirstName",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoctorLastName",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorFirstName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorLastName",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Appointments",
                newName: "PatientName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Appointments",
                newName: "DoctorName");
        }
    }
}
