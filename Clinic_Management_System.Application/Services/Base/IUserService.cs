using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Management_System.Application.Services.Base
{
    public interface IUserService
    {
        User? Login(LoginDto loginDto);

        User? Register(RegisterDto registerDto);
    }
}
