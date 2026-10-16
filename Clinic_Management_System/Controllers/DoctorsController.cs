using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {


        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;

        }


        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAllDoctors();

            return View(doctors);
        }


        // Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDepartmentsAsync();

            return View(new DoctorCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorCreateDto doctorDTO)
        {
            if (ModelState.IsValid)
            {
           
                await _doctorService.AddDoctor(doctorDTO);

                return RedirectToAction(nameof(Index));
            }
            await LoadDepartmentsAsync();

            return View(doctorDTO);
        }


        // Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {

            if (uuid == null)
            {
                return NotFound();
            }
            var doctor = await _doctorService.GetDoctorById(uuid);

            if (doctor == null)
            {
                return NotFound();
            }
            await LoadDepartmentsAsync();

            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorUpdateDto doctorDTO)
        {
            if (ModelState.IsValid)
            {
                var existingDoctor = 
                    await _doctorService.GetDoctorById(doctorDTO.Uuid);

                if (existingDoctor == null)
                {
                    return NotFound();
                }

                await _doctorService.UpdateDoctor(doctorDTO);

                return RedirectToAction(nameof(Index));
            }
            await LoadDepartmentsAsync();

            return View(doctorDTO);

            }




            // Delete

            [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {

            if (uuid == null)
            {
                return NotFound();
            }

            var doctor = await _doctorService.GetDoctorById(uuid);

            if (doctor == null)
            {
                return NotFound();
            }
           
            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string uuid)
        {

            if (uuid == null)
            {
                return NotFound();
            }

            var doctor = await _doctorService.GetDoctorById(uuid);

            if (doctor == null)
            {
                return NotFound();
            }

            await _doctorService.DeleteDoctor(uuid);


            return RedirectToAction(nameof(Index));
        }

        // LOAD DEPARTMENTS
        private async Task LoadDepartmentsAsync()
        {
            var departments = await _doctorService.GetAllDepartments();

            ViewBag.Departments = new SelectList(
                departments,
                "Id",
                "Name"
            );
        }


    }
}