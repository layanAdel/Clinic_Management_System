using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ActionResult Index()
        {
            IEnumerable<Invoice> inv = _db.Invoices.ToList();
            return View(inv);
        }


        // Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _db.Invoices.Add(invoice);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields ");
            return View(invoice);
        }


        // Edit
        [HttpGet]
        public ActionResult Edit(string uuid)
        {
            var inv = _db.Invoices.FirstOrDefault(x => x.Uuid == uuid);

            if (inv == null)
            {
                return NotFound();
            }

            return View(inv);
        }

        [HttpPost]
        public ActionResult Edit(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                var oldinv = _db.Invoices
                    .FirstOrDefault(x => x.Uuid == invoice.Uuid);

                if (oldinv == null)
                    return NotFound();

                oldinv.FirstName = invoice.FirstName;
                oldinv.LastName = invoice.LastName;
                oldinv.InvoiceDate = invoice.InvoiceDate;
                oldinv.Amount = invoice.Amount;
                oldinv.PaymentMethod = invoice.PaymentMethod;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(invoice);
        }


        // Delete
        [HttpGet]
        public ActionResult Delete(string uuid)
        {
            var inv = _db.Invoices.FirstOrDefault(x => x.Uuid == uuid);

            if (inv == null)
            {
                return NotFound();
            }

            return View(inv);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string uuid)
        {
            var inv = _db.Invoices.FirstOrDefault(x => x.Uuid == uuid);

            if (inv == null)
            {
                return NotFound();
            }

            _db.Invoices.Remove(inv);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}