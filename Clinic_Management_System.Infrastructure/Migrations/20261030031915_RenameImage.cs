using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic_Management_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MedicationImage",
                table: "Prescriptions",
                newName: "PrescriptionImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrescriptionImage",
                table: "Prescriptions",
                newName: "MedicationImage");
        }
    }
}
