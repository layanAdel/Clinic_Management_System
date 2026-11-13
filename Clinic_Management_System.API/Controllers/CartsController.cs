using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetCart(int patientId)
        {
            var cart = await _context.Carts
                .Select(ci => new
                {
                    ci.Id,
                    ci.AppointmentId,
                    ci.Appointment.DoctorId, DoctorName =
                    ci.Appointment.Doctor.FirstName,
                    ci.Appointment.AppointmentDate,
                    ci.Appointment.Status,
                    ci.PatientId
                })
                .Where(ci => ci.PatientId == patientId)
                .ToListAsync();

            return Ok(cart);
        }




        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest addToCart)
        {
            var appointment = await _context.Appointments.FindAsync(addToCart.AppointmentId);
            
            if (appointment == null)
            {
                return NotFound(new { message = "Appointment not found" });

            }

            if (appointment.Status != "Available")
            {
                return BadRequest(new { message = "Appointment is not available" });
            }


            var item = await _context.Carts
              .FirstOrDefaultAsync(x =>
                  x.PatientId == addToCart.PatientId &&
                  x.AppointmentId == addToCart.AppointmentId);


            if (item == null)
            {
                item = new Cart
                {
                    PatientId = addToCart.PatientId,
                    AppointmentId = addToCart.AppointmentId
                };

            _context.Carts.Add(item);

            }

            else
            {
                return BadRequest(new { message = "Appointment already added to cart" });
            }


            await _context.SaveChangesAsync();
        return Ok(new { message = "Item added to cart" });
        }



        [HttpDelete("{patientId}/{appointmentId}")]
        public async Task<IActionResult> RemoveFromCart(int patientId, int appointmentId)
        {
            var item = await _context.Carts
                .FirstOrDefaultAsync(x => 
                x.PatientId == patientId &&
                x.AppointmentId == appointmentId);

            if (item == null)
            {
                return NotFound(new { message = "Item not found in cart" });
            }

            _context.Carts.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Item removed from cart" });
        }



        public class AddToCartRequest
        {
            public int AppointmentId { get; set; }
            public int PatientId { get; set; }
     
        }

    }
}