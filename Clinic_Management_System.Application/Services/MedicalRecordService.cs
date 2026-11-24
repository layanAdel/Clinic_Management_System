using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Repositories.Base;
using Clinic_Management_System.Application.Services.Base;

namespace Clinic_Management_System.Application.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicalRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecords()
        {
            var medicalRecords = await _unitOfWork.MedicalRecordRepository.GetAllMedicalRecordsAsync();

            return medicalRecords
                 .Select(a => new MedicalRecordDto
                 {
                     Id = a.Id,
                     Uuid = a.Uuid,
                     PatientName = a.Patient != null ? a.Patient.FirstName : null,
                     DoctorName = a.Doctor != null ? a.Doctor.FirstName : null,
                     VisitDate = a.VisitDate,
                     Diagnosis = a.Diagnosis,
                     Notes = a.Notes

                 });
        }


        public async Task AddMedicalRecord(MedicalRecordCreateDto medicalRecordDTO)
        {
            var medicalRecord = new MedicalRecord
            {
                PatientId = medicalRecordDTO.PatientId,
                DoctorId = medicalRecordDTO.DoctorId,
                VisitDate = medicalRecordDTO.VisitDate,
                Diagnosis = medicalRecordDTO.Diagnosis,
                Notes = medicalRecordDTO.Notes,
            };

            await _unitOfWork.MedicalRecordRepository.AddMedicalRecordAsync(medicalRecord);

        }

        public async Task DeleteMedicalRecord(string uuid)
        {
            var med = await _unitOfWork.MedicalRecordRepository.GetMedicalRecordByIdAsync(uuid);

            if (med == null)
            {
                throw new KeyNotFoundException("MedicalRecord not found");
            }

            await _unitOfWork.MedicalRecordRepository.DeleteMedicalRecordAsync(uuid);

        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctors()
        {
            var doctors = await _unitOfWork.DoctorRepository.GetAllDoctorsAsync();

            return doctors.Select(p => new DoctorDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
            });
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatients()
        {
            var patients = await _unitOfWork.PatientRepository.GetAllPatientsAsync();

            return patients.Select(p => new PatientDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
            });
        }

        public async Task<MedicalRecordUpdateDto?> GetMedicalRecordById(string uuid)
        {

            var med = await _unitOfWork.MedicalRecordRepository.GetMedicalRecordByIdAsync(uuid);

            if (med == null)
            {
                return null;
            }

           return new MedicalRecordUpdateDto
            {
                Id = med.Id,
                Uuid = med.Uuid,
                PatientId = med.PatientId,
                DoctorId = med.DoctorId,
                VisitDate = med.VisitDate,
                Diagnosis = med.Diagnosis,
                Notes = med.Notes,
            };
        }
    

        public async Task UpdateMedicalRecord(MedicalRecordUpdateDto medicalRecordDTO)
        {
            var medicalRecord = await _unitOfWork.MedicalRecordRepository.GetMedicalRecordByIdAsync(medicalRecordDTO.Uuid);

            if (medicalRecord == null)
            {
                throw new KeyNotFoundException("MedicalRecord not found");
            }

            medicalRecord.PatientId = medicalRecordDTO.PatientId;
            medicalRecord.DoctorId = medicalRecordDTO.DoctorId;
            medicalRecord.VisitDate = medicalRecordDTO.VisitDate;
            medicalRecord.Diagnosis = medicalRecordDTO.Diagnosis;
            medicalRecord.Notes = medicalRecordDTO.Notes;

            await _unitOfWork.MedicalRecordRepository.UpdateMedicalRecordAsync(medicalRecord);
        }
    }
}
