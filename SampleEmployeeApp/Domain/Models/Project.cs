namespace SampleEmployeeApp.Domain.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Primarymanager { get; set; } = null!;

        public string Secondarymanager { get; set;} = null!;

        public ICollection<Employee> Employees { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
