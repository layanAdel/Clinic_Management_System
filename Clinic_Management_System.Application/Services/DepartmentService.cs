using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Repositories.Base;

namespace Clinic_Management_System.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartments()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllDepartmentsAsync();

            return departments
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Uuid = d.Uuid,
                    Name = d.Name,
                    Description = d.Description,
                    DoctorId = d.Doctors != null ? d.Doctors.Count() : 0,
                    EmployeeId = d.Employees != null ? d.Employees.Count() : 0

                });
        }

        public async Task AddDepartment(DepartmentCreateDto departmentDTO)
        {
            var department = new Department
            {
                Name = departmentDTO.Name,
                Description = departmentDTO.Description
            };

            await _unitOfWork.DepartmentRepository.AddDepartmentAsync(department);

        }

        public async Task DeleteDepartment(string uuid)
        {
            var department = await _unitOfWork.DepartmentRepository.GetDepartmentByIdAsync(uuid);
           
            if (department == null)
            {
                throw new KeyNotFoundException("Department not found");
            }

            await _unitOfWork.DepartmentRepository.DeleteDepartmentAsync(uuid);

        }



        public async Task<DepartmentUpdateDto?> GetDepartmentById(string uuid)
        {
            var department = await _unitOfWork.DepartmentRepository.GetDepartmentByIdAsync(uuid);

            if (department == null)
            {
                return null;
            }

           return new DepartmentUpdateDto
            {
                Id = department.Id,
                Uuid = department.Uuid,
                Name = department.Name,
                Description = department.Description
            };
        }

        public async Task UpdateDepartment(DepartmentUpdateDto departmentDTO)
        {

            var department = await _unitOfWork.DepartmentRepository.GetDepartmentByIdAsync(departmentDTO.Uuid);

            if (department == null)
            {
                throw new KeyNotFoundException("Department not found");
            }

            department.Name = departmentDTO.Name;
            department.Description = departmentDTO.Description;


            await _unitOfWork.DepartmentRepository.UpdateDepartmentAsync(department);
        }
    }
}
