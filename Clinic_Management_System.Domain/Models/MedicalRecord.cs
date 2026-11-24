using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic_Management_System.Domain.Models
{
    [Index(nameof(Uuid), IsUnique = true)]

    public class MedicalRecord
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();


        public int PatientId { get; set; }

        public Patient? Patient { get; set; }

        public int DoctorId { get; set; }

        public Doctor? Doctor { get; set; }

        [DisplayName("Visit Date")]
        [Required(ErrorMessage = "Visit Date is required")]
        public DateTime VisitDate { get; set; }

        [Required(ErrorMessage = "Diagnosis is required")]
        public string Diagnosis { get; set; }

        [DisplayName("Doctor Notes")]
        public string Notes { get; set; }
    }
}
