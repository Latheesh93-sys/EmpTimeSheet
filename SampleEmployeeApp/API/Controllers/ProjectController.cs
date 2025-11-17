using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Application.Interfaces;
using SampleEmployeeApp.Application.Services;
using SampleEmployeeApp.Domain.Models;

namespace SampleEmployeeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMemoryCache _memoryCache;
        private const string ProjectsCacheKey = "projects_cache";

        public ProjectController(IProjectService projectService, IMemoryCache memoryCache)
        {
            _projectService = projectService;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddProjectDTO projectDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdProject = await _projectService.AddAsync(projectDTO);
            _memoryCache.Remove(ProjectsCacheKey);
            return Ok(createdProject);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound(new { message = $"Project with ID {id} not found." });
            return Ok(project);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!_memoryCache.TryGetValue(ProjectsCacheKey, out IEnumerable<ProjectResponseDTO> projects))
            {
                // Not in cache, fetch from database
                projects = await _projectService.GetAllAsync();

                // Set cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))  // reset expiration if accessed
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));   // max lifetime

                // Store in cache
                _memoryCache.Set(ProjectsCacheKey, projects, cacheEntryOptions);
            }
            return Ok(projects);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AddProjectDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedProject = await _projectService.UpdateProjectAsync(id, dto);

            if (updatedProject == null)
                return NotFound($"Project with Id {id} not found.");

            _memoryCache.Remove(ProjectsCacheKey);
            return Ok(updatedProject);
        }
    }
}
