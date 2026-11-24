using Clinic_Management_System.Infrastructure.Data;
using Clinic_Management_System.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Clinic_Management_System.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _db;

        public EmployeeRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task AddEmployeeAsync(Employee employee)
        {

            _db.Employees.Add(employee);
            return _db.SaveChangesAsync();   
         }

        public async Task DeleteEmployeeAsync(string uuid)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(d => d.Uuid == uuid);

            if (employee != null)
            {
                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _db.Employees
        .Include(e => e.Department)
        .ToListAsync();

            return employees;
        }

        public async Task<Employee?> GetEmployeeByIdAsync(string uuid)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(d => d.Uuid == uuid);
            return employee;
        }

        public Task UpdateEmployeeAsync(Employee employee)
        {
            _db.Employees.Update(employee);
            return _db.SaveChangesAsync();
        }
    }
}
