using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly AppDbContext _db;

        public DoctorsController(AppDbContext db)
        {
            _db = db;
        }


        public ActionResult Index()
        {
            IEnumerable<Doctor> doct = _db.Doctors.ToList();
            return View(doct);
        }


        // Create

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _db.Doctors.Add(doctor);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(doctor);
        }


        // Edit

        [HttpGet]
        public ActionResult Edit(string uuid)
        {
            var doct = _db.Doctors.FirstOrDefault(x => x.Uuid == uuid);

            if (doct == null)
            {
                return NotFound();
            }

            return View(doct);
        }

        [HttpPost]
        public ActionResult Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var olddoct = _db.Doctors
                    .FirstOrDefault(x => x.Uuid == doctor.Uuid);

                if (olddoct == null)
                    return NotFound();

                olddoct.FirstName = doctor.FirstName;
                olddoct.LastName = doctor.LastName;
                olddoct.Department = doctor.Department;
                olddoct.Specialization = doctor.Specialization;
                olddoct.Phone = doctor.Phone;
                olddoct.Email = doctor.Email;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(doctor);
        }


        // Delete

        [HttpGet]
        public ActionResult Delete(string uuid)
        {
            var doct = _db.Doctors.FirstOrDefault(x => x.Uuid == uuid);

            if (doct == null)
            {
                return NotFound();
            }

            return View(doct);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string uuid)
        {
            var doct = _db.Doctors.FirstOrDefault(x => x.Uuid == uuid);

            if (doct == null)
            {
                return NotFound();
            }

            _db.Doctors.Remove(doct);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}