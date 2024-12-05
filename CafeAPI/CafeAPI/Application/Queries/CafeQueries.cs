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
            
            var cafes = context.Cafes.OrderByDescending(c=>c.Employees);
            
            if (string.IsNullOrEmpty(location) || location == "default")
            {
                var result = await cafes.Select(c => new Cafe
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
            if (!string.IsNullOrEmpty(location))
            {
                var result = await cafes.Where(x=>x.Location==location).Select(c => new Cafe
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
            //var cafeid = new Guid(cafe);
            var cafeid = context.Cafes.Where(x => x.Name == cafe).Select(x => x.Id).FirstOrDefault();
            var employee = context.Employees.Where(e => e.CafeId == cafeid);

            var employees = await employee.Select(e => new Employee
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Phone = e.Phone,
                DaysWorked = (int)(DateTime.UtcNow - e.StartDate).TotalDays,
                Cafe = e.Cafe.Name,
                Gender = e.Gender,
                StartDate = e.StartDate,
            }).OrderByDescending(e => e.DaysWorked).ToListAsync();

            if (employees is null)
                throw new KeyNotFoundException();

            return employees;
        }
    }

}
