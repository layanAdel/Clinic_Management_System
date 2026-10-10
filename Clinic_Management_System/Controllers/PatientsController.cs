using Clinic_Management_System.Data;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly AppDbContext _db;

        public PatientsController(AppDbContext db)
        {
            _db = db;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<PatientDto> patients = await _db.Patients
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
                   AppointmentId = p.Appointments.Count(),
                   MedicalRecordId = p.MedicalRecords.Count(),
                   PrescriptionId = p.Prescriptions.Count(),
                   InvoiceId = p.Invoices.Count()

               }).ToListAsync();

            return View(patients);
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
                _db.Patients.Add(patient);
               await _db.SaveChangesAsync();
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

            var patient = await _db.Patients.FirstOrDefaultAsync(x => x.Uuid == uuid);

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
                var patient = await _db.Patients
                    .FirstOrDefaultAsync(x => x.Uuid == patientDto.Uuid);

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

                await _db.SaveChangesAsync();

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
            var patient = await _db.Patients.FirstOrDefaultAsync(x => x.Uuid == uuid);

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
            var patient =await _db.Patients.FirstOrDefaultAsync(x => x.Uuid == uuid);

            if (patient != null)
            {
                _db.Patients.Remove(patient);
                await _db.SaveChangesAsync();
            }

           

            return RedirectToAction(nameof(Index));
        }
    }
}