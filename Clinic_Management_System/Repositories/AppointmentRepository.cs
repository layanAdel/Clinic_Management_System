using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly AppDbContext _db;

        public AppointmentRepository(AppDbContext db)
        {
            _db = db;
        }


        public Task AddAppointmentAsync(Appointment appointment)
        {
            _db.Appointments.Add(appointment);
            return _db.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(string uuid)
        {
            var appointment = await _db.Appointments.FirstOrDefaultAsync(d => d.Uuid == uuid);

            if (appointment != null)
            {
                _db.Appointments.Remove(appointment);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            IEnumerable<Appointment> appointments = await _db.Appointments
         .Include(a => a.Patient)
         .Include(a => a.Doctor)
         .ToListAsync();

            return appointments;
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(string uuid)
        {
            var appointment = await _db.Appointments.FirstOrDefaultAsync(d => d.Uuid == uuid);
            return appointment;
        }

        public Task UpdateAppointmentAsync(Appointment appointment)
        {
            _db.Appointments.Update(appointment);
            return _db.SaveChangesAsync();
        }
    }
}
