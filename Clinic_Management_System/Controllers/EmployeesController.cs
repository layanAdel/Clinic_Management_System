using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _db;

        public EmployeesController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<Employee> emp = _db.Employees.ToList();
            return View(emp);
        }


        // Create

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(employee);
        }


        // Edit

        [HttpGet]
        public ActionResult Edit(string uuid)
        {
            var emp = _db.Employees.FirstOrDefault(x => x.Uuid == uuid);

            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var oldemp = _db.Employees
                    .FirstOrDefault(x => x.Uuid == employee.Uuid);

                if (oldemp == null)
                    return NotFound();

                oldemp.FirstName = employee.FirstName;
                oldemp.LastName = employee.LastName;
                oldemp.Position = employee.Position;
                oldemp.Phone = employee.Phone;
                oldemp.Email = employee.Email;
                oldemp.HireDate = employee.HireDate;
                oldemp.Salary = employee.Salary;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(employee);
        }


        // Delete

        [HttpGet]
        public ActionResult Delete(string uuid)
        {
            var emp = _db.Employees.FirstOrDefault(x => x.Uuid == uuid);

            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string uuid)
        {
            var emp = _db.Employees.FirstOrDefault(x => x.Uuid == uuid);

            if (emp == null)
            {
                return NotFound();
            }

            _db.Employees.Remove(emp);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}