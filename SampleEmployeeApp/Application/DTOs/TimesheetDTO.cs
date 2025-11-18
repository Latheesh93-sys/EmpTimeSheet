using System.ComponentModel.DataAnnotations;

namespace SampleEmployeeApp.Application.DTOs
{
    public class TimesheetDTO
    {
        public int Id { get; set; }
        public int ProjectId {  get; set; }
        public string EmployeeName { get; set; } = null!;
        public string ProjectName { get; set; } = null!;
        public DateTime Date { get; set; }
        public double HoursWorked { get; set; }
        public string Description { get; set; }
        public Boolean IsApproved { get; set; }
    }
}
