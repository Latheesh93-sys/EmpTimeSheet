using Microsoft.EntityFrameworkCore;
using SampleEmployeeApp.Domain.Interfaces;
using SampleEmployeeApp.Domain.Models;
using SampleEmployeeApp.Infrastructure.Data;

namespace SampleEmployeeApp.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext dbContext;
        public AuthRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Employee> GetUserAsync(Employee loginEmp)
        {
            var user = await dbContext.Employees.FirstOrDefaultAsync(c => c.Email == loginEmp.Email && c.Password == loginEmp.Password);
            return user;
        }
    }
}
