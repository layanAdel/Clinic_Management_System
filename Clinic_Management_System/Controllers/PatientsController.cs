using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;

        }


        public async Task<IActionResult> Index()
        {
            var patients = await _patientService.GetAllPatients();

            return View(patients);
        }
        


        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new PatientCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(PatientCreateDto patientDTO)
        {

            if (ModelState.IsValid)
            {
                await _patientService.AddPatient(patientDTO);

                return RedirectToAction(nameof(Index));
            }
            return View(patientDTO);
        }


        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var patient = await _patientService.GetPatientById(uuid);

            if (patient == null)
            {
                return NotFound();
            }
          

            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientUpdateDto patientDTO)
        {
            if (ModelState.IsValid)
            {
                var existingPatient = 
                    await _patientService.GetPatientById(patientDTO.Uuid);

                if (existingPatient == null)
                {
                    return NotFound();
                }

                 await _patientService.UpdatePatient(patientDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(patientDTO);
        }



        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }
            var patient = await _patientService.GetPatientById(uuid);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
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

            var patient = await _patientService.GetPatientById(uuid);

            if (patient == null)
            {
                return NotFound();
            }

            await _patientService.DeletePatient(uuid);

            return RedirectToAction(nameof(Index));
        }
    }
}