using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Repositories.Base;
using Clinic_Management_System.Application.Services.Base;

namespace Clinic_Management_System.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PatientDto>> GetAllPatients()
        {
            var patients = await _unitOfWork.PatientRepository.GetAllPatientsAsync();

            return patients
               .Select(p => new PatientDto
               {
                   Id = p.Id,
                   Uuid = p.Uuid,
                   FirstName = p.FirstName,
                   LastName = p.LastName,
                   BirthDate = p.BirthDate,
                   Gender = p.Gender,
                   Phone = p.Phone,
                   Email = p.Email,
                   AppointmentId = p.Appointments != null ? p.Appointments.Count() : 0,
                   MedicalRecordId = p.MedicalRecords != null ? p.MedicalRecords.Count() : 0,
                   PrescriptionId = p.Prescriptions != null ? p.Prescriptions.Count() : 0,
                   InvoiceId = p.Invoices != null ? p.Invoices.Count() : 0

               });
        }

        public async Task AddPatient(PatientCreateDto patientDTO)
        {
            var patient = new Patient
            {
                FirstName = patientDTO.FirstName,
                LastName = patientDTO.LastName,
                BirthDate = patientDTO.BirthDate,
                Gender = patientDTO.Gender,
                Phone = patientDTO.Phone,
                Email = patientDTO.Email
            };

            await _unitOfWork.PatientRepository.AddPatientAsync(patient);
        }

        public async Task DeletePatient(string uuid)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientByIdAsync(uuid);

            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found");
            }

            await _unitOfWork.PatientRepository.DeletePatientAsync(uuid);
        }
        public async Task<PatientUpdateDto?> GetPatientById(string uuid)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientByIdAsync(uuid);

            if (patient == null)
            {
                return null;
            }

            return new PatientUpdateDto
            {
                Id = patient.Id,
                Uuid = patient.Uuid,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BirthDate = patient.BirthDate,
                Gender = patient.Gender,
                Phone = patient.Phone,
                Email = patient.Email
            };
        }

        public async Task UpdatePatient(PatientUpdateDto patientDTO)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientByIdAsync(patientDTO.Uuid);

            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found");
            }

            patient.FirstName = patientDTO.FirstName;
            patient.LastName = patientDTO.LastName;
            patient.BirthDate = patientDTO.BirthDate;
            patient.Gender = patientDTO.Gender;
            patient.Phone = patientDTO.Phone;
            patient.Email = patientDTO.Email;

            await _unitOfWork.PatientRepository.UpdatePatientAsync(patient);
        }
    }
}
