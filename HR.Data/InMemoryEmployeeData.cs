using HR.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data;
public class InMemoryEmployeeData : IEmployeeData
{
    private readonly List<Employee> employees;

    public InMemoryEmployeeData()
    {
        employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Sarah", Location = "USA", EmployeeType = EmployeeType.FullTimeEmployee },
            new Employee { Id = 2, Name = "Sarah", Location = "Italy", EmployeeType = EmployeeType.Contractor },
            new Employee { Id = 3, Name = "Sarah", Location = "Germany", EmployeeType = EmployeeType.Vendor }
        };
    }
    public Employee Add(Employee newEmployee)
    {
        employees.Add(newEmployee);
        newEmployee.Id = employees.Max(e => e.Id) + 1;
        return newEmployee;
    }

    public int Commit()
    {
        return 0;
    }

    public Employee Delete(int id)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee != null)
        {
            employees.Remove(employee);
        }
        return employee;
    }

    public Employee GetById(int id)
    {
        return employees.SingleOrDefault(e => e.Id == id);
    }

    public IEnumerable<Employee> GetEmployeeByName(string name)
    {
        return from e in employees
               where string.IsNullOrEmpty(name) || e.Name.Contains(name, System.StringComparison.InvariantCultureIgnoreCase)
               orderby e.Name
               select e;
    }

    public int GetEmployeeCount()
    {
        return employees.Count();
    }

    public Employee Update(Employee updatedEmployee)
    {
        var employee = employees.SingleOrDefault(e => e.Id == updatedEmployee.Id);
        if (employee!= null)
        {
            employee.Name = updatedEmployee.Name;
            employee.Location = updatedEmployee.Location;
            employee.EmployeeType= updatedEmployee.EmployeeType;

        }
        return employee;
    }
}
