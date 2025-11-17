namespace SampleEmployeeApp.Domain.Models
{
    public class EmployeeProject
    {
        public int Id { get; set; } 

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
}
