using Clinic_Management_System.Application.Dtos;
using Clinic_Management_System.Application.Services.Base;
using Clinic_Management_System.Domain.Models;
using Clinic_Management_System.Infrastructure.Data;
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
        private readonly AppDbContext _db;

        public UserService(IUnitOfWork unitOfWork, AppDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }


        public User? Login(LoginDto loginDto)
        {
            var user = _unitOfWork.UserRepository.GetByUsername(loginDto.Username);

            if (user == null)
            {
                return null;
            }

            if (user.IsLocked)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!isPasswordValid)
            {
                return null;
            }

            user.Patient = _db.Patients.FirstOrDefault(p => p.UserId == user.Id);


            return user;
        }


        public async Task<User?> Register(RegisterDto registerDto)
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

            await using var transaction =
              await _db.Database.BeginTransactionAsync();


            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var newUser = new User
            {
                Name = registerDto.FirstName + " " + registerDto.LastName,
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Password = hashedPassword
            };

            _unitOfWork.UserRepository.Add(newUser);

            var newPatient = new Patient
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                Phone = registerDto.Phone,
                Email = registerDto.Email,
                User = newUser
            };

            await _unitOfWork.PatientRepository.AddPatientAsync(newPatient);

            await transaction.CommitAsync();

            return newUser;
        }



    }
}