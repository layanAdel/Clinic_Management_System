using Clinic_Management_System.Domain.Models;

namespace Clinic_Management_System.Infrastructure.Repositories
{
    public interface IMedicalRecordRepository
    {
        Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync();
        Task<MedicalRecord?> GetMedicalRecordByIdAsync(string uuid);
        Task AddMedicalRecordAsync(MedicalRecord medicalRecord);
        Task UpdateMedicalRecordAsync(MedicalRecord medicalRecord);
        Task DeleteMedicalRecordAsync(string uuid);
    }
}
