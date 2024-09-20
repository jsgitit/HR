using HR.Core;
using Microsoft.EntityFrameworkCore;

namespace HR.Data;
public class SqlEmployeeData : IEmployeeData
{
    private readonly HRDbContext _db;

    public SqlEmployeeData(HRDbContext db)
    {
        _db = db;
    }

    public Employee Add(Employee newEmployee)
    {
        _db.Employees.Add(newEmployee);
        return newEmployee;
    }

    public int Commit()
    {
        return _db.SaveChanges();
    }

    public Employee Delete(int id)
    {
        var employee = GetById(id);
        if ( employee != null)
        {
            _db.Employees.Remove(employee);
        }
        return employee;
    }

    public Employee GetById(int id)
    {
        return _db.Employees.Find(id);
    }

    public IEnumerable<Employee> GetEmployeeByName(string name)
    {
        var query = from e in _db.Employees
                    where string.IsNullOrEmpty(name) || e.Name.ToLower().Contains(name.ToLower()) // Use ToLower for case-insensitive comparison
                    orderby e.Name
                    select e;
        return query;
    }

    public int GetEmployeeCount()
    {
        return _db.Employees.Count();
    }

    public Employee Update(Employee updatedEmployee)
    {
        var employee = _db.Employees.Attach(updatedEmployee);  // tells ef to start tracking changes on this entity
                                                                // all fields in the table will be updated to match the entity
        employee.State = EntityState.Modified;
        return updatedEmployee;
    }
}
