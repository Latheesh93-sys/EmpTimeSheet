using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectResponseDTO> AddAsync(AddProjectDTO projectDTO);
        Task<ProjectResponseDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ProjectResponseDTO>> GetAllAsync();
        Task<ProjectResponseDTO> UpdateProjectAsync(int id, AddProjectDTO dto);
    }
}
