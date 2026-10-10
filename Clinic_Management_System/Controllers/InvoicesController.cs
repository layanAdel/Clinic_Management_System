using Clinic_Management_System.Data;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly AppDbContext _db;

        public InvoicesController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<InvoiceDto> inv = await _db.Invoices.Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    Uuid = i.Uuid,
                    PatientName = i.Patient != null ? i.Patient.FirstName : null,
                    InvoiceDate = i.InvoiceDate,
                    Amount = i.Amount,
                    PaymentMethod = i.PaymentMethod

                }).ToListAsync();

            return View(inv);
        }


        // Create
        [HttpGet]
        public IActionResult Create()
        {
            var patients = _db.Patients.ToList();

            SelectList selectList = new SelectList(patients, "Id", "FirstName");
            ViewBag.Patients = selectList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceCreateDto invoiceDto)
        {
            if (ModelState.IsValid)
            {

                var invoice = new Invoice
                {
                    PatientId = invoiceDto.PatientId,
                    InvoiceDate = invoiceDto.InvoiceDate,
                    Amount = invoiceDto.Amount,
                    PaymentMethod = invoiceDto.PaymentMethod
                };

                _db.Invoices.Add(invoice);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            ViewBag.Patients = new SelectList(
                await _db.Patients.ToListAsync(), "Id", "FirstName");

            return View(invoiceDto);
        }


        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var inv = await _db.Invoices.FirstOrDefaultAsync(x => x.Uuid == uuid);

            if (inv == null)
            {
                return NotFound();
            }

            var invoiceDto = new InvoiceUpdateDto
            {
                Id = inv.Id,
                Uuid = inv.Uuid,
                PatientId = inv.PatientId,
                InvoiceDate = inv.InvoiceDate,
                Amount = inv.Amount,
                PaymentMethod = inv.PaymentMethod
            };

            var Patients = await _db.Patients.ToListAsync();
            ViewBag.Patients = new SelectList(Patients, "Id", "FirstName");

            return View(invoiceDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InvoiceUpdateDto invoiceDto)
        {
            if (ModelState.IsValid)
            {
                var invoice = await _db.Invoices
                    .FirstOrDefaultAsync(x => x.Uuid == invoiceDto.Uuid);

                if (invoice == null)
                {
                    return NotFound();
                }

                invoice.PatientId = invoiceDto.PatientId;
                invoice.InvoiceDate = invoiceDto.InvoiceDate;
                invoice.Amount = invoiceDto.Amount;
                invoice.PaymentMethod = invoiceDto.PaymentMethod;

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new SelectList(
                await _db.Patients.ToListAsync(), "Id", "FirstName");

            return View(invoiceDto);
        }


        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var inv = await _db.Invoices.FirstOrDefaultAsync(x => x.Uuid == uuid);

            if (inv == null)
            {
                return NotFound();
            }

            return View(inv);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirm(string uuid)
        {
            var inv = await _db.Invoices.FirstOrDefaultAsync(x => x.Uuid == uuid);

            if (inv != null)
            {
                _db.Invoices.Remove(inv);
                await _db.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }


       

    }
}