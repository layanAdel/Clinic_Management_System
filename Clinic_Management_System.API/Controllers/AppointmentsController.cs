using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = (await _appointmentService.GetAllAppointments())
                .Select(a => new
                {
                    a.Id,
                    a.Uuid,
                    a.PatientName,
                    a.DoctorName,
                    a.AppointmentDate,
                    a.Status,
                    a.Reason
                });

            return Ok(appointments);
        }
    }
}