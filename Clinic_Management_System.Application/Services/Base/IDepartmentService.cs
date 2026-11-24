using Clinic_Management_System.Application.Dtos;

namespace Clinic_Management_System.Application.Services.Base
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartments();

        Task<DepartmentUpdateDto?> GetDepartmentById(string uuid);
        Task AddDepartment(DepartmentCreateDto departmentDTO);

        Task UpdateDepartment(DepartmentUpdateDto departmentDTO);

        Task DeleteDepartment(string uuid);
    }
}
