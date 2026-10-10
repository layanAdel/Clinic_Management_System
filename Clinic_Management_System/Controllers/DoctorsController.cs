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
    public class DoctorsController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public DoctorsController(
            IDoctorRepository doctorRepository,
            IDepartmentRepository departmentRepository)
        {
            _doctorRepository = doctorRepository;
            _departmentRepository = departmentRepository;
        }


        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync();

            IEnumerable<DoctorDto> doctor = doctors
                .Select(d => new DoctorDto
                {
                    Id = d.Id,
                    Uuid = d.Uuid,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Specialization = d.Specialization,
                    Phone = d.Phone,
                    Email = d.Email,
                    DepartmentName = d.Department != null ? d.Department.Name : null,

                    AppointmentId = d.Appointments != null ? d.Appointments.Count() : 0,
                    MedicalRecordId = d.MedicalRecords != null ? d.MedicalRecords.Count() : 0,
                    PrescriptionId = d.Prescriptions != null ? d.Prescriptions.Count() : 0

                }).ToList();

            return View(doctor);
        }


        // Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();

            SelectList selectList = new SelectList(departments, "Id", "Name");

            ViewBag.Departments = selectList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorCreateDto doctorDto)
        {
            if (ModelState.IsValid)
            {
                var doctor = new Doctor
                {
                    FirstName = doctorDto.FirstName,
                    LastName = doctorDto.LastName,
                    Specialization = doctorDto.Specialization,
                    Phone = doctorDto.Phone,
                    Email = doctorDto.Email,
                    DepartmentId = doctorDto.DepartmentId
                };

                await _doctorRepository.AddDoctorAsync(doctor);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(
             await _departmentRepository.GetAllDepartmentsAsync(), "Id", "Name");


            return View(doctorDto);
        }


        // Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {

            if (uuid == null)
            {
                return NotFound();
            }
            var doctor = await _doctorRepository.GetDoctorByIdAsync(uuid);

            if (doctor == null)
            {
                return NotFound();
            }

            var doctorDto = new DoctorUpdateDto
            {
                Id = doctor.Id,
                Uuid = doctor.Uuid,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                Phone = doctor.Phone,
                Email = doctor.Email,
                DepartmentId = doctor.DepartmentId
            };


            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View(doctorDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorUpdateDto doctorDto)
        {
            if (ModelState.IsValid)
            {
                var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorDto.Uuid);

                if (doctor == null)
                {
                    return NotFound();
                }

                doctor.FirstName = doctorDto.FirstName;
                doctor.LastName = doctorDto.LastName;
                doctor.Specialization = doctorDto.Specialization;
                doctor.Phone = doctorDto.Phone;
                doctor.Email = doctorDto.Email;
                doctor.DepartmentId = doctorDto.DepartmentId;


                await _doctorRepository.UpdateDoctorAsync(doctor);

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(
             await _departmentRepository.GetAllDepartmentsAsync(), "Id", "Name");
            return View(doctorDto);

            }




            // Delete

            [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {

            if (uuid == null)
            {
                return NotFound();
            }

            var doctor = await _doctorRepository.GetDoctorByIdAsync(uuid);

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
            var doctor = await _doctorRepository.GetDoctorByIdAsync(uuid);

            if (doctor != null)
            {
                await _doctorRepository.DeleteDoctorAsync(uuid);
            }

            return RedirectToAction(nameof(Index));
        }

       
    }
}