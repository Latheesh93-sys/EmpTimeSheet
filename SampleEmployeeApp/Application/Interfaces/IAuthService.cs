using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.Application.Interfaces
{
    public interface IAuthService
    {
        public  Task<EmpLoginResponseDTO> GetEmployee(EmpLoginDTO user);
    }
}
