using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctors()
        {
            var doctors = await _unitOfWork.DoctorRepository.GetAllDoctorsAsync();

            return doctors
                .Select(d => new DoctorDto
                {
                    Id = d.Id,
                    Uuid = d.Uuid,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Specialization = d.Specialization,
                    Phone = d.Phone,
                    Email = d.Email,
                    DepartmentName = d.Department != null ? d.Department.Name : null,
                    AppointmentId = d.Appointments != null ? d.Appointments.Count() : 0,
                    MedicalRecordId = d.MedicalRecords != null ? d.MedicalRecords.Count() : 0,
                    PrescriptionId = d.Prescriptions != null ? d.Prescriptions.Count() : 0

                });
        }


        public async Task AddDoctor(DoctorCreateDto doctorDTO)
        {
            var doctor = new Doctor
            {
                FirstName = doctorDTO.FirstName,
                LastName = doctorDTO.LastName,
                Specialization = doctorDTO.Specialization,
                Phone = doctorDTO.Phone,
                Email = doctorDTO.Email,
                DepartmentId = doctorDTO.DepartmentId
            };

            await _unitOfWork.DoctorRepository.AddDoctorAsync(doctor);
        }


        public async Task DeleteDoctor(string uuid)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetDoctorByIdAsync(uuid);

            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found");
            }

            await _unitOfWork.DoctorRepository.DeleteDoctorAsync(uuid);

        }
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartments()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllDepartmentsAsync();
            return departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            });
        }

        public  async Task<DoctorUpdateDto?> GetDoctorById(string uuid)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetDoctorByIdAsync(uuid);
            if (doctor == null)
            {
                return null;
            }

           return new DoctorUpdateDto
            {
                Id = doctor.Id,
                Uuid = doctor.Uuid,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                Phone = doctor.Phone,
                Email = doctor.Email,
                DepartmentId = doctor.DepartmentId
            };
        }

        public async Task UpdateDoctor(DoctorUpdateDto doctorDTO)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetDoctorByIdAsync(doctorDTO.Uuid);

            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found");
            }

            doctor.FirstName = doctorDTO.FirstName;
            doctor.LastName = doctorDTO.LastName;
            doctor.Specialization = doctorDTO.Specialization;
            doctor.Phone = doctorDTO.Phone;
            doctor.Email = doctorDTO.Email;
            doctor.DepartmentId = doctorDTO.DepartmentId;


            await _unitOfWork.DoctorRepository.UpdateDoctorAsync(doctor);
        }

    


    }
}
