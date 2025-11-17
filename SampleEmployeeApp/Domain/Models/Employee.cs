namespace SampleEmployeeApp.Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Designation { get; set; } = null!;

        public string Password { get; set; } = null!;

        public ICollection<Project> Projects { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();

    }

}
