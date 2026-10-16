using Clinic_Management_System.Domain.Models;

namespace Clinic_Management_System.Infrastructure.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(string uuid);
        Task AddAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(string uuid);
    }
}
