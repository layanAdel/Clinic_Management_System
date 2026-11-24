using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Repositories.Base;

namespace Clinic_Management_System.Application.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PrescriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PrescriptionDto>> GetAllPrescriptions()
        {
            var prescriptions = await _unitOfWork.PrescriptionRepository.GetAllPrescriptionsAsync();

          return prescriptions
                .Select(p => new PrescriptionDto
                {
                    Id = p.Id,
                    Uuid = p.Uuid,
                    PatientName = p.Patient != null ? p.Patient.FirstName : null,
                    DoctorName = p.Doctor != null ? p.Doctor.FirstName : null,
                    Medication = p.Medication,
                    Dosage = p.Dosage,
                    Instructions = p.Instructions,
                    PrescriptionImage = p.PrescriptionImage,
                    PrescriptionDate = p.PrescriptionDate

                });
        }
        public async Task AddPrescription(PrescriptionCreateDto prescriptionDTO)
        {
            var prescription = new Prescription
            {
                PatientId = prescriptionDTO.PatientId,
                DoctorId = prescriptionDTO.DoctorId,
                Medication = prescriptionDTO.Medication,
                Dosage = prescriptionDTO.Dosage,
                Instructions = prescriptionDTO.Instructions,
                PrescriptionImage = prescriptionDTO.PrescriptionImage,
                PrescriptionDate = prescriptionDTO.PrescriptionDate
            };

            await _unitOfWork.PrescriptionRepository.AddPrescriptionAsync(prescription);
        }

        public async Task DeletePrescription(string uuid)
        {
            var pre = await _unitOfWork.PrescriptionRepository.GetPrescriptionByIdAsync(uuid);

            if (pre == null)
            {
                throw new KeyNotFoundException("Prescription not found");
            }
            await _unitOfWork.PrescriptionRepository.DeletePrescriptionAsync(uuid);
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

        public async Task<PrescriptionUpdateDto?> GetPrescriptionById(string uuid)
        {
            var pre = await _unitOfWork.PrescriptionRepository.GetPrescriptionByIdAsync(uuid);

            if (pre == null)
            {
                return null;
            }

            return new PrescriptionUpdateDto
            {
                Id = pre.Id,
                Uuid = pre.Uuid,
                PatientId = pre.PatientId,
                DoctorId = pre.DoctorId,
                Medication = pre.Medication,
                Dosage = pre.Dosage,
                Instructions = pre.Instructions,
                PrescriptionDate = pre.PrescriptionDate
            };
        }

        public async Task UpdatePrescription(PrescriptionUpdateDto prescriptionDTO)
        {
            var prescription = await _unitOfWork.PrescriptionRepository.GetPrescriptionByIdAsync(prescriptionDTO.Uuid);

            if (prescription == null)
            {
                throw new KeyNotFoundException("Prescription not found");
            }

            prescription.PatientId = prescriptionDTO.PatientId;
            prescription.DoctorId = prescriptionDTO.DoctorId;
            prescription.Medication = prescriptionDTO.Medication;
            prescription.Dosage = prescriptionDTO.Dosage;
            prescription.Instructions = prescriptionDTO.Instructions;
            prescription.PrescriptionDate = prescriptionDTO.PrescriptionDate;

            await _unitOfWork.PrescriptionRepository.UpdatePrescriptionAsync(prescription);
        }
    }
}
