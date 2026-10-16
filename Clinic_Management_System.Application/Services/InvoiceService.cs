using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Repositories.Base;
using Clinic_Management_System.Application.Services.Base;

namespace Clinic_Management_System.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
         
        public async Task<IEnumerable<InvoiceDto>> GetAllInvoices()
        {
            var invoices = await _unitOfWork.InvoiceRepository.GetAllInvoicesAsync();

            return invoices

                .Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    Uuid = i.Uuid,
                    PatientName = i.Patient != null ? i.Patient.FirstName : null,
                    InvoiceDate = i.InvoiceDate,
                    Amount = i.Amount,
                    PaymentMethod = i.PaymentMethod

                });
        }
        public async Task AddInvoice(InvoiceCreateDto invoiceDTO)
        {
            var invoice = new Invoice
            {
                PatientId = invoiceDTO.PatientId,
                InvoiceDate = invoiceDTO.InvoiceDate,
                Amount = invoiceDTO.Amount,
                PaymentMethod = invoiceDTO.PaymentMethod
            };

            await _unitOfWork.InvoiceRepository.AddInvoiceAsync(invoice);
        }

        public async Task DeleteInvoice(string uuid)
        {
            var inv = await _unitOfWork.InvoiceRepository.GetInvoiceByIdAsync(uuid);

            if (inv == null)
            {
                throw new KeyNotFoundException("Invoice not found");
            }

            await _unitOfWork.InvoiceRepository.DeleteInvoiceAsync(uuid);

        }

        public async Task<IEnumerable<PatientDto>> GetAllPatients()
        {
            var patients = await _unitOfWork.PatientRepository.GetAllPatientsAsync();

            return patients.Select(p => new PatientDto
            {
                Id = p.Id,
                FirstName = p.FirstName
            });
            
        }

        public async Task<InvoiceUpdateDto?> GetInvoiceById(string uuid)
        {
            var inv = await _unitOfWork.InvoiceRepository.GetInvoiceByIdAsync(uuid);

            if (inv == null)
            {
                return null;
            }
            return new InvoiceUpdateDto
            {
                Id = inv.Id,
                Uuid = inv.Uuid,
                PatientId = inv.PatientId,
                InvoiceDate = inv.InvoiceDate,
                Amount = inv.Amount,
                PaymentMethod = inv.PaymentMethod
            };
        }

        public async Task UpdateInvoice(InvoiceUpdateDto invoiceDTO)
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetInvoiceByIdAsync(invoiceDTO.Uuid);


            if (invoice == null)
            {
                throw new KeyNotFoundException("Invoice not found");
            }

            invoice.PatientId = invoiceDTO.PatientId;
            invoice.InvoiceDate = invoiceDTO.InvoiceDate;
            invoice.Amount = invoiceDTO.Amount;
            invoice.PaymentMethod = invoiceDTO.PaymentMethod;

            await _unitOfWork.InvoiceRepository.UpdateInvoiceAsync(invoice);
        }
    }
}
