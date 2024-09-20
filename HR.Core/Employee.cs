using System.ComponentModel.DataAnnotations;

namespace HR.Core;

public class Employee
{
    public int Id { get; set; }
    [Required, StringLength(80)]
    public string Name { get; set; }
    [Required, StringLength(255)]
    public string Location { get; set; }
    public EmployeeType EmployeeType { get; set; }
}
