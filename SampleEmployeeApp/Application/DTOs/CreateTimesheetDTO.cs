using System.ComponentModel.DataAnnotations;

namespace SampleEmployeeApp.Application.DTOs
{
    public class CreateTimesheetDTO
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime WorkDate { get; set; }
        public double HoursWorked { get; set; }
        public string Description { get; set; }
    }
}
