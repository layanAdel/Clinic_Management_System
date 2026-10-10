using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic_Management_System.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    public class Prescription
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public int PatientId { get; set; }

        public Patient? Patient { get; set; }


        public int DoctorId { get; set; }

        public Doctor? Doctor { get; set; }

        [Required(ErrorMessage = "Medication is required")]
        public string Medication { get; set; }

        [Required(ErrorMessage = "Dosage is required")]
        public string Dosage { get; set; }

        public string Instructions { get; set; }

        [Required(ErrorMessage = "PrescriptionDate is required")]
        public DateTime PrescriptionDate { get; set; }
    }
}
