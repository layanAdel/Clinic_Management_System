using Clinic_Management_System.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Application.Dtos
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public string PatientName { get; set; } = string.Empty;


        public string DoctorName { get; set; } = string.Empty;

        public string? PrescriptionImage { get; set; }
        public string Medication { get; set; } = string.Empty;

        public string Dosage { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;

        public DateTime PrescriptionDate { get; set; }
    }

    public class PrescriptionCreateDto
    {

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public string? PrescriptionImage { get; set; }
        public string Medication { get; set; } = string.Empty;

        public string Dosage { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;

        public DateTime PrescriptionDate { get; set; }
    }

    public class PrescriptionUpdateDto : PrescriptionCreateDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
    }
}

