using Clinic_Management_System.Infrastructure.Data;
using Clinic_Management_System.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {


        private readonly AppDbContext _db;

        public DepartmentRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task AddDepartmentAsync(Department department)
        {
            _db.Departments.Add(department);
            return _db.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(string uuid)
        {
            var department = await _db.Departments.FirstOrDefaultAsync(d => d.Uuid == uuid);

            if (department != null)
            {
                _db.Departments.Remove(department);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            IEnumerable<Department> departments = await _db.Departments
                   .Include(d => d.Doctors)
                   .Include(d => d.Employees)
                   .ToListAsync();
            
            return departments;
        }

        public async Task<Department?> GetDepartmentByIdAsync(string uuid)
        {
            var department = await _db.Departments.FirstOrDefaultAsync(d => d.Uuid == uuid);
            return department;
        }

        public Task UpdateDepartmentAsync(Department department)
        {
            _db.Departments.Update(department);
            return _db.SaveChangesAsync();
        }
    }
}