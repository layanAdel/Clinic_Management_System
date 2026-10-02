using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class MedicalRecordsController : Controller
    {
        private readonly AppDbContext _db;

        public MedicalRecordsController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<MedicalRecord> med = _db.MedicalRecords.ToList();
            return View(med);
        }


        // Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _db.MedicalRecords.Add(medicalRecord);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(medicalRecord);
        }


        // Edit
        [HttpGet]
        public ActionResult Edit(string uuid)
        {
            var med = _db.MedicalRecords.FirstOrDefault(x => x.Uuid == uuid);

            if (med == null)
            {
                return NotFound();
            }

            return View(med);
        }

        [HttpPost]
        public ActionResult Edit(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                var oldmed = _db.MedicalRecords
                    .FirstOrDefault(x => x.Uuid == medicalRecord.Uuid);

                if (oldmed == null)
                    return NotFound();

                oldmed.FirstName = medicalRecord.FirstName;
                oldmed.LastName = medicalRecord.LastName;
                oldmed.DoctorFirstName = medicalRecord.DoctorFirstName;
                oldmed.DoctorLastName = medicalRecord.DoctorLastName;
                oldmed.VisitDate = medicalRecord.VisitDate;
                oldmed.Diagnosis = medicalRecord.Diagnosis;
                oldmed.Notes = medicalRecord.Notes;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(medicalRecord);
        }


        // Delete
        [HttpGet]
        public ActionResult Delete(string uuid)
        {
            var med = _db.MedicalRecords.FirstOrDefault(x => x.Uuid == uuid);

            if (med == null)
            {
                return NotFound();
            }

            return View(med);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string uuid)
        {
            var med = _db.MedicalRecords.FirstOrDefault(x => x.Uuid == uuid);

            if (med == null)
            {
                return NotFound();
            }

            _db.MedicalRecords.Remove(med);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}