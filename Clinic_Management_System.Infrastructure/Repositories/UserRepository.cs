using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Management_System.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }
        public User? GetByUsername(string username)
        {
            return _db.Users.FirstOrDefault(x => x.UserName == username);
        }
        public User? GetByEmail(string email)
        {
            return _db.Users.FirstOrDefault(x => x.Email == email);
        }
        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
    }
}
