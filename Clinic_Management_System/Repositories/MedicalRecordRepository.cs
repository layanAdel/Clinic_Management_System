using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly AppDbContext _db;

        public MedicalRecordRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task AddMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            _db.MedicalRecords.Add(medicalRecord);
            return _db.SaveChangesAsync();
        }

        public async Task DeleteMedicalRecordAsync(string uuid)
        {
            var medicalRecord = await _db.MedicalRecords.FirstOrDefaultAsync(d => d.Uuid == uuid);

            if (medicalRecord != null)
            {
                _db.MedicalRecords.Remove(medicalRecord);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync()
        {
            IEnumerable<MedicalRecord> medicalRecords = await _db.MedicalRecords
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .ToListAsync();

            return medicalRecords;
        }

        public async Task<MedicalRecord> GetMedicalRecordByIdAsync(string uuid)
        {
            var medicalRecord = await _db.MedicalRecords.FirstOrDefaultAsync(d => d.Uuid == uuid);
            return medicalRecord;
        }

        public Task UpdateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            _db.MedicalRecords.Update(medicalRecord);
            return _db.SaveChangesAsync();
        }
    }
}
