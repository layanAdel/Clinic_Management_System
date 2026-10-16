using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public UsersController(
            IPatientService patientService,
            IDoctorService doctorService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
        }


        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatients();

            return Ok(new { message = "Get all patients", patients });
        }


        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctors();

            return Ok(new { message = "Get all doctors", doctors });
        }

    }
}
