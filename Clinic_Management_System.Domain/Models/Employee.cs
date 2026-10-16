using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Domain.Models
{
    [Index(nameof(Uuid), IsUnique = true)]

    public class Employee
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();


        [DisplayName("First Name")]
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Hire Date is required")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; } = 0;

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
