using Clinic_Management_System.Data;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class MedicalRecordsController : Controller
    {
        private readonly AppDbContext _db;

        public MedicalRecordsController(AppDbContext db)
        {
            _db = db;
        }



        public async Task<IActionResult> Index()
        {
            IEnumerable<MedicalRecordDto> med = await _db.MedicalRecords.Select(a => new MedicalRecordDto
            {
                Id = a.Id,
                Uuid = a.Uuid,
                PatientName = a.Patient != null ? a.Patient.FirstName : null,
                DoctorName = a.Doctor != null ? a.Doctor.FirstName : null,
                VisitDate = a.VisitDate,
                Diagnosis = a.Diagnosis,
                Notes = a.Notes
            }).ToListAsync();

            return View(med);
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
        public async Task<IActionResult> Create(MedicalRecordCreateDto medicalRecordDto)
        {
            if (ModelState.IsValid)
            {
                var medicalRecord = new MedicalRecord
                {
                    PatientId = medicalRecordDto.PatientId,
                    DoctorId = medicalRecordDto.DoctorId,
                    VisitDate = medicalRecordDto.VisitDate,
                    Diagnosis = medicalRecordDto.Diagnosis,
                    Notes = medicalRecordDto.Notes,
                };

                _db.MedicalRecords.Add(medicalRecord);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new SelectList( 
                await _db.Patients.ToListAsync(), "Id", "FirstName");

            ViewBag.Doctors = new SelectList(
              await _db.Doctors.ToListAsync(), "Id", "FirstName");
            
            return View(medicalRecordDto);
        }


        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }
            var med =await _db.MedicalRecords.FirstOrDefaultAsync(x => x.Uuid == uuid);

            if (med == null)
            {
                return NotFound();
            }

            var medicalRecordDto = new MedicalRecordUpdateDto
            {
                Id = med.Id,
                Uuid = med.Uuid,
                PatientId = med.PatientId,
                DoctorId = med.DoctorId,
                VisitDate = med.VisitDate,
                Diagnosis = med.Diagnosis,
                Notes = med.Notes,
            };

            var Patients = await _db.Patients.ToListAsync();
            ViewBag.Patients = new SelectList(Patients, "Id", "FirstName");


            var Doctors = await _db.Doctors.ToListAsync();
            ViewBag.Doctors = new SelectList(Doctors, "Id", "FirstName");

            return View(medicalRecordDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicalRecordUpdateDto medicalRecordDto)
        {
            if (ModelState.IsValid)
            {
                var medicalRecord = await _db.MedicalRecords
                    .FirstOrDefaultAsync(x => x.Uuid == medicalRecordDto.Uuid);

                if (medicalRecord == null)
                {
                    return NotFound();
                }

                medicalRecord.PatientId = medicalRecordDto.PatientId;
                medicalRecord.DoctorId = medicalRecordDto.DoctorId;
                medicalRecord.VisitDate = medicalRecordDto.VisitDate;
                medicalRecord.Diagnosis = medicalRecordDto.Diagnosis;
                medicalRecord.Notes = medicalRecordDto.Notes;

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new SelectList(
                await _db.Patients.ToListAsync(), "Id", "FirstName");

            ViewBag.Doctors = new SelectList(
                await _db.Doctors.ToListAsync(), "Id", "FirstName");

            return View(medicalRecordDto);
        }


        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {

            if (uuid == null)
            {
                return NotFound();
            }

            var med =await _db.MedicalRecords.FirstOrDefaultAsync(x => x.Uuid == uuid);

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
            var med = await _db.MedicalRecords.FirstOrDefaultAsync(x => x.Uuid == uuid);

            if (med != null)
            {
                _db.MedicalRecords.Remove(med);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
      

    }
}