using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Repositories.Base;
using Clinic_Management_System.Application.Services.Base;

namespace Clinic_Management_System.Application.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllEmployeesAsync();

           return employees
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

                });
        }


        public async Task AddEmployee(EmployeeCreateDto employeeDTO)
        {

            var employee = new Employee
            {
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                Position = employeeDTO.Position,
                Phone = employeeDTO.Phone,
                Email = employeeDTO.Email,
                HireDate = employeeDTO.HireDate,
                Salary = employeeDTO.Salary,
                DepartmentId = employeeDTO.DepartmentId
            };
            await _unitOfWork.EmployeeRepository.AddEmployeeAsync(employee);
        }

        public async Task DeleteEmployee(string uuid)
        {
            var emp = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(uuid);

            if (emp == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }

            await _unitOfWork.EmployeeRepository
              .DeleteEmployeeAsync(uuid);

        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartments()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllDepartmentsAsync();
            return departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            });

        }

 

        //Get by id 
        public async Task<EmployeeUpdateDto?> GetEmployeeById(string uuid)
        {
            var emp = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(uuid);
            
            if (emp == null)
            {
                return null;
            }

            return new EmployeeUpdateDto
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
        }

        public async Task UpdateEmployee(EmployeeUpdateDto employeeDTO)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(employeeDTO.Uuid);

            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }
            employee.FirstName = employeeDTO.FirstName;
            employee.LastName = employeeDTO.LastName;
            employee.Position = employeeDTO.Position;
            employee.Phone = employeeDTO.Phone;
            employee.Email = employeeDTO.Email;
            employee.HireDate = employeeDTO.HireDate;
            employee.Salary = employeeDTO.Salary;
            employee.DepartmentId = employeeDTO.DepartmentId;

            await _unitOfWork.EmployeeRepository.UpdateEmployeeAsync(employee);


        }
    }
}
