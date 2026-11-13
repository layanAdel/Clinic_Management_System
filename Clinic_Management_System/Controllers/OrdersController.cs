using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Dashboard - All Orders
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrders();
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(
            int orderId,
            UpdateOrderStatusDto dto)
        {
            bool updated = _orderService.UpdateStatus(
                orderId,
                dto.Status
            );

            if (!updated)
            {
                return BadRequest("Invalid order or status");
            }


            return RedirectToAction("Index");

        }
    }

    public class UpdateOrderStatusDto
    {
        public string Status { get; set; } = "";
    }
}
