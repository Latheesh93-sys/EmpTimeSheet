using Microsoft.EntityFrameworkCore;
using SampleEmployeeApp.Application.DTOs;
using SampleEmployeeApp.Domain.Interfaces;
using SampleEmployeeApp.Domain.Models;
using SampleEmployeeApp.Infrastructure.Data;
using SampleEmployeeApp.Infrastructure.Extensions;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SampleEmployeeApp.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.Where(e=>e.Designation!="Admin").ToListAsync();
        }
        public async Task<PaginatedResult<Employee>> GetPaginatedResultAsync(string name,
        string designation,
        string sortBy,
        string sortOrder,
        int page,
        int pageSize)
        {
            var query = _context.Employees.Where(e => e.Designation != "Admin").AsQueryable();
            // Filter by name (if provided)
            if (!string.IsNullOrEmpty(name) && name != "All")
            {
                query = query.Where(c => c.Name == name);
            }
            //filter by designation
            if (!string.IsNullOrEmpty(designation) && designation != "All")
            {
                query = query.Where(c => c.Designation == designation);
            }
            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        query = sortOrder == "desc"
                            ? query.OrderByDescending(c => c.Name)
                            : query.OrderBy(c => c.Name);
                        break;

                    case "designation":
                        query = sortOrder == "desc"
                            ? query.OrderByDescending(c => c.Designation)
                            : query.OrderBy(c => c.Designation);
                        break;
                }
            }
            // Total count before pagination
            var totalCount = await query.CountAsync();

            // Pagination
            var items = await query.Paginate(page,pageSize);

            return items;
        }
    }
}
