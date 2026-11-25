namespace Clinic_Management_System.Infrastructure.Repositories.Base
{
    public interface IUnitOfWork
    {
        IPatientRepository PatientRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        IPrescriptionRepository PrescriptionRepository { get; }
        IUserRepository UserRepository { get; }

        public IOrderRepository OrderRepo { get; }

    }
}
