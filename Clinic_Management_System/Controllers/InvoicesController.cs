using Clinic_Management_System.Data;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Clinic_Management_System.Repositories;
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

        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPatientRepository _patientRepository;

        public InvoicesController(IInvoiceRepository invoiceRepository,
            IPatientRepository patientRepository)
        {
            _invoiceRepository = invoiceRepository;
            _patientRepository = patientRepository;
        }


        public async Task<IActionResult> Index()
        {
            var invoices = await _invoiceRepository.GetAllInvoicesAsync();

            IEnumerable <InvoiceDto> invoice = invoices

                .Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    Uuid = i.Uuid,
                    PatientName = i.Patient != null ? i.Patient.FirstName : null,
                    InvoiceDate = i.InvoiceDate,
                    Amount = i.Amount,
                    PaymentMethod = i.PaymentMethod

                }).ToList();

            return View(invoice);
        }


        // Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var patients = await _patientRepository.GetAllPatientsAsync();

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

                await _invoiceRepository.AddInvoiceAsync(invoice);

                return RedirectToAction(nameof(Index));
            }


            ViewBag.Patients = new SelectList(
              await _patientRepository.GetAllPatientsAsync(), "Id", "FirstName");

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

            var inv = await _invoiceRepository.GetInvoiceByIdAsync(uuid);

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

            var Patients = await _patientRepository.GetAllPatientsAsync();
            ViewBag.Patients = new SelectList(Patients, "Id", "FirstName");

            return View(invoiceDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InvoiceUpdateDto invoiceDto)
        {
            if (ModelState.IsValid)
            {
                var invoice = await _invoiceRepository.GetInvoiceByIdAsync(invoiceDto.Uuid);


                if (invoice == null)
                {
                    return NotFound();
                }

                invoice.PatientId = invoiceDto.PatientId;
                invoice.InvoiceDate = invoiceDto.InvoiceDate;
                invoice.Amount = invoiceDto.Amount;
                invoice.PaymentMethod = invoiceDto.PaymentMethod;

                await _invoiceRepository.UpdateInvoiceAsync(invoice);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new SelectList(
               await _patientRepository.GetAllPatientsAsync(), "Id", "FirstName");

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

            var inv = await _invoiceRepository.GetInvoiceByIdAsync(uuid);

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
            var inv = await _invoiceRepository.GetInvoiceByIdAsync(uuid);

            if (inv != null)
            {
              await _invoiceRepository.DeleteInvoiceAsync(uuid);


            }

            return RedirectToAction(nameof(Index));
        }


       

    }
}