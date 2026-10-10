namespace Clinic_Management_System.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int DoctorId { get; set; }
        public int EmployeeId { get; set; }
    }

    public class DepartmentCreateDto
    {
       
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

       
    }

    public class DepartmentUpdateDto : DepartmentCreateDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
    }


}
