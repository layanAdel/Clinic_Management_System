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
    public class PrescriptionsController : Controller
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public PrescriptionsController(IPrescriptionRepository prescriptionRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public async Task<IActionResult> Index()
        {
            var prescriptions = await _prescriptionRepository.GetAllPrescriptionsAsync();

            IEnumerable<PrescriptionDto> prescription = prescriptions
                .Select(p => new PrescriptionDto
                {
                    Id = p.Id,
                    Uuid = p.Uuid,
                    PatientName = p.Patient != null ? p.Patient.FirstName : null,
                    DoctorName = p.Doctor != null ? p.Doctor.FirstName : null,
                    Medication = p.Medication,
                    Dosage = p.Dosage,
                    Instructions = p.Instructions,
                    PrescriptionDate = p.PrescriptionDate

                }).ToList();

            return View(prescription);
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
        public async Task<IActionResult> Create(PrescriptionCreateDto prescriptionDto)
        {
            if (ModelState.IsValid)
            {

                var prescription = new Prescription
                {
                    PatientId = prescriptionDto.PatientId,
                    DoctorId = prescriptionDto.DoctorId,
                    Medication = prescriptionDto.Medication,
                    Dosage = prescriptionDto.Dosage,
                    Instructions = prescriptionDto.Instructions,
                    PrescriptionDate = prescriptionDto.PrescriptionDate
                };


              
                await _prescriptionRepository.AddPrescriptionAsync(prescription);

                return RedirectToAction(nameof(Index));
        }

            ViewBag.Patients = new SelectList(
                 await _patientRepository.GetAllPatientsAsync(), "Id", "FirstName");

            ViewBag.Doctors = new SelectList(
              await _doctorRepository.GetAllDoctorsAsync(), "Id", "FirstName");

            return View(prescriptionDto);
        }


        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }
            var pre = await _prescriptionRepository.GetPrescriptionByIdAsync(uuid);

            if (pre == null)
            {
                return NotFound();
            }

            var prescriptionDto = new PrescriptionUpdateDto
            {
                Id = pre.Id,
                Uuid = pre.Uuid,
                PatientId = pre.PatientId,
                DoctorId = pre.DoctorId,
                Medication = pre.Medication,
                Dosage = pre.Dosage,
                Instructions = pre.Instructions,
                PrescriptionDate = pre.PrescriptionDate
            };


            var Patients = await _patientRepository.GetAllPatientsAsync();
            ViewBag.Patients = new SelectList(Patients, "Id", "FirstName");


            var Doctors = await _doctorRepository.GetAllDoctorsAsync();
            ViewBag.Doctors = new SelectList(Doctors, "Id", "FirstName");

            return View(prescriptionDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PrescriptionUpdateDto prescriptionDto)
        {
            if (ModelState.IsValid)
            {
                var prescription = await _prescriptionRepository.GetPrescriptionByIdAsync(prescriptionDto.Uuid);


                if (prescription == null)
                {
                    return NotFound();
                }

                prescription.PatientId = prescriptionDto.PatientId;
                prescription.DoctorId = prescriptionDto.DoctorId;
                prescription.Medication = prescriptionDto.Medication;
                prescription.Dosage = prescriptionDto.Dosage;
                prescription.Instructions = prescriptionDto.Instructions;
                prescription.PrescriptionDate = prescriptionDto.PrescriptionDate;

                await _prescriptionRepository.UpdatePrescriptionAsync(prescription);    

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new SelectList(
              await _patientRepository.GetAllPatientsAsync(), "Id", "FirstName");

            ViewBag.Doctors = new SelectList(
              await _doctorRepository.GetAllDoctorsAsync(), "Id", "FirstName");

            return View(prescriptionDto);
        }


        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {

            if (uuid == null)
            {
                return NotFound();
            }
            var pre = await _prescriptionRepository.GetPrescriptionByIdAsync(uuid);

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
            var pre = await _prescriptionRepository.GetPrescriptionByIdAsync(uuid);

            if (pre == null)
            {
                return NotFound();
            }
            await _prescriptionRepository.DeletePrescriptionAsync(uuid);


            return RedirectToAction(nameof(Index));
        }
        


    }
}