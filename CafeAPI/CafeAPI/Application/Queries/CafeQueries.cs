using Cafe.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CafeAPI.Application.Queries
{

    public class CafeQueries(CafeContext context)
        : ICafeQueries
    {
        public async Task<IEnumerable<Cafe>> GetCafeByLocationAsync(string location)
        {
            
            var cafes = context.Cafes.AsQueryable(); 
            if (!string.IsNullOrEmpty(location)) { 
                cafes = cafes.Where(c => c.Location.Equals(location, StringComparison.OrdinalIgnoreCase));
            }
            if (location == "default")
            {
                var result = await context.Cafes.OrderByDescending(c => c.Employees).Select(c => new Cafe
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Employees = c.Employees,
                    Logo = c.Logo,
                    Location = c.Location

                }).ToListAsync();
                return (IEnumerable<Cafe>)result;
            }
            IEnumerable<Cafe> emptyList = Enumerable.Empty<Cafe>();
            return (IEnumerable<Cafe>)emptyList;
            
        }


        public async Task<IEnumerable<Employee>> GetEmployeesByCafe(string cafe)
        {
            var cafeid = new Guid(cafe);
            var employee = context.Employees.Where(e => string.IsNullOrEmpty(cafe) || e.CafeId == cafeid);

            var employees = await employee.Select(e => new Employee
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Phone = e.Phone,
                DaysWorked = (int)(DateTime.UtcNow - e.StartDate).TotalDays,
                Cafe = e.Cafe.Name
            }).OrderByDescending(e => e.DaysWorked).ToListAsync();

            if (employees is null)
                throw new KeyNotFoundException();

            return employees;
        }
    }

}
