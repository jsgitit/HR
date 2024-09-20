using HR.Core;

namespace HR.Data;
public interface IEmployeeData
{
    IEnumerable<Employee> GetEmployeeByName(string name);
    Employee GetById(int id);
    Employee Add(Employee newEmployee);
    Employee Update(Employee updatedEmployee);
    Employee Delete(int id);
    int GetEmployeeCount();
    int Commit(); // SaveChanges();
}
