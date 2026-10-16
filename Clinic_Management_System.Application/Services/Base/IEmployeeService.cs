using Clinic_Management_System.Application.Dtos;

namespace Clinic_Management_System.Application.Services.Base
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();

        Task<EmployeeUpdateDto?> GetEmployeeById(string uuid);

        Task<IEnumerable<DepartmentDto>> GetAllDepartments();

        Task AddEmployee(EmployeeCreateDto employeeDTO);

        Task UpdateEmployee(EmployeeUpdateDto employeeDTO);

        Task DeleteEmployee(string uuid);
    }
}
