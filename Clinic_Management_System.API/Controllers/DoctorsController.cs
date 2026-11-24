using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = (await _doctorService.GetAllDoctors())
                .Select(d => new
                {
                    d.Id,
                    d.Uuid,
                    d.FirstName,
                    d.LastName,
                    d.Specialization,
                    d.Phone,
                    d.Email,
                    d.DepartmentName
                });

            return Ok(doctors);
        }
    }
}