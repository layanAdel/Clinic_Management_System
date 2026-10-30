using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services;
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
        private readonly IUserService _userService;

        public UsersController(
            IPatientService patientService,
            IDoctorService doctorService,
            IUserService userService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
            _userService = userService;
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userService.Login(loginDto);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            return Ok(new
            {

                message = "Login successful",
                id = user.Id,
                name = user.Name,
                username = user.UserName,
                email = user.Email,
            });
        }



        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            var user = _userService.Register(registerDto);
            if (user == null)
            {
                return BadRequest(new { message = "Username or email already exists" });
            }
            return Ok(new { message = "Registration successful", user });
        }

        //[HttpGet("GetAllPatients")]
        //public async Task<IActionResult> GetAllPatients()
        //{
        //    var patients = await _patientService.GetAllPatients();

        //    return Ok(new { message = "Get all patients", patients });
        //}


        //[HttpGet("GetAllDoctors")]
        //public async Task<IActionResult> GetAllDoctors()
        //{
        //    var doctors = await _doctorService.GetAllDoctors();

        //    return Ok(new { message = "Get all doctors", doctors });
        //}

    }
}
