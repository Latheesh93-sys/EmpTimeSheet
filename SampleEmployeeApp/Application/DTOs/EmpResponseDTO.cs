namespace SampleEmployeeApp.Application.DTOs
{
    public class EmpResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Designation { get; set; } = null!;
    }
}
