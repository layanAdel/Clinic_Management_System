using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _db;

        public PatientRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task AddPatientAsync(Patient patient)
        {
            _db.Patients.Add(patient);
            return _db.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(string uuid)
        {
            var patient = await _db.Patients.FirstOrDefaultAsync(d => d.Uuid == uuid);

            if (patient != null)
            {
                _db.Patients.Remove(patient);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            IEnumerable<Patient> patients = await _db.Patients
                   .Include(p => p.Appointments)
                   .Include(p => p.MedicalRecords)
                   .Include(p => p.Prescriptions)
                   .Include(p => p.Invoices)
                   .ToListAsync();
            return patients;
        }

        public async Task<Patient?> GetPatientByIdAsync(string uuid)
        {
            var patient = await _db.Patients.FirstOrDefaultAsync(d => d.Uuid == uuid);
            return patient;
        }

        public Task UpdatePatientAsync(Patient patient)
        {
            _db.Patients.Update(patient);
            return _db.SaveChangesAsync();
        }
    }
}
