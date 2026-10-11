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
    public class EmployeesController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public EmployeesController(IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();

            IEnumerable<EmployeeDto> employee = employees
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Uuid = e.Uuid,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Position = e.Position,
                    Phone = e.Phone,
                    Email = e.Email,
                    HireDate = e.HireDate,
                    Salary = e.Salary,
                    DepartmentName = e.Department != null ? e.Department.Name : null

                }).ToList();

            return View(employee);
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
        public async Task<IActionResult> Create(EmployeeCreateDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Position = employeeDto.Position,
                    Phone = employeeDto.Phone,
                    Email = employeeDto.Email,
                    HireDate = employeeDto.HireDate,
                    Salary = employeeDto.Salary,
                    DepartmentId = employeeDto.DepartmentId
                };


                await _employeeRepository.AddEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(
                await _departmentRepository.GetAllDepartmentsAsync(), "Id", "Name");

            return View(employeeDto);
        }


        // Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var emp = await _employeeRepository.GetEmployeeByIdAsync(uuid);

            if (emp == null)
            {
                return NotFound();
            }
            var employeeDto = new EmployeeUpdateDto
            {
                Id = emp.Id,
                Uuid = emp.Uuid,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Position = emp.Position,
                Phone = emp.Phone,
                Email = emp.Email,
                HireDate = emp.HireDate,
                Salary = emp.Salary,
                DepartmentId = emp.DepartmentId
            };

            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View(employeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeUpdateDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeDto.Uuid);

                if (employee == null)
                {
                    return NotFound();
                }

                employee.FirstName = employeeDto.FirstName;
                employee.LastName = employeeDto.LastName;
                employee.Position = employeeDto.Position;
                employee.Phone = employeeDto.Phone;
                employee.Email = employeeDto.Email;
                employee.HireDate = employeeDto.HireDate;
                employee.Salary = employeeDto.Salary;
                employee.DepartmentId = employeeDto.DepartmentId;

                await _employeeRepository.UpdateEmployeeAsync(employee);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(
                await _departmentRepository.GetAllDepartmentsAsync(), "Id", "Name");

            return View(employeeDto);
        }



        // Delete

        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }
            var emp = await _employeeRepository.GetEmployeeByIdAsync(uuid);


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
            var emp = await _employeeRepository.GetEmployeeByIdAsync(uuid);


            if (emp != null)
            {
                await _employeeRepository.DeleteEmployeeAsync(uuid);
            }


            return RedirectToAction(nameof(Index));
        }

       
    }
}