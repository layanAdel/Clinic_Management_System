using Clinic_Management_System.Application.Services.Base;
using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Data;
using Clinic_Management_System.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Management_System.Application.Services
{
    public class OrderService : IOrderService
    {

        private readonly AppDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(
            AppDbContext db,
            IUnitOfWork unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            IEnumerable<Order> orders = await _unitOfWork.OrderRepo.GetAll();
            return orders;
        }

        public async Task<List<Order>> GetMyOrders(int patientId)
        {
            var orders = await _unitOfWork.OrderRepo.GetOrdersByPatientIdAsync(patientId);
            return orders.ToList();
        }

        public async Task<int> MakeOrder(int patientId)
        {
            await using var transaction =
                await _db.Database.BeginTransactionAsync();


            var cartItems = await _db.Carts
                .Include(x => x.Appointment)
                .Where(x => x.PatientId == patientId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return 0;
            }

            if (cartItems.Any(x => x.Appointment == null ||
                                 x.Appointment.Status != "Available"||
                                 x.Appointment.AppointmentDate <= DateTime.Now))
            {
                return 0;
            }

            var order = new Order
            {
                PatientId = patientId,
                Status = "Confirmed",

                Items = cartItems.Select(x => new OrderItem
                {
                    AppointmentId = x.AppointmentId
                }).ToList()
            };

            foreach (var item in cartItems)
            {
                item.Appointment!.Status = "Booked";
                item.Appointment.PatientId = patientId;
            }



            await _unitOfWork.OrderRepo.AddOrderAsync(order);
            await _db.SaveChangesAsync();

            _db.Notifications.Add(new Notification
            {
                PatientId = patientId,
                OrderId = order.Id,
                Title = "Booking Confirmed",
                Message =
                  $"Your booking with ID {order.Id} has been confirmed successfully."
            });
            _db.Carts.RemoveRange(cartItems);

            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return order.Id;
        }

        public bool UpdateStatus(int orderId, string newStatus)
        {
            var order = _unitOfWork.OrderRepo.GetOrderByIdAsync(orderId).Result;
            if (order == null)
            {
                return false;
            }

            var allowedStatuses = new[]
       {
            "Pending", "Confirmed", "Completed", "Cancelled"
        };

            if (!allowedStatuses.Contains(newStatus))
                return false;

            if (order.Status == newStatus)
                return true;

            order.Status = newStatus;

            _unitOfWork.OrderRepo.UpdateOrderAsync(order);

            _db.Notifications.Add(new Notification
            {
                PatientId = order.PatientId,
                OrderId = order.Id,
                Title = "Booking Status Updated",
                Message = $"Your booking with ID {order.Id} status has been updated to '{newStatus}'."
            });

            _db.SaveChanges();
            return true;

        }
    }
}