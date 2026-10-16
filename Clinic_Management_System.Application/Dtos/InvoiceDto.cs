using Clinic_Management_System.Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Application.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public string PatientName { get; set; } = string.Empty;
      
        public DateTime InvoiceDate { get; set; }

        
        public decimal Amount { get; set; }

        
        public string PaymentMethod { get; set; } = string.Empty;
    }

    public class InvoiceCreateDto
    {

        public int PatientId { get; set; }

        public DateTime InvoiceDate { get; set; }


        public decimal Amount { get; set; }


        public string PaymentMethod { get; set; } = string.Empty;
    }

    public class InvoiceUpdateDto : InvoiceCreateDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
    }
}
