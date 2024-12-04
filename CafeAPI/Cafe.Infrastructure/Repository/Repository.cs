using Cafe.Domain.Repository;
using Cafe.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace eShop.Ordering.Infrastructure.Repositories;

public class CafeRepository
    : ICafeRepository
{
    private readonly CafeContext _context;


    public CafeRepository(CafeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public int Add(Cafe.Domain.Models.Cafe Cafe)
    {
        _context.Add(Cafe);
         int result= _context.SaveChanges();
        return result;

    }
    public int AddEmployee(Cafe.Domain.Models.Employee Employee)
    {
        _context.Add(Employee);
        return _context.SaveChanges();

    }
    public int RemoveCafe(Guid Id)
    {
        var cafe = _context.Cafes.FirstOrDefault(c=>c.Id==Id);
        var employee = _context.Employees.Where(x=>x.CafeId == Id);

        if (cafe != null)
        {
            _context.RemoveRange(employee);
            _context.RemoveRange(cafe);
        }
        return _context.SaveChanges();
    }

    public int RemoveEmployee(Guid Id)
    {
        var Id1 = Id.ToString().Split('-');
        var empid = "UI" + Id1[0].Substring(0, 7);
        var cafe = _context.Employees.FirstOrDefault(c => c.Id == empid);
        _context.Remove(cafe);
        return _context.SaveChanges();
    }

    public int UpdateCafe(Cafe.Domain.Models.Cafe Cafe)
    {
        _context.Update(Cafe);
        return _context.SaveChanges();

    }
    public int UpdateEmployee(Cafe.Domain.Models.Employee employee)
    {
        employee.CafeId = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA");
        _context.Update(employee);
        return _context.SaveChanges();

    }
}
