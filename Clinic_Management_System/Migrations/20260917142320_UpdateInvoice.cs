using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Invoices",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PatientName",
                table: "Invoices",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Invoices",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Invoices",
                newName: "PatientName");
        }
    }
}
