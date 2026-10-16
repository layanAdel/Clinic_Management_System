using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {

        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

        }


        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployees();

            return View(employees);
        }


        // Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDepartmentsAsync();

            return View(new EmployeeCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateDto employeeDTO)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployee(employeeDTO);

                return RedirectToAction(nameof(Index));
            }

            await LoadDepartmentsAsync();

            return View(employeeDTO);
        }


        // Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var emp = await _employeeService.GetEmployeeById(uuid);

            if (emp == null)
            {
                return NotFound();
            }
            await LoadDepartmentsAsync();


            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeUpdateDto employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee =
                   await _employeeService.GetEmployeeById(employeeDTO.Uuid);

                if (existingEmployee == null)
                {
                    return NotFound();
                }

                await _employeeService.UpdateEmployee(employeeDTO);
                return RedirectToAction(nameof(Index));
            }

            await LoadDepartmentsAsync();

            return View(employeeDTO);
        }



        // Delete

        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }
            var emp = await _employeeService.GetEmployeeById(uuid);


            if (emp == null)
            {
                return NotFound();
            }
           
            return View(emp);
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

            var emp = await _employeeService.GetEmployeeById(uuid);

            if (emp == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteEmployee(uuid);

            return RedirectToAction(nameof(Index));
        }

        // LOAD DEPARTMENTS
        private async Task LoadDepartmentsAsync()
        {
            var departments = await _employeeService.GetAllDepartments();

            ViewBag.Departments = new SelectList(
                departments,
                "Id",
                "Name"
            );
        }



    }
}