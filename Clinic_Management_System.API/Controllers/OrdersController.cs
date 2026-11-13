using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Make Order
        [HttpPost("{patientId}")]
        public async Task<IActionResult> MakeOrder(int patientId)
        {
            int orderId = await _orderService.MakeOrder(patientId);

            if (orderId == 0)
                return BadRequest("Cart is empty or appointment is not available");

            return Ok(new
            {
                message = "Booking created successfully",
                orderId = orderId
            });
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetMyOrders(int patientId)
        {
            var orders = await _orderService.GetMyOrders(patientId);

            var result = orders.Select(order => new
            {
                order.Id,
                order.PatientId,
                order.CreatedAt,
                order.Status,

                Items = order.Items.Select(item => new
                {
                    item.Id,
                    item.AppointmentId,
                    item.Appointment?.DoctorId,
                    item.Appointment?.AppointmentDate,
                    item.Appointment?.Status

                }).ToList()

            }).ToList();

            return Ok(result);
        }

    }
}