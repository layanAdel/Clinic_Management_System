using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMedicalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientName",
                table: "MedicalRecords",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "DoctorName",
                table: "MedicalRecords",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "DoctorFirstName",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoctorLastName",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorFirstName",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "DoctorLastName",
                table: "MedicalRecords");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "MedicalRecords",
                newName: "PatientName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "MedicalRecords",
                newName: "DoctorName");
        }
    }
}
