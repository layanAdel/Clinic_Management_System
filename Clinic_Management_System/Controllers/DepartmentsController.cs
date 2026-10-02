using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _db;

        public DepartmentsController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<Department> dep = _db.Departments.ToList();
            return View(dep);
        }


        // Create

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _db.Departments.Add(department);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(department);
        }


        // Edit

        [HttpGet]
        public ActionResult Edit(string uuid)
        {
            var dep = _db.Departments.FirstOrDefault(x => x.Uuid == uuid);

            if (dep == null)
            {
                return NotFound();
            }

            return View(dep);
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                var olddep = _db.Departments
                    .FirstOrDefault(x => x.Uuid == department.Uuid);

                if (olddep == null)
                    return NotFound();

                olddep.Name = department.Name;
                olddep.Description = department.Description;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(department);
        }


        // Delete

        [HttpGet]
        public ActionResult Delete(string uuid)
        {
            var dep = _db.Departments.FirstOrDefault(x => x.Uuid == uuid);

            if (dep == null)
            {
                return NotFound();
            }

            return View(dep);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string uuid)
        {
            var dep = _db.Departments.FirstOrDefault(x => x.Uuid == uuid);

            if (dep == null)
            {
                return NotFound();
            }

            _db.Departments.Remove(dep);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}