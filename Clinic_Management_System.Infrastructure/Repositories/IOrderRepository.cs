using Clinic_Management_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Management_System.Infrastructure.Repositories
{
    public interface IOrderRepository
    {

        Task<IEnumerable<Order>> GetAll();

        Task<IEnumerable<Order>> GetOrdersByPatientIdAsync(int patientId);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
    }
}
