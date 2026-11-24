using Clinic_Management_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Management_System.Application.Services.Base
{
    public interface IOrderService
    {
        Task<int> MakeOrder(int patientId);

        Task<List<Order>> GetMyOrders(int patientId);

        Task<IEnumerable<Order>> GetAllOrders();

        bool UpdateStatus(int orderId, string newStatus);
    }
}