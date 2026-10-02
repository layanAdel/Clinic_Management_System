using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _db;

        public AppointmentsController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<Appointment> appo = _db.Appointments.ToList();
            return View(appo);
        }

        // Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _db.Appointments.Add(appointment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(appointment);
        }


        // Edit
        [HttpGet]
        public ActionResult Edit(string uuid)
        {
            var appo = _db.Appointments.FirstOrDefault(x => x.Uuid == uuid);

            if (appo == null)
            {
                return NotFound();
            }

            return View(appo);
        }

        [HttpPost]
        public ActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var oldappo = _db.Appointments
                    .FirstOrDefault(x => x.Uuid == appointment.Uuid);

                if (oldappo == null)
                    return NotFound();

                oldappo.FirstName = appointment.FirstName;
                oldappo.LastName = appointment.LastName;
                oldappo.DoctorFirstName = appointment.DoctorFirstName;
                oldappo.DoctorLastName = appointment.DoctorLastName;
                oldappo.AppointmentDate = appointment.AppointmentDate;
                oldappo.Status = appointment.Status;
                oldappo.Reason = appointment.Reason;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(appointment);
        }


        // Delete
        [HttpGet]
        public ActionResult Delete(string uuid)
        {
            var appo = _db.Appointments.FirstOrDefault(x => x.Uuid == uuid);

            if (appo == null)
            {
                return NotFound();
            }

            return View(appo);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string uuid)
        {
            var appo = _db.Appointments.FirstOrDefault(x => x.Uuid == uuid);

            if (appo == null)
            {
                return NotFound();
            }

            _db.Appointments.Remove(appo);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}