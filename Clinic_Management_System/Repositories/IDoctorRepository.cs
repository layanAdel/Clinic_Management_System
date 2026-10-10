using Clinic_Management_System.Models;

namespace Clinic_Management_System.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor?>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(string uuid);
        Task AddDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(string uuid);

    }
}

