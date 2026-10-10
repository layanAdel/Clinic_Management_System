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
    public class DepartmentsController : Controller
    {
        //private readonly AppDbContext _db;

        //public DepartmentsController(AppDbContext db)
        //{
        //    _db = db;
        //}
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }


        public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();

            IEnumerable<DepartmentDto> department = departments
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Uuid = d.Uuid,
                    Name = d.Name,
                    Description = d.Description,
                    DoctorId = d.Doctors != null ? d.Doctors.Count() : 0,
                    EmployeeId = d.Employees != null ? d.Employees.Count() : 0

                }).ToList();

            return View(department);
        }


        // Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentCreateDto departmentDto)
        {
            if (ModelState.IsValid)
            {
           
                var department = new Department
                {
                    Name = departmentDto.Name,
                    Description = departmentDto.Description
                };

                await _departmentRepository.AddDepartmentAsync(department);
                //_db.Add(department);
                //await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentDto);
        }


        // Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }
            var department = await _departmentRepository.GetDepartmentByIdAsync(uuid);

            if (department == null)
            {
                return NotFound();
            }
            var departmentDto = new DepartmentUpdateDto
            {

                Id = department.Id,
                Uuid = department.Uuid,
                Name = department.Name,
                Description = department.Description
            };




            return View(departmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentUpdateDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                //var department = await _db.Departments
                //    .FirstOrDefaultAsync(x => x.Uuid == departmentDto.Uuid);

                var department = await _departmentRepository.GetDepartmentByIdAsync(departmentDto.Uuid);

                if (department == null)
                {
                    return NotFound();
                }

                department.Name = departmentDto.Name;
                department.Description = departmentDto.Description;


             await _departmentRepository.UpdateDepartmentAsync(department);
                return RedirectToAction(nameof(Index));
            }
            return View(departmentDto);
        }



        // Delete

        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }
            var department = await _departmentRepository.GetDepartmentByIdAsync(uuid);

            //var department = await _db.Departments.FirstOrDefaultAsync(x => x.Uuid == uuid);


            if (department == null)
            {
                return NotFound();
            }


            return View(department);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(string uuid)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(uuid);

            if (department != null)
            {
                await _departmentRepository.DeleteDepartmentAsync(uuid);
                //_db.Departments.Remove(department);
                //await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }
    }
}