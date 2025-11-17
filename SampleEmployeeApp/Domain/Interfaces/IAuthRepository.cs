using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<Employee> GetUserAsync(Employee emp);
    }
}
