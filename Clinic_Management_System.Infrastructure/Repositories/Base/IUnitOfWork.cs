namespace Clinic_Management_System.Infrastructure.Repositories.Base
{
    public interface IUnitOfWork
    {
        IPatientRepository PatientRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        IMedicalRecordRepository MedicalRecordRepository { get; }
        IPrescriptionRepository PrescriptionRepository { get; }

    }
}
