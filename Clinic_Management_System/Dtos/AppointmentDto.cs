using Clinic_Management_System.Models;
using System.ComponentModel;

namespace Clinic_Management_System.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;

        public string DoctorName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
    }


    public class AppointmentCreateDto
    {
        public int PatientId { get; set; } 

        public int DoctorId { get; set; }

        public string Status { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
    }

    public class AppointmentUpdateDto : AppointmentCreateDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
    }

}
