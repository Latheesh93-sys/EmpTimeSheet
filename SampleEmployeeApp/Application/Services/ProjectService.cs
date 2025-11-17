using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Application.Interfaces;
using SampleEmployeeApp.Domain.Interfaces;
using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectResponseDTO> AddAsync(AddProjectDTO projectDTO)
        {
            var project = new Project
            {
                Name = projectDTO.Name,
                StartDate = projectDTO.StartDate,
                EndDate = projectDTO.EndDate,
                Primarymanager = projectDTO.Primarymanager,
                Secondarymanager = projectDTO.Secondarymanager,
                EmployeeProjects = projectDTO.EmployeeIds.Select(id => new EmployeeProject
                {
                    EmployeeId = id
                }).ToList()
            };
            var createdProject = await _projectRepository.AddAsync(project);
            var projectResponseDTO = new ProjectResponseDTO
            {
                Id = createdProject.Id,
                Name = createdProject.Name,
                StartDate = createdProject.StartDate,
                EndDate = createdProject.EndDate,
                Primarymanager = createdProject.Primarymanager,
                Secondarymanager = createdProject.Secondarymanager,
                EmployeeIds = createdProject.EmployeeProjects
                                ?.Select(ep => ep.EmployeeId)
                                 .ToList() ?? new List<int>()
            };
            return projectResponseDTO;

        }

        public async Task<ProjectResponseDTO?> GetByIdAsync(int id)
        {

            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return null;
            }
            var projectDTO = new ProjectResponseDTO
            {
                Id = project.Id,
                Name = project.Name,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Primarymanager = project.Primarymanager,
                Secondarymanager = project.Secondarymanager
            };
            return projectDTO;
        }
        public async Task<IEnumerable<ProjectResponseDTO>> GetAllAsync()
        {
            var projects= await _projectRepository.GetAllAsync();
            var projectDtos = projects.Select(p => new ProjectResponseDTO
            {
                Id = p.Id,
                Name = p.Name,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Primarymanager = p.Primarymanager,
                Secondarymanager = p.Secondarymanager,
                EmployeeIds = p.EmployeeProjects.Select(ep => ep.EmployeeId).ToList()
            });
            return projectDtos;
        }

        public async Task<ProjectResponseDTO> UpdateProjectAsync(int id, AddProjectDTO dto)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                return null;

            // Update main fields
            project.Name = dto.Name;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;
            project.Primarymanager = dto.Primarymanager;
            project.Secondarymanager = dto.Secondarymanager;

            // Handle EmployeeProjects
            var newEmployeeIds = dto.EmployeeIds ?? new List<int>();
            var currentEmployeeIds = project.EmployeeProjects.Select(ep => ep.EmployeeId).ToList();

            // Remove old employees
            project.EmployeeProjects = project.EmployeeProjects
                .Where(ep => newEmployeeIds.Contains(ep.EmployeeId))
                .ToList();

            // Add new employees
            var toAdd = newEmployeeIds.Except(currentEmployeeIds);
            foreach (var empId in toAdd)
            {
                project.EmployeeProjects.Add(new EmployeeProject
                {
                    EmployeeId = empId,
                    ProjectId = id
                });
            }
            var updatedProject = await _projectRepository.UpdateAsync(project);
            var projectResponseDTO = new ProjectResponseDTO
            {
                Id = updatedProject.Id,
                Name = updatedProject.Name,
                StartDate = updatedProject.StartDate,
                EndDate = updatedProject.EndDate,
                Primarymanager = updatedProject.Primarymanager,
                Secondarymanager =  updatedProject.Secondarymanager,
                EmployeeIds = updatedProject.EmployeeProjects
                                ?.Select(ep => ep.EmployeeId)
                                 .ToList() ?? new List<int>()
            };
            return projectResponseDTO;
        }
    }
}
