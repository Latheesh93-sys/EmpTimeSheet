using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Application.Interfaces;
using SampleEmployeeApp.Domain.Interfaces;
using SampleEmployeeApp.Domain.Models;
using SampleEmployeeApp.Infrastructure.Repositories;

namespace SampleEmployeeApp.Application.Services
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly ITimeSheetRepository _timesheetRepository;

        public TimeSheetService(ITimeSheetRepository timesheetRepository)
        {
            _timesheetRepository = timesheetRepository;
        }

        public async Task<Timesheet> AddTimesheetAsync(int employeeId, CreateTimesheetDTO timesheetDTO)
        {
            var timesheet = new Timesheet
            {
                ProjectId = timesheetDTO.ProjectId,
                EmployeeId = timesheetDTO.EmployeeId,
                Date = timesheetDTO.WorkDate,
                HoursWorked = timesheetDTO.HoursWorked
            };
            var createdTimesheet = await _timesheetRepository.AddTimesheetAsync(employeeId, timesheet);
            return createdTimesheet;
        }

        public async Task<TimesheetDTO?> GetByIdAsync(int id)
        {
            return await _timesheetRepository.GetByIdAsync(id);
        }

        public async Task<Timesheet> ApproveTimesheetAsync(int id)
        {
            return await _timesheetRepository.ApproveTimesheetAsync(id);
        }

        public async Task<IEnumerable<TimesheetDTO>> GetAllAsync()
        {
            return await _timesheetRepository.GetAllAsync();
        }
    }
}
