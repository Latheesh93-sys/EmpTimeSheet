using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmpResponseDTO> AddAsync(AddEmployeeDTO employeeDTO);
        Task<Employee?> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
    }
}
