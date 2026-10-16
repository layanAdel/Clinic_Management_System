using Clinic_Management_System.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        { 
        }

        public DbSet<Patient> Patients { get; set; } 

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
       public DbSet<Appointment> Appointments { get; set; }
       public DbSet<MedicalRecord> MedicalRecords { get; set; }
       public DbSet<Prescription> Prescriptions { get; set; }
       public DbSet<Employee> Employees { get; set; }
       public DbSet<Invoice> Invoices { get; set; }
        public DbSet<User> Users { get; set; }



    }
}
