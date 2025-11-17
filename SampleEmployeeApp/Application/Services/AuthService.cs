using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Application.Interfaces;
using SampleEmployeeApp.Domain.Interfaces;
using SampleEmployeeApp.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace SampleEmployeeApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _authRepository;
        public AuthService(IConfiguration config, IAuthRepository authRepository)
        {
            _config = config;
            _authRepository = authRepository;

        }
        public string GenerateToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),   
                Issuer = _config["Jwt:Issuer"],         
                Audience = _config["Jwt:Audience"],    
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public async Task<EmpLoginResponseDTO> GetEmployee(EmpLoginDTO user)
        {
            var emp = new Employee
            {
                Email = user.Email,
                Password = user.Password
            };
            var empLoginResponseDTO = new EmpLoginResponseDTO();
            Employee loginuser = await _authRepository.GetUserAsync(emp);
            
            if (loginuser == null)
                return null;

            var token = GenerateToken(loginuser.Email);
            empLoginResponseDTO.Id = loginuser.Id;
            empLoginResponseDTO.Name = loginuser.Name;
            empLoginResponseDTO.Email = loginuser.Email;
            empLoginResponseDTO.Designation = loginuser.Designation;
            empLoginResponseDTO.Token = token;
            return empLoginResponseDTO;
        }
    }

}