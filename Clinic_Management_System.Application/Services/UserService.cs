using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Management_System.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        public User? Login(LoginDto loginDto)
        {
            var user = _unitOfWork.UserRepository.GetByUsername(loginDto.Username);
            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!isPasswordValid)
            {
                return null;
            }

            return user;
        }


        public User? Register(RegisterDto registerDto)
        {
            var existingUserByUsername = _unitOfWork.UserRepository.GetByUsername(registerDto.Username);
            if (existingUserByUsername != null)
            {
                return null;
            }
            var existingUserByEmail = _unitOfWork.UserRepository.GetByEmail(registerDto.Email);
            if (existingUserByEmail != null)
            {
                return null;
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var newUser = new User
            {
                Name = registerDto.Name,
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Password = hashedPassword
            };
            _unitOfWork.UserRepository.Add(newUser);

            return newUser;
        }

    }
}