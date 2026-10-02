using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Models
{
    [Index(nameof(Uuid), IsUnique = true)]

    public class Appointment
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();


        [DisplayName("First Name")]
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Name is required")]
        public string LastName { get; set; }

        [DisplayName("Doctor First Name")]
        [Required(ErrorMessage = "Name is required")]
        public string DoctorFirstName { get; set; }

        [DisplayName("Doctor Last Name")]
        [Required(ErrorMessage = "Name is required")]
        public string DoctorLastName { get; set; }

        [DisplayName("Appointment Date")]
        [Required(ErrorMessage = "Appointment Date is required")]
        public DateTime AppointmentDate { get; set; }

        [DisplayName("Appointment Status")]
        public string Status { get; set; }

        [DisplayName("Appointment Reason")]
        public string Reason { get; set; }
    }
}
