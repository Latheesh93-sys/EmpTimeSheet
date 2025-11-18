using Microsoft.EntityFrameworkCore;
using SampleEmployeeApp.Application.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SampleEmployeeApp.Infrastructure.Extensions
{
    public static class PaginationExtensions
    {
        public static async Task<PaginatedResult<T>> Paginate<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var result = new PaginatedResult<T>
            {
                TotalCount = await query.CountAsync()
            };
            result.Items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            return result;
        }
    }
}
