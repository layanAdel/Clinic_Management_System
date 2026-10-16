using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {

        private readonly IAppointmentService _appointmentService;
        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;

        }


        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAppointments();

            return View(appointments);
        }

            // Create
            [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDoctorAndPatientsAsync();

            return View(new AppointmentCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(AppointmentCreateDto appointmentDTO)
       {
            if (ModelState.IsValid)
            {
           
               await _appointmentService.AddAppointment(appointmentDTO);

                return RedirectToAction(nameof(Index));
            }

            await LoadDoctorAndPatientsAsync();

            return View(appointmentDTO);
        }


        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var appo = await _appointmentService.GetAppointmentById(uuid);

            if (appo == null)
            {
                return NotFound();
            }

            await LoadDoctorAndPatientsAsync();

            return View(appo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentUpdateDto appointmentDTO)
        {

            if (ModelState.IsValid)
            {
                var existingAppointment = 
                    await _appointmentService.GetAppointmentById(appointmentDTO.Uuid);

                if (existingAppointment == null)
                {
                    return NotFound();
                }
                await _appointmentService.UpdateAppointment(appointmentDTO);

                return RedirectToAction(nameof(Index));
             }

            await LoadDoctorAndPatientsAsync();

            return View(appointmentDTO);
        }


        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var appo = await _appointmentService.GetAppointmentById(uuid);

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
            if (uuid == null)
            {
                return NotFound();
            }

            var appo = await _appointmentService.GetAppointmentById(uuid);

            if (appo == null)
            {
                  return NotFound();
            }

            await _appointmentService.DeleteAppointment(uuid);

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDoctorAndPatientsAsync()
        {
            var doctors = await _appointmentService.GetAllDoctors();

            ViewBag.Doctors = new SelectList(
                doctors,
                "Id",
                "FirstName"
            );

            var patients = await _appointmentService.GetAllPatients();

            ViewBag.Patients = new SelectList(
                patients, "Id", "FirstName"
             );
        }

    }
}