namespace SampleEmployeeApp.Application.DTOs
{
    public class ProjectResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Primarymanager { get; set; } = null!;

        public string Secondarymanager { get; set; } = null!;

        public List<int> EmployeeIds { get; set; } = new();
    }
}
