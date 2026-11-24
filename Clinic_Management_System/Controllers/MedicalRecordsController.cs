using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class MedicalRecordsController : Controller
    {

        private readonly IMedicalRecordService _medicalRecordService;
        public MedicalRecordsController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;

        }


        public async Task<IActionResult> Index()
        {
            var medicalRecords = await _medicalRecordService.GetAllMedicalRecords();

           
            return View(medicalRecords);
        }


        // Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDoctorAndPatientsAsync();
           
            return View(new MedicalRecordCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalRecordCreateDto medicalRecordDTO)
        {
            if (ModelState.IsValid)
            {
                await _medicalRecordService.AddMedicalRecord(medicalRecordDTO);

                return RedirectToAction(nameof(Index));
            }

            await LoadDoctorAndPatientsAsync();

            return View(medicalRecordDTO);
        }


        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var med = await _medicalRecordService.GetMedicalRecordById(uuid);

            if (med == null)
            {
                return NotFound();
            }

            await LoadDoctorAndPatientsAsync();

            return View(med);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicalRecordUpdateDto medicalRecordDTO)
        {
            if (ModelState.IsValid)
            {
                var existingMedicalRecord = 
                    await _medicalRecordService.GetMedicalRecordById(medicalRecordDTO.Uuid);

                if (existingMedicalRecord == null)
                {
                    return NotFound();
                }

                await _medicalRecordService.UpdateMedicalRecord(medicalRecordDTO);

                return RedirectToAction(nameof(Index));
            }

            await LoadDoctorAndPatientsAsync();

            return View(medicalRecordDTO);
        }


        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {

            if (uuid == null)
            {
                return NotFound();
            }

            var med =await _medicalRecordService.GetMedicalRecordById(uuid);

            if (med == null)
            {
                return NotFound();
            }

            return View(med);
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

            var med = await _medicalRecordService.GetMedicalRecordById(uuid);

            if (med == null)
            {
                return NotFound();

            }

            await _medicalRecordService.DeleteMedicalRecord(uuid);

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDoctorAndPatientsAsync()
        {
            var doctors = await _medicalRecordService.GetAllDoctors();

            ViewBag.Doctors = new SelectList(
                doctors,
                "Id",
                "FirstName"
            );

            var patients = await _medicalRecordService.GetAllPatients();

            ViewBag.Patients = new SelectList(
                patients, 
                "Id", 
                "FirstName"
             );
        }

    }
}