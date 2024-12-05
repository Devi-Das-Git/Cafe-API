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
        var existingCafe = _context.Cafes.Where(c=>c.Id==Cafe.Id).FirstOrDefault();

        if (existingCafe != null) {
            existingCafe.Location = Cafe.Location;
            existingCafe.Description = Cafe.Description;
            existingCafe.Name = Cafe.Name;
            
            _context.Update(existingCafe);
        }
        
        return _context.SaveChanges();

    }
    public int UpdateEmployee(Cafe.Domain.Models.Employee employee)
    {
        var existingEmployee = _context.Employees.Find(employee.Id);
        if (existingEmployee != null)
        {
            existingEmployee.Name = employee.Name;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.Email = employee.Email;
            existingEmployee.CafeId = employee.CafeId;
            _context.Update(existingEmployee);
        }
        return _context.SaveChanges();

    }
}
