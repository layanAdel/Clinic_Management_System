using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Management_System.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task AddOrderAsync(Order order)
        {
            _db.Orders.Add(order);
            return Task.CompletedTask;
        }

        public Task DeleteOrderAsync(int orderId)
        {
            var order = _db.Orders.Find(orderId);
            if (order != null)
            {
                _db.Orders.Remove(order);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _db.Orders
            .Include(x => x.Items)
            .ThenInclude(x => x.Appointment)
            .OrderByDescending(x => x.Id)
            .ToListAsync();

            return orders;
        }

        public Task<Order?> GetOrderByIdAsync(int orderId)
        {
            var order = _db.Orders
            .Include(x => x.Items)
            .FirstOrDefault(x => x.Id == orderId);

            return Task.FromResult<Order?>(order);
        }

        public Task<IEnumerable<Order>> GetOrdersByPatientIdAsync(int patientId)
        {
            var orders = _db.Orders
            .Include(x => x.Items)
            .ThenInclude(x => x.Appointment)
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.Id)
            .ToList();

            return Task.FromResult<IEnumerable<Order>>(orders);

        }

        public Task UpdateOrderAsync(Order order)
        {
            _db.Orders.Update(order);
            return Task.CompletedTask;
        }
    }
}