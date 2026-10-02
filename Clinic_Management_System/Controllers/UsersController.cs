using Clinic_Management_System.Data;
using Clinic_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var users = _db.Users.ToList();

            return View(users);
        }


        // Details
        public IActionResult Details(string uuid)
        {
            var user = _db.Users.FirstOrDefault(x => x.Uuid == uuid);

            if (user == null)
                return NotFound();

            return View(user);
        }


        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _db.Users.Add(user);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }


        // Edit
        public IActionResult Edit(string uuid)
        {
            var user = _db.Users.FirstOrDefault(x => x.Uuid == uuid);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var oldUser = _db.Users
                    .FirstOrDefault(x => x.Uuid == user.Uuid);

                if (oldUser == null)
                    return NotFound();

                oldUser.Name = user.Name;
                oldUser.UserName = user.UserName;
                oldUser.Email = user.Email;
                oldUser.IsLocked = user.IsLocked;

                if (!string.IsNullOrEmpty(user.Password))
                {
                    oldUser.Password =
                        BCrypt.Net.BCrypt.HashPassword(user.Password);
                }

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }


        // Delete
        public IActionResult Delete(string uuid)
        {
            var user = _db.Users.FirstOrDefault(x => x.Uuid == uuid);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var user = _db.Users.FirstOrDefault(x => x.Uuid == uuid);

            if (user == null)
                return NotFound();

            _db.Users.Remove(user);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}