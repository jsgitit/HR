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
    public class DetailModel : PageModel
    {
        private readonly IEmployeeData EmployeeData;

        //output model
        [TempData]
        public string Message { get; set; }
        public Employee Employee { get; set; }
        public DetailModel(IEmployeeData EmployeeData)
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
            return Page(); // renders Detail.cshtml
        }
    }
}
