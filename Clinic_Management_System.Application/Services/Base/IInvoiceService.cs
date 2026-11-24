using Clinic_Management_System.Application.Dtos;

namespace Clinic_Management_System.Application.Services.Base
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllInvoices();

        Task<InvoiceUpdateDto?> GetInvoiceById(string uuid);

        Task<IEnumerable<PatientDto>> GetAllPatients();

        Task AddInvoice(InvoiceCreateDto invoiceDTO);

        Task UpdateInvoice(InvoiceUpdateDto invoiceDTO);

        Task DeleteInvoice(string uuid);
    }
}
