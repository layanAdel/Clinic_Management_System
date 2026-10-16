using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {

        private readonly IInvoiceService _invoiceService;
        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;

        }


        public async Task<IActionResult> Index()
        {
            var invoices = await _invoiceService.GetAllInvoices();

            return View(invoices);
        }


        // Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadPatientsAsync();

            return View(new InvoiceCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceCreateDto invoiceDTO)
        {
            if (ModelState.IsValid)
            {

              await _invoiceService.AddInvoice(invoiceDTO);

                return RedirectToAction(nameof(Index));
            }

            await LoadPatientsAsync();

            return View(invoiceDTO);
        }


        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var inv = await _invoiceService.GetInvoiceById(uuid);

            if (inv == null)
            {
                return NotFound();
            }


            await LoadPatientsAsync();

            return View(inv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InvoiceUpdateDto invoiceDTO)
        {
            if (ModelState.IsValid)
            {
                var existingInvoice =
                    await _invoiceService.GetInvoiceById(invoiceDTO.Uuid);

                if (existingInvoice == null)
                {
                    return NotFound();
                }

                await _invoiceService.UpdateInvoice(invoiceDTO);

                return RedirectToAction(nameof(Index));
            }

            await LoadPatientsAsync();

            return View(invoiceDTO);
        }


        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var inv = await _invoiceService.GetInvoiceById(uuid);

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
            if (uuid == null)
            {
                return NotFound();
            }

            var inv = await _invoiceService.GetInvoiceById(uuid);

            if (inv == null)
            {
                return NotFound();
            }

            await _invoiceService.DeleteInvoice(uuid);

            return RedirectToAction(nameof(Index));
        }
        private async Task LoadPatientsAsync()
        {
            var patients = await _invoiceService.GetAllPatients();

            ViewBag.patients = new SelectList(
                patients,
                "Id",
                "FirstName"
            );
        }

    }
}