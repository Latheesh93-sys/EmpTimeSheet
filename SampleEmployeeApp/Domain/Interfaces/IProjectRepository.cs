using SampleEmployeeApp.Domain.Models;
using System.Threading.Tasks;

namespace SampleEmployeeApp.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> AddAsync(Project project);
        Task<Project?> GetByIdAsync(int id);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> UpdateAsync(Project project);
    }
}
