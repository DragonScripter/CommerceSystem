using CommerceDAL.DAO;
using CommerceDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AuthService
    {
        private readonly UserDAO _uDAO;

        public AuthService(UserDAO uDAO)
        {
            _uDAO = uDAO;
        }

        public bool Verify(string enteredPass, string storedHash) 
        {
            var hash = new PasswordHasher<Users>();
            var result = hash.VerifyHashedPassword(null, enteredPass, storedHash);
            return result == PasswordVerificationResult.Success;
        }
        public string GenerateJwtToken(Users user)
        {
           
            return "something";
        }
    }
}
