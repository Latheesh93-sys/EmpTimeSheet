using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.Domain.Interfaces
{
    public interface ITimeSheetRepository
    {
        Task<Timesheet> AddTimesheetAsync(int employeeId, Timesheet timesheet);
        Task<TimesheetDTO?> GetByIdAsync(int id);
        Task<Timesheet> ApproveTimesheetAsync(int id);
        Task<IEnumerable<TimesheetDTO>> GetAllAsync();
    }
}
