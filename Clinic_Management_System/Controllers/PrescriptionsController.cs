using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class PrescriptionsController : Controller
    {
        private readonly AppDbContext _db;

        public PrescriptionsController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<Prescription> pre = _db.Prescriptions.ToList();
            return View(pre);
        }


        // Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _db.Prescriptions.Add(prescription);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(prescription);
        }


        // Edit
        [HttpGet]
        public ActionResult Edit(string uuid)
        {
            var pre = _db.Prescriptions.FirstOrDefault(x => x.Uuid == uuid);

            if (pre == null)
            {
                return NotFound();
            }

            return View(pre);
        }

        [HttpPost]
        public ActionResult Edit(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                var oldpre = _db.Prescriptions
                    .FirstOrDefault(x => x.Uuid == prescription.Uuid);

                if (oldpre == null)
                    return NotFound();

                oldpre.FirstName = prescription.FirstName;
                oldpre.LastName = prescription.LastName;
                oldpre.DoctorFirstName = prescription.DoctorFirstName;
                oldpre.DoctorLastName = prescription.DoctorLastName;
                oldpre.Medication = prescription.Medication;
                oldpre.Dosage = prescription.Dosage;
                oldpre.Instructions = prescription.Instructions;
                oldpre.PrescriptionDate = prescription.PrescriptionDate;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(prescription);
        }


        // Delete
        [HttpGet]
        public ActionResult Delete(string uuid)
        {
            var pre = _db.Prescriptions.FirstOrDefault(x => x.Uuid == uuid);

            if (pre == null)
            {
                return NotFound();
            }

            return View(pre);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string uuid)
        {
            var pre = _db.Prescriptions.FirstOrDefault(x => x.Uuid == uuid);

            if (pre == null)
            {
                return NotFound();
            }

            _db.Prescriptions.Remove(pre);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}