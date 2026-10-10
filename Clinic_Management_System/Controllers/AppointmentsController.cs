using Clinic_Management_System.Data;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
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
        private readonly AppDbContext _db;

        public AppointmentsController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {

            IEnumerable<AppointmentDto> appointment = _db.Appointments.Select(a => new AppointmentDto
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
        public IActionResult Create()
        {

            var patients = _db.Patients.ToList();

            SelectList selectList = new SelectList(patients, "Id", "FirstName");
            ViewBag.Patients = selectList;

            var doctors = _db.Doctors.ToList();

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
                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Patients = new SelectList(
              await _db.Patients.ToListAsync(), "Id", "FirstName");

            ViewBag.Doctors = new SelectList(
              await _db.Doctors.ToListAsync(), "Id", "FirstName");
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

            var appo = await _db.Appointments.FirstOrDefaultAsync(x => x.Uuid == uuid);

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

            var Patients = await _db.Patients.ToListAsync();
            ViewBag.Patients = new SelectList(Patients, "Id", "FirstName");


            var Doctors = await _db.Doctors.ToListAsync();
            ViewBag.Doctors = new SelectList(Doctors, "Id", "FirstName");

            return View(appointmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(AppointmentUpdateDto appointmentDto)
        {
          

            if (ModelState.IsValid)
            {
                    var appointment = await _db.Appointments
                        .FirstOrDefaultAsync(x => x.Uuid == appointmentDto.Uuid);
                    
                    if (appointment == null)
                    {
                        return NotFound();
                    }

                        appointment.PatientId = appointmentDto.PatientId;
                        appointment.DoctorId = appointmentDto.DoctorId;
                        appointment.AppointmentDate = appointmentDto.AppointmentDate;
                        appointment.Status = appointmentDto.Status;
                        appointment.Reason = appointmentDto.Reason;

                        await _db.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
            

            ViewBag.Patients = new SelectList(await _db.Patients.ToListAsync(), "Id", "FirstName");
            ViewBag.Doctors = new SelectList(await _db.Doctors.ToListAsync(), "Id", "FirstName");

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

            var appo =await _db.Appointments.FirstOrDefaultAsync(x => x.Uuid == uuid);

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
            var appo = await _db.Appointments.FirstOrDefaultAsync(x => x.Uuid == uuid);

            if (appo == null)
            {
                    return NotFound();
                }
                _db.Appointments.Remove(appo);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
        }
       

    }
}