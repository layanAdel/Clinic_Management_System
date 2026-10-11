using Clinic_Management_System.Data;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Clinic_Management_System.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class MedicalRecordsController : Controller
    {
       
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public MedicalRecordsController(IMedicalRecordRepository medicalRecordRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public async Task<IActionResult> Index()
        {
            var medicalRecords = await _medicalRecordRepository.GetAllMedicalRecordsAsync();

            IEnumerable <MedicalRecordDto> medicalRecord = medicalRecords
                .Select(a => new MedicalRecordDto
            {
                Id = a.Id,
                Uuid = a.Uuid,
                PatientName = a.Patient != null ? a.Patient.FirstName : null,
                DoctorName = a.Doctor != null ? a.Doctor.FirstName : null,
                VisitDate = a.VisitDate,
                Diagnosis = a.Diagnosis,
                Notes = a.Notes

            }).ToList();

            return View(medicalRecord);
        }


        // Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var patients = await _patientRepository.GetAllPatientsAsync();

            SelectList selectList = new SelectList(patients, "Id", "FirstName");
            ViewBag.Patients = selectList;



            var doctors = await _doctorRepository.GetAllDoctorsAsync();

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

                await _medicalRecordRepository.AddMedicalRecordAsync(medicalRecord);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new SelectList(
             await _patientRepository.GetAllPatientsAsync(), "Id", "FirstName");

            ViewBag.Doctors = new SelectList(
              await _doctorRepository.GetAllDoctorsAsync(), "Id", "FirstName");

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
            var med =await _medicalRecordRepository.GetMedicalRecordByIdAsync(uuid);

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

            var Patients = await _patientRepository.GetAllPatientsAsync();
            ViewBag.Patients = new SelectList(Patients, "Id", "FirstName");


            var Doctors = await _doctorRepository.GetAllDoctorsAsync();
            ViewBag.Doctors = new SelectList(Doctors, "Id", "FirstName");

            return View(medicalRecordDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicalRecordUpdateDto medicalRecordDto)
        {
            if (ModelState.IsValid)
            {
                var medicalRecord = await _medicalRecordRepository.GetMedicalRecordByIdAsync(medicalRecordDto.Uuid);

                if (medicalRecord == null)
                {
                    return NotFound();
                }

                medicalRecord.PatientId = medicalRecordDto.PatientId;
                medicalRecord.DoctorId = medicalRecordDto.DoctorId;
                medicalRecord.VisitDate = medicalRecordDto.VisitDate;
                medicalRecord.Diagnosis = medicalRecordDto.Diagnosis;
                medicalRecord.Notes = medicalRecordDto.Notes;

                await _medicalRecordRepository.UpdateMedicalRecordAsync(medicalRecord);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new SelectList(
               await _patientRepository.GetAllPatientsAsync(), "Id", "FirstName");

            ViewBag.Doctors = new SelectList(
              await _doctorRepository.GetAllDoctorsAsync(), "Id", "FirstName");

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

            var med =await _medicalRecordRepository.GetMedicalRecordByIdAsync(uuid);

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
            var med = await _medicalRecordRepository.GetMedicalRecordByIdAsync(uuid);

            if (med != null)
            {
               await _medicalRecordRepository.DeleteMedicalRecordAsync(uuid);
            }

            return RedirectToAction(nameof(Index));
        }
      

    }
}