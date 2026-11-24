using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {

        private readonly IDepartmentService _departmentService;
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;

        }

        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartments();

            return View(departments);
        }


        // Create

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DepartmentCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentCreateDto departmentDTO)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.AddDepartment(departmentDTO);
                
                return RedirectToAction(nameof(Index));
            }
            return View(departmentDTO);
        }


        // Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }
            var department = await _departmentService.GetDepartmentById(uuid);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentUpdateDto departmentDTO)
        {
            if (ModelState.IsValid)
            {
                
                var existingDepartment =
                    await _departmentService.GetDepartmentById(departmentDTO.Uuid);

                if (existingDepartment == null)
                {
                    return NotFound();
                }

             await _departmentService.UpdateDepartment(departmentDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(departmentDTO);
        }



        // Delete

        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }
            var department = await _departmentService.GetDepartmentById(uuid);


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

            if (uuid == null)
            {
                return NotFound();
            }
            var department = await _departmentService.GetDepartmentById(uuid);

            if (department == null)
            {
                return NotFound();
            }

                await _departmentService.DeleteDepartment(uuid);
             
            return RedirectToAction(nameof(Index));
        }
    }
}