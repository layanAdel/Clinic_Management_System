using Clinic_Management_System.Application.Dtos;

namespace Clinic_Management_System.Application.Services.Base
{
    public interface IMedicalRecordService
    {
        Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecords();

        Task<MedicalRecordUpdateDto?> GetMedicalRecordById(string uuid);

        Task<IEnumerable<PatientDto>> GetAllPatients();

        Task<IEnumerable<DoctorDto>> GetAllDoctors();

        Task AddMedicalRecord(MedicalRecordCreateDto medicalRecordDTO);

        Task UpdateMedicalRecord(MedicalRecordUpdateDto medicalRecordDTO);

        Task DeleteMedicalRecord(string uuid);
    }
}
