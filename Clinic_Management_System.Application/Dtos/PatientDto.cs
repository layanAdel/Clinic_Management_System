using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Application.Dtos
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int AppointmentId { get; set; }
        public int MedicalRecordId { get; set; }
        public int PrescriptionId { get; set; }
        public int InvoiceId { get; set; }
    }

    public class PatientCreateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class PatientUpdateDto : PatientCreateDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
    }

}
