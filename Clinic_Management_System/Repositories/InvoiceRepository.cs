using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _db;

        public InvoiceRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task AddInvoiceAsync(Invoice invoice)
        {
            _db.Invoices.Add(invoice);
            return _db.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(string uuid)
        {
            var invoice = await _db.Invoices.FirstOrDefaultAsync(d => d.Uuid == uuid);

            if (invoice != null)
            {
                _db.Invoices.Remove(invoice);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            IEnumerable<Invoice> invoices = await _db.Invoices
                     .Include(a => a.Patient)
                     .ToListAsync();

            return invoices;
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(string uuid)
        {
            var invoice = await _db.Invoices
                .FirstOrDefaultAsync(d => d.Uuid == uuid);

            return invoice;
        }

        public Task UpdateInvoiceAsync(Invoice invoice)
        {
            _db.Invoices.Update(invoice);
            return _db.SaveChangesAsync();
        }
    }
}
