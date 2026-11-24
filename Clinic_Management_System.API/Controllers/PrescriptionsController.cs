using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrescriptions()
        {
            var prescriptions = (await _prescriptionService.GetAllPrescriptions())
                .Select(p => new
                {
                    p.Id,
                    p.Uuid,
                    p.PatientName,
                    p.DoctorName,
                    p.Medication,
                    p.Dosage,
                    p.Instructions,
                    p.PrescriptionImage,
                    p.PrescriptionDate
                });

            return Ok(prescriptions);
        }
    }
}