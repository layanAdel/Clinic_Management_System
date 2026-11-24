using Clinic_Management_System.Application.Dtos;

namespace Clinic_Management_System.Application.Services.Base
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatients();

        Task<PatientUpdateDto?> GetPatientById(string uuid);

        Task AddPatient(PatientCreateDto patientDTO);

        Task UpdatePatient(PatientUpdateDto patientDTO);

        Task DeletePatient(string uuid);
    }
}
