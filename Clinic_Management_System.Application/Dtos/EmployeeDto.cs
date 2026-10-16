using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Application.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public string FirstName { get; set; } = string.Empty;

       
        public string LastName { get; set; } = string.Empty;


        public string Position { get; set; } = string.Empty;


        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;


        public DateTime HireDate { get; set; }

        public decimal Salary { get; set; } = 0;

        public string DepartmentName { get; set; } = string.Empty;
    }

    public class EmployeeCreateDto
    {

        public string FirstName { get; set; } = string.Empty;


        public string LastName { get; set; } = string.Empty;


        public string Position { get; set; } = string.Empty;


        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;


        public DateTime HireDate { get; set; }

        public decimal Salary { get; set; } = 0;

        public int DepartmentId { get; set; } 
    }

    public class EmployeeUpdateDto : EmployeeCreateDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
    }

}
