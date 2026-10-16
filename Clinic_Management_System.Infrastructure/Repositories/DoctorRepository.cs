using Clinic_Management_System.Infrastructure.Data;
using Clinic_Management_System.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _db;

        public DoctorRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task AddDoctorAsync(Doctor doctor)
        {
            _db.Doctors.Add(doctor);
            return _db.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(string uuid)
        {
            var doctor = await _db.Doctors.FirstOrDefaultAsync(d => d.Uuid == uuid);

            if (doctor != null)
            {
                _db.Doctors.Remove(doctor);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            IEnumerable<Doctor> doctors = await _db.Doctors
                   .Include(d => d.Department)
                   .Include(d => d.Appointments)
                   .Include(d => d.MedicalRecords)
                   .Include(d => d.Prescriptions)
                   .ToListAsync();
            
            return doctors;
        }

        public async Task<Doctor?> GetDoctorByIdAsync(string uuid)
        {
            var doctor = await _db.Doctors.FirstOrDefaultAsync(d => d.Uuid == uuid);
            return doctor;
        }

        public Task UpdateDoctorAsync(Doctor doctor)
        {
            _db.Doctors.Update(doctor);
            return _db.SaveChangesAsync();
        }
    }
}
