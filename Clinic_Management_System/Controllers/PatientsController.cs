using Clinic_Management_System.Data;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Clinic_Management_System.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        public PatientsController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }


        public async Task<IActionResult> Index()
        {
            var patients = await _patientRepository.GetAllPatientsAsync();

            IEnumerable<PatientDto> patient = patients
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

               }).ToList();

            return View(patient);
        }
        


        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(PatientCreateDto patientDto)
        {

            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    FirstName = patientDto.FirstName,
                    LastName = patientDto.LastName,
                    BirthDate = patientDto.BirthDate,
                    Gender = patientDto.Gender,
                    Phone = patientDto.Phone,
                    Email = patientDto.Email
                };
                
                await _patientRepository.AddPatientAsync(patient);
                return RedirectToAction(nameof(Index));
            }
            return View(patientDto);
        }


        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var patient = await _patientRepository.GetPatientByIdAsync(uuid);

            if (patient == null)
            {
                return NotFound();
            }
            var patientDto = new PatientUpdateDto
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

            return View(patientDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientUpdateDto patientDto)
        {
            if (ModelState.IsValid)
            {
                var patient = await _patientRepository.GetPatientByIdAsync(patientDto.Uuid);

                if (patient == null)
                {
                    return NotFound();
                }

                patient.FirstName = patientDto.FirstName;
                patient.LastName = patientDto.LastName;
                patient.BirthDate = patientDto.BirthDate;
                patient.Gender = patientDto.Gender;
                patient.Phone = patientDto.Phone;
                patient.Email = patientDto.Email;

                await _patientRepository.UpdatePatientAsync(patient);

                return RedirectToAction(nameof(Index));
            }

            return View(patientDto);
        }



        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }
            var patient = await _patientRepository.GetPatientByIdAsync(uuid);

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
            var patient = await _patientRepository.GetPatientByIdAsync(uuid);

            if (patient != null)
            {

                await _patientRepository.DeletePatientAsync(uuid);
            }

           

            return RedirectToAction(nameof(Index));
        }
    }
}