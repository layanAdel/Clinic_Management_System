using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly AppDbContext _db;

        public PrescriptionRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task AddPrescriptionAsync(Prescription prescription)
        {
            _db.Prescriptions.Add(prescription);
            return _db.SaveChangesAsync();
        }

        public async Task DeletePrescriptionAsync(string uuid)
        {
            var prescription = await _db.Prescriptions.FirstOrDefaultAsync(d => d.Uuid == uuid);

            if (prescription != null)
            {
                _db.Prescriptions.Remove(prescription);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
        {
            IEnumerable<Prescription> prescriptions = await _db.Prescriptions
                     .Include(a => a.Patient)
                     .Include(a => a.Doctor)
                     .ToListAsync();

            return prescriptions;
        }

        public async Task<Prescription?> GetPrescriptionByIdAsync(string uuid)
        {
            var prescription = await _db.Prescriptions.FirstOrDefaultAsync(d => d.Uuid == uuid);
            return prescription;
                }

        public Task UpdatePrescriptionAsync(Prescription prescription)
        {
            _db.Prescriptions.Update(prescription);
            return _db.SaveChangesAsync();
        }
    }
}
