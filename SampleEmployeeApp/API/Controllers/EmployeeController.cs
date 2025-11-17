using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Application.Interfaces;
using SampleEmployeeApp.Application.Services;
using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMemoryCache _memoryCache;
        private const string EmployeesCacheKey = "employees_cache";
        public EmployeeController(IEmployeeService employeeService,IMemoryCache memoryCache)
        {
            _employeeService = employeeService;
            _memoryCache = memoryCache;
        }

        //GET: api/employee
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!_memoryCache.TryGetValue(EmployeesCacheKey, out IEnumerable<Employee> employees))
            {
                // Not in cache, fetch from database
                employees = await _employeeService.GetAllAsync();

                // Set cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))  // reset expiration if accessed
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));   // max lifetime

                // Store in cache
                _memoryCache.Set(EmployeesCacheKey, employees, cacheEntryOptions);
            }
            return Ok(employees);
        }

        //GET: api/employee/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound(new { message = $"Employee with ID {id} not found." });

            return Ok(employee);
        }

        //POST: api/employee
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddEmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdEmployee = await _employeeService.AddAsync(employeeDTO);
            _memoryCache.Remove(EmployeesCacheKey);
            return Ok(createdEmployee);
        }
    }
}
