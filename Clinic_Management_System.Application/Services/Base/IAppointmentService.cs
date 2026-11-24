using Clinic_Management_System.Application.Dtos;

namespace Clinic_Management_System.Application.Services.Base
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointments();

        Task<AppointmentUpdateDto?> GetAppointmentById(string uuid);

        Task AddAppointment(AppointmentCreateDto appointmentDTO);

        Task UpdateAppointment(AppointmentUpdateDto appointmentDTO);

        Task DeleteAppointment(string uuid);

        Task<IEnumerable<DoctorDto>> GetAllDoctors();

        Task<IEnumerable<PatientDto>> GetAllPatients();

    }
}
