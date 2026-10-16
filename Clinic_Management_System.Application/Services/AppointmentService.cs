using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Repositories.Base;
using Clinic_Management_System.Application.Services.Base;

namespace Clinic_Management_System.Application.Services
{
    public class AppointmentService : IAppointmentService
    {

        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointments()
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsAsync();

           return appointments
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    Uuid = a.Uuid,
                    PatientName = a.Patient != null ? a.Patient.FirstName : null,
                    DoctorName = a.Doctor != null ? a.Doctor.FirstName : null,
                    Status = a.Status,
                    Reason = a.Reason,
                    AppointmentDate = a.AppointmentDate,


                });
        }


        public async Task AddAppointment(AppointmentCreateDto appointmentDTO)
        {
            var appointment = new Appointment
            {
                PatientId = appointmentDTO.PatientId,
                DoctorId = appointmentDTO.DoctorId,
                Status = appointmentDTO.Status,
                Reason = appointmentDTO.Reason,
                AppointmentDate = appointmentDTO.AppointmentDate,
            };


            await _unitOfWork.AppointmentRepository.AddAppointmentAsync(appointment);
        }

        public async Task DeleteAppointment(string uuid)
        {
            var appo = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(uuid);

            if (appo == null)
            {
                throw new KeyNotFoundException("Appointment not found");
            }

            await _unitOfWork.AppointmentRepository.DeleteAppointmentAsync(uuid);
        }

        public async Task<AppointmentUpdateDto?> GetAppointmentById(string uuid)
        {
            var appo = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(uuid);

            if (appo == null)
            {
                return null;
            }

            return new AppointmentUpdateDto
            {
                Id = appo.Id,
                Uuid = appo.Uuid,
                PatientId = appo.PatientId,
                DoctorId = appo.DoctorId,
                AppointmentDate = appo.AppointmentDate,
                Status = appo.Status,
                Reason = appo.Reason
            };
        }

        public async Task UpdateAppointment(AppointmentUpdateDto appointmentDTO)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(appointmentDTO.Uuid);


            if (appointment == null)
            {
                throw new KeyNotFoundException("appointment not found");
            }

            appointment.PatientId = appointmentDTO.PatientId;
            appointment.DoctorId = appointmentDTO.DoctorId;
            appointment.AppointmentDate = appointmentDTO.AppointmentDate;
            appointment.Status = appointmentDTO.Status;
            appointment.Reason = appointmentDTO.Reason;

            await _unitOfWork.AppointmentRepository.UpdateAppointmentAsync(appointment);
        }


        public async Task<IEnumerable<DoctorDto>> GetAllDoctors()
        {
            var doctors = await _unitOfWork.DoctorRepository.GetAllDoctorsAsync();
            return doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                FirstName = d.FirstName
            });
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatients()
        {
            var patients = await _unitOfWork.PatientRepository.GetAllPatientsAsync();
             return patients.Select(p => new PatientDto {
                 Id = p.Id,
                 FirstName = p.FirstName
             });

        }
    }
}
