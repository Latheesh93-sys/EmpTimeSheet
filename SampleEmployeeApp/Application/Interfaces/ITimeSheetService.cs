using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.Application.Interfaces
{
    public interface ITimeSheetService
    {
        Task<Timesheet> AddTimesheetAsync(int employeeId, CreateTimesheetDTO timesheetDTO);
        Task<TimesheetDTO?> GetByIdAsync(int id);
        Task<Timesheet> ApproveTimesheetAsync(int id);
        Task<IEnumerable<TimesheetDTO>> GetAllAsync();
    }
}
