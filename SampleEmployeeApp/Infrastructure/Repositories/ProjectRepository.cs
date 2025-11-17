using Microsoft.EntityFrameworkCore;
using SampleEmployeeApp.Domain.Interfaces;
using SampleEmployeeApp.Domain.Models;
using SampleEmployeeApp.Infrastructure.Data;
using System;

namespace SampleEmployeeApp.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project> AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }


        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.EmployeeProjects)
                .ThenInclude(ep => ep.Employee)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            var projects = await _context.Projects
                            .Include(p => p.EmployeeProjects)
                            .ThenInclude(ep => ep.Employee)
                            .ToListAsync(); 
            return projects;
        }

        public async Task<Project> UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

    }
}
