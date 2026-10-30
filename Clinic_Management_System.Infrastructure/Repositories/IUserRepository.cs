using Clinic_Management_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Management_System.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);


        User? GetByEmail(string email);

        void Add(User user);

    }
}
