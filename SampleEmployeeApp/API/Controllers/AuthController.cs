using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Application.Interfaces;
using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] EmpLoginDTO loginUser)
        {
            var user = await _authService.GetEmployee(loginUser);
            if (user == null)
                return Unauthorized();
            return Ok(user);
        }
    }
}
