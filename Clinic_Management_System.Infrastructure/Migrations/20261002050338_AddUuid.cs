using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic_Management_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "MedicalRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Uuid",
                table: "Users",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_Uuid",
                table: "Prescriptions",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Uuid",
                table: "Patients",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Uuid",
                table: "MedicalRecords",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Uuid",
                table: "Invoices",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Uuid",
                table: "Employees",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Uuid",
                table: "Doctors",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Uuid",
                table: "Departments",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Uuid",
                table: "Appointments",
                column: "Uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Uuid",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_Uuid",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Uuid",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_Uuid",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_Uuid",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Uuid",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_Uuid",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Departments_Uuid",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Uuid",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Appointments");
        }
    }
}
