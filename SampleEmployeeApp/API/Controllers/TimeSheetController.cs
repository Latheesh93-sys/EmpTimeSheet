using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Application.Interfaces;
using SampleEmployeeApp.Domain.Models;
using SampleEmployeeApp.Infrastructure.Extensions;

namespace SampleEmployeeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeSheetController : ControllerBase
    {
        private readonly ITimeSheetService _timesheetService;

        public TimeSheetController(ITimeSheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        //POST: api/timesheet/{employeeId}
        [HttpPost]
        public async Task<IActionResult> AddTimesheet([FromBody] CreateTimesheetDTO timesheetDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var createdTimesheet = await _timesheetService.AddTimesheetAsync(timesheetDTO.EmployeeId, timesheetDTO);
            return Ok(createdTimesheet);
        }

        //GET: api/timesheet/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var timesheet = await _timesheetService.GetByIdAsync(id);
            if (timesheet == null)
                return NotFound(new { message = $"Timesheet with ID {id} not found." });
            return Ok(timesheet);
        }

        //PUT: api/timesheet/{id}/approve
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveTimesheet(int id)
        {
            var timesheet = await _timesheetService.ApproveTimesheetAsync(id);
            return Ok(timesheet);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var timesheets = await _timesheetService.GetAllAsync();
            return Ok(timesheets);
        }
    }
}

    
