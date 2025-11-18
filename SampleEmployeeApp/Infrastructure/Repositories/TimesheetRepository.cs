using SampleEmployeeApp.Domain.Models;
using SampleEmployeeApp.Infrastructure.Data;
using SampleEmployeeApp.Domain.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using SampleEmployeeApp.Application.DTOs;

namespace SampleEmployeeApp.Infrastructure.Repositories
{
    public class TimeSheetRepository : ITimeSheetRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeSheetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Timesheet> AddTimesheetAsync(int employeeId, Timesheet timesheet)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
                throw new Exception($"Employee with ID {employeeId} not found.");

            await _context.Timesheets.AddAsync(timesheet);
            await _context.SaveChangesAsync();

            return timesheet;
        }

        public async Task<TimesheetDTO?> GetByIdAsync(int id)
        {
            return await _context.Timesheets
                .Include(t => t.Employee)
                .Include(t => t.Project)
                .Where(t => t.Id == id)
                .Select(t => new TimesheetDTO
                {
                    Id = t.Id,
                    EmployeeName = t.Employee.Name,
                    ProjectName = t.Project.Name,
                    Date = t.Date,
                    Description= t.Description,
                    HoursWorked = t.HoursWorked
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Timesheet> ApproveTimesheetAsync(int id)
        {
            var timesheet = await _context.Timesheets.FindAsync(id);
            if (timesheet == null)
                throw new Exception($"Timesheet with ID {id} not found.");

            timesheet.IsApproved = true;

            await _context.SaveChangesAsync();
            return timesheet;
        }

        public async Task<IEnumerable<TimesheetDTO>> GetAllAsync()
        {
            var timesheets = await _context.Timesheets
            .Include(t => t.Employee)
            .Include(t => t.Project)
            .Select(t => new TimesheetDTO
                {
                    Id = t.Id,
                    EmployeeName = t.Employee.Name,
                    ProjectId=t.Project.Id,
                    ProjectName = t.Project.Name,
                    Date = t.Date,
                    IsApproved = t.IsApproved,
                    Description= t.Description,
                    HoursWorked = t.HoursWorked
                })
                .ToListAsync();

            return timesheets;
        }
    }
}
