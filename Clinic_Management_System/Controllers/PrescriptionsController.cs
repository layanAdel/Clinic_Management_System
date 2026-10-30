using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Clinic_Management_System.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class PrescriptionsController : Controller
    {
        private readonly IPrescriptionService _prescriptionService;
        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;

        }

        public async Task<IActionResult> Index()
        {
            var prescriptions = await _prescriptionService.GetAllPrescriptions();

            return View(prescriptions);
        }
        

        // Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            await LoadDoctorAndPatientsAsync();

            return View(new PrescriptionCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrescriptionCreateDto prescriptionDTO, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    prescriptionDTO.PrescriptionImage = UploadImage(image);
                }

                await _prescriptionService.AddPrescription(prescriptionDTO);

                return RedirectToAction(nameof(Index));
        }

            await LoadDoctorAndPatientsAsync();

            return View(prescriptionDTO);
        }

        private string UploadImage(IFormFile image)
        {
            string fileName = Guid.NewGuid().ToString()
                              + Path.GetExtension(image.FileName);


            string folderPath = Path.Combine(
              Directory.GetCurrentDirectory(),
              "wwwroot",
              "images",
              "prescription"
            );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return "/images/prescription/" + fileName;
        }


        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }
            var pre = await _prescriptionService.GetPrescriptionById(uuid);

            if (pre == null)
            {
                return NotFound();
            }

            await LoadDoctorAndPatientsAsync();

            return View(pre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PrescriptionUpdateDto prescriptionDTO)
        {
            if (ModelState.IsValid)
            {
                var existingPrescription = 
                    await _prescriptionService.GetPrescriptionById(prescriptionDTO.Uuid);


                if (existingPrescription == null)
                {
                    return NotFound();
                }

               
                await _prescriptionService.UpdatePrescription(prescriptionDTO);    

                return RedirectToAction(nameof(Index));
            }

            await LoadDoctorAndPatientsAsync();

            return View(prescriptionDTO);
        }


        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {

            if (uuid == null)
            {
                return NotFound();
            }
            var pre = await _prescriptionService.GetPrescriptionById(uuid);

            if (pre == null)
            {
                return NotFound();
            }

            return View(pre);
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

            var pre = await _prescriptionService.GetPrescriptionById(uuid);

            if (pre == null)
            {
                return NotFound();
            }
            await _prescriptionService.DeletePrescription(uuid);


            return RedirectToAction(nameof(Index));
        }


        private async Task LoadDoctorAndPatientsAsync()
        {
            var doctors = await _prescriptionService.GetAllDoctors();

            ViewBag.Doctors = new SelectList(
                doctors,
                "Id",
                "FirstName"
            );

            var patients = await _prescriptionService.GetAllPatients();

            ViewBag.Patients = new SelectList(
                patients,
                "Id",
                "FirstName"
             );
        }
    }
}