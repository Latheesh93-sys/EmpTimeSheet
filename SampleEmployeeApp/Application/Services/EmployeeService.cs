using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Application.Interfaces;
using SampleEmployeeApp.Domain.Interfaces;
using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmpResponseDTO> AddAsync(AddEmployeeDTO employeeDTO)

        {
            var employee = new Employee
            {
                Name = employeeDTO.Name,
                Email = employeeDTO.Email,
                Password = employeeDTO.Password,
                Designation = employeeDTO.Designation
            };
            var createdEmployee = await _employeeRepository.AddAsync(employee);
            var empResponseDTO = new EmpResponseDTO
            {
                Id = createdEmployee.Id,
                Name = createdEmployee.Name,
                Email = createdEmployee.Email,
                Designation = createdEmployee.Designation
            };
            return empResponseDTO;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }
    }
}
