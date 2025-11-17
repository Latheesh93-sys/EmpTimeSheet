using SampleEmployeeApp.Application.Interfaces;
using SampleEmployeeApp.Application.Services;
using SampleEmployeeApp.Domain.Interfaces;
using SampleEmployeeApp.Infrastructure.Repositories;

namespace SampleEmployeeApp.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITimeSheetRepository,TimesheetRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITimeSheetService,  TimeSheetService>();
        }
    }
}
