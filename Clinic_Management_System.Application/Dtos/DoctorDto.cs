using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Application.Dtos
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? DepartmentName { get; set; }
        public int AppointmentId { get; set; }
        public int MedicalRecordId { get; set; }
        public int PrescriptionId { get; set; }

    }
    public class DoctorCreateDto
    {
        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;       
        public string Specialization { get; set; } = string.Empty;      
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
    }

    public class DoctorUpdateDto : DoctorCreateDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
    }




}
