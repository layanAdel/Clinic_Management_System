using Clinic_Management_System.Domain.Models;

namespace Clinic_Management_System.Infrastructure.Repositories
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription?> GetPrescriptionByIdAsync(string uuid);
        Task AddPrescriptionAsync(Prescription prescription);
        Task UpdatePrescriptionAsync(Prescription prescription);
        Task DeletePrescriptionAsync(string uuid);
    }
}
