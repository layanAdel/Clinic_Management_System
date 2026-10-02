using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly AppDbContext _db;

        public PatientsController(AppDbContext db)
        {
            _db = db;
        }


        public ActionResult Index()
        {
            IEnumerable<Patient> pat = _db.Patients.ToList();
            return View(pat);
        }


        // Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _db.Patients.Add(patient);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(patient);
        }


        // Edit
        [HttpGet]
        public ActionResult Edit(string uuid)
        {
            var pat = _db.Patients.FirstOrDefault(x => x.Uuid == uuid);

            if (pat == null)
            {
                return NotFound();
            }

            return View(pat);
        }

        [HttpPost]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var oldpat = _db.Patients
      .FirstOrDefault(x => x.Uuid == patient.Uuid);

                if (oldpat == null)
                    return NotFound();

                oldpat.FirstName = patient.FirstName;
                oldpat.LastName = patient.LastName;
                oldpat.BirthDate = patient.BirthDate;
                oldpat.Gender = patient.Gender;
                oldpat.Phone = patient.Phone;
                oldpat.Email = patient.Email;

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(patient);
        }


        // Delete
        [HttpGet]
        public ActionResult Delete(string uuid)
        {
            var pat = _db.Patients.FirstOrDefault(x => x.Uuid == uuid);

            if (pat == null)
            {
                return NotFound();
            }

            return View(pat);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string uuid)
        {
            var pat = _db.Patients.FirstOrDefault(x => x.Uuid == uuid);

            if (pat == null)
            {
                return NotFound();
            }

            _db.Patients.Remove(pat);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}