namespace SampleEmployeeApp.Application.DTOs
{
    public class EmpLoginResponseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Designation { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
