using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Models
{
    [Index(nameof(Uuid), IsUnique = true)]

    public class Invoice
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public int PatientId { get; set; }

        public Patient? Patient { get; set; }

        [DisplayName("Invoice Date")]
        [Required(ErrorMessage = "Invoice Date is required")]
        public DateTime InvoiceDate { get; set; }

        [DisplayName("Invoice Amount")]
        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }

        [DisplayName("Payment Method")]
        [Required(ErrorMessage = "Payment Method is required")]
        public string PaymentMethod { get; set; }

    }
}
