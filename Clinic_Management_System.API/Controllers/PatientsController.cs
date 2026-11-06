using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = (await _patientService.GetAllPatients())
                .Select(p => new
                {
                    p.Id,
                    p.Uuid,
                    p.FirstName,
                    p.LastName,
                    p.BirthDate,
                    p.Gender,
                    p.Phone,
                    p.Email
                });

            return Ok(patients);
        }
    }
}