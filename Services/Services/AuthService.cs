using CommerceDAL.DAO;
using CommerceDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.Data;

namespace Services.Services
{
    public class AuthService
    {
        private readonly UserDAO _uDAO;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userM;
        private readonly SignInManager<IdentityUser> _signInM;

        public AuthService(UserDAO uDAO, IConfiguration configuration, UserManager<IdentityUser> userM, SignInManager<IdentityUser> signInM)
        {
            _uDAO = uDAO;
            _configuration = configuration;
            _userM = userM;
            _signInM = signInM;
        }
        public async Task<IdentityResult> Register(RegisterRequest registerRequest) 
        {
            var user = new IdentityUser
            {
                UserName = registerRequest.Email,
                Email = registerRequest.Email
            };
            var result = await _userM.CreateAsync(user, registerRequest.Password);
            return result;
        }
        public bool Verify(string enteredPass, string storedHash) 
        {
            var hash = new PasswordHasher<Users>();
            var result = hash.VerifyHashedPassword(null, enteredPass, storedHash);
            return result == PasswordVerificationResult.Success;
        }
        public string GenerateJwtToken(Users user)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var expiration = Convert.ToInt32(_configuration["JwtSettings:ExpirationMinutes"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")

            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(issuer: issuer, 
                audience: audience, 
                claims:claims, 
                expires:DateTime.Now.AddMinutes(expiration), 
                signingCredentials:credential);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
