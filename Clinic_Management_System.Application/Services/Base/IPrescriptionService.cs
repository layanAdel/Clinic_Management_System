using Clinic_Management_System.Application.Dtos;

namespace Clinic_Management_System.Application.Services.Base
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<PrescriptionDto>> GetAllPrescriptions();

        Task<PrescriptionUpdateDto?> GetPrescriptionById(string uuid);

        Task<IEnumerable<PatientDto>> GetAllPatients();

        Task<IEnumerable<DoctorDto>> GetAllDoctors();

        Task AddPrescription(PrescriptionCreateDto prescriptionDTO);

        Task UpdatePrescription(PrescriptionUpdateDto prescriptionDTO);

        Task DeletePrescription(string uuid);
    }
}
