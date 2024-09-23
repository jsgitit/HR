using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HR.Core;
using HR.Data;

namespace HR.App.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeData EmployeeData;
        public Employee Employee { get; set; }
        public DeleteModel(IEmployeeData EmployeeData)
        {
            this.EmployeeData = EmployeeData;
        }
        public IActionResult OnGet(int EmployeeId)
        {
            Employee = EmployeeData.GetById(EmployeeId);
            if (Employee == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
        public IActionResult OnPost(int EmployeeId)
        {
            var Employee = EmployeeData.Delete(EmployeeId);
            EmployeeData.Commit();
            if (Employee == null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{Employee.Name} was deleted.";
            return RedirectToPage("./List");

        }
    }
}
