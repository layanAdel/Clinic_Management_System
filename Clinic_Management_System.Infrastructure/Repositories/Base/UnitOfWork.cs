using Clinic_Management_System.Infrastructure.Data;
using Clinic_Management_System.Infrastructure.Repositories;

namespace Clinic_Management_System.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            PatientRepository = new PatientRepository(_db);

            EmployeeRepository = new EmployeeRepository(_db);

            DoctorRepository = new DoctorRepository(_db);

            DepartmentRepository = new DepartmentRepository(_db);

            InvoiceRepository = new InvoiceRepository(_db);

            AppointmentRepository = new AppointmentRepository(_db);

            MedicalRecordRepository = new MedicalRecordRepository(_db);

            PrescriptionRepository = new PrescriptionRepository(_db);
            UserRepository = new UserRepository(_db);
        }

        public IPatientRepository PatientRepository {  get; }

        public IEmployeeRepository EmployeeRepository { get; }

        public IDoctorRepository DoctorRepository { get; }

        public IDepartmentRepository DepartmentRepository { get; }

        public IInvoiceRepository InvoiceRepository { get; }

        public IAppointmentRepository AppointmentRepository { get; }

        public IMedicalRecordRepository MedicalRecordRepository { get; }

        public IPrescriptionRepository PrescriptionRepository { get; }
        public IUserRepository UserRepository { get; }
    }
}
