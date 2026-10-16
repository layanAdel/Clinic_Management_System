using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic_Management_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorFirstName",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "DoctorLastName",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "DoctorFirstName",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "DoctorLastName",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Invoices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
