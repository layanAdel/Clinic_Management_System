using Clinic_Management_System.Data;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Clinic_Management_System.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Net.NetworkInformation;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentsController(IAppointmentRepository appointmentRepository,
            IDoctorRepository doctorRepository, 
            IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }


        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();

            IEnumerable <AppointmentDto> appointment = appointments
                .Select(a => new AppointmentDto
            {
                Id = a.Id,
                Uuid = a.Uuid,
                PatientName = a.Patient != null ? a.Patient.FirstName : null,
                DoctorName = a.Doctor != null ? a.Doctor.FirstName : null,
                Status = a.Status,
                Reason = a.Reason,
                AppointmentDate = a.AppointmentDate,


            }).ToList();

            return View(appointment);
        }

            // Create
            [HttpGet]
        public async Task<IActionResult> Create()
        {

            var patients = await _patientRepository.GetAllPatientsAsync();

            SelectList selectList = new SelectList(patients, "Id", "FirstName");
            ViewBag.Patients = selectList;



            var doctors =await _doctorRepository.GetAllDoctorsAsync();

            SelectList selectListD = new SelectList(doctors, "Id", "FirstName");
            ViewBag.Doctors = selectListD;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(AppointmentCreateDto appointmentDto)
        {
            if (ModelState.IsValid)
            {
                    var appointment = new Appointment
                    {
                        PatientId = appointmentDto.PatientId,
                        DoctorId = appointmentDto.DoctorId,
                        Status = appointmentDto.Status,
                        Reason = appointmentDto.Reason,
                        AppointmentDate = appointmentDto.AppointmentDate,
                    };


               await _appointmentRepository.AddAppointmentAsync(appointment);

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Patients = new SelectList(
               await _patientRepository.GetAllPatientsAsync(), "Id", "FirstName");

            ViewBag.Doctors = new SelectList(
              await _doctorRepository.GetAllDoctorsAsync(), "Id", "FirstName");
            return View(appointmentDto);
        }


        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var appo = await _appointmentRepository.GetAppointmentByIdAsync(uuid);

            if (appo == null)
            {
                return NotFound();
            }
            var appointmentDto = new AppointmentUpdateDto
            {
                Id = appo.Id,
                Uuid = appo.Uuid,
                PatientId = appo.PatientId,
                DoctorId = appo.DoctorId,
                AppointmentDate = appo.AppointmentDate,
                Status = appo.Status,
                Reason = appo.Reason
            };

            var Patients = await _patientRepository.GetAllPatientsAsync();
            ViewBag.Patients = new SelectList(Patients, "Id", "FirstName");


            var Doctors = await _doctorRepository.GetAllDoctorsAsync();
            ViewBag.Doctors = new SelectList(Doctors, "Id", "FirstName");

            return View(appointmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(AppointmentUpdateDto appointmentDto)
        {
          

            if (ModelState.IsValid)
            {
                var appointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentDto.Uuid);


                if (appointment == null)
                    {
                        return NotFound();
                    }

                        appointment.PatientId = appointmentDto.PatientId;
                        appointment.DoctorId = appointmentDto.DoctorId;
                        appointment.AppointmentDate = appointmentDto.AppointmentDate;
                        appointment.Status = appointmentDto.Status;
                        appointment.Reason = appointmentDto.Reason;

                await _appointmentRepository.UpdateAppointmentAsync(appointment);

                return RedirectToAction(nameof(Index));
                    }


            ViewBag.Patients = new SelectList(
               await _patientRepository.GetAllPatientsAsync(), "Id", "FirstName");

            ViewBag.Doctors = new SelectList(
              await _doctorRepository.GetAllDoctorsAsync(), "Id", "FirstName");

            return View(appointmentDto);
        }


        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var appo = await _appointmentRepository.GetAppointmentByIdAsync(uuid);

            if (appo == null)
            {
                return NotFound();
            }
         

            return View(appo);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirm(string uuid)
        {
            var appo = await _appointmentRepository.GetAppointmentByIdAsync(uuid);

            if (appo == null)
            {
                    return NotFound();
                }

            await _appointmentRepository.DeleteAppointmentAsync(uuid);

                return RedirectToAction(nameof(Index));
        }
       

    }
}