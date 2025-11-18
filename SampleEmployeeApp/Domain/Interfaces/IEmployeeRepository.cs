using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddAsync(Employee employee);
        Task<Employee?> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();

        Task<PaginatedResult<Employee>> GetPaginatedResultAsync(string name,
        string designation,
        string sortBy,
        string sortOrder,
        int page,
        int pageSize);
    }
}
