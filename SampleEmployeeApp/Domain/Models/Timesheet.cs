using System.ComponentModel.DataAnnotations;

namespace SampleEmployeeApp.Domain.Models
{
    public class Timesheet
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public DateTime Date { get; set; }
        public double HoursWorked { get; set; }

        [MaxLength(500)] 
        public string Description { get; set; }

        public bool IsApproved { get; set; } = false;

    }
}
