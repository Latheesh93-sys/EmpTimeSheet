namespace SampleEmployeeApp.Application.DTOs
{
    public class AddEmployeeDTO
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Designation { get; set; } = null!;

        public string Password { get; set; }=null!;
    }
}
