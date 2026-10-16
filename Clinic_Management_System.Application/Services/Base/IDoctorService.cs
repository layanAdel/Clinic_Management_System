using Clinic_Management_System.Application.Dtos;

namespace Clinic_Management_System.Application.Services.Base
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctors();
        Task<DoctorUpdateDto?> GetDoctorById(string uuid);
        Task AddDoctor(DoctorCreateDto doctorDTO);
        Task UpdateDoctor(DoctorUpdateDto doctorDTO);
        Task DeleteDoctor(string uuid);
        Task<IEnumerable<DepartmentDto>> GetAllDepartments();
       
    }
}
