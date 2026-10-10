using System.ComponentModel;

namespace Clinic_Management_System.Dtos
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;

        public string DoctorName { get; set; } = string.Empty;
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

    }

    public class MedicalRecordCreateDto
    {
       
        public int PatientId { get; set; } 

        public int DoctorId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

    }

    public class MedicalRecordUpdateDto: MedicalRecordCreateDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
    }

}
