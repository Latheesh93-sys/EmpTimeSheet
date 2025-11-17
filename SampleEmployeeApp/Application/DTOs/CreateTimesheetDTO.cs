namespace SampleEmployeeApp.Application.DTOs
{
    public class CreateTimesheetDTO
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public double HoursWorked { get; set; }
    }
}
