using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using HR.Core;
using HR.Data;

namespace HR.App.Pages.Employees
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IEmployeeData EmployeeData;

        public string Message { get; set; }
        public IEnumerable<Employee> Employees { get; set; }

        [BindProperty(SupportsGet = true)] // allows searchTerm value to be preserved on the form.
        public string SearchTerm { get; set; }
        public ListModel(IConfiguration config, IEmployeeData EmployeeData)

        {
            this.config = config;
            this.EmployeeData = EmployeeData;
        }
        public void OnGet(string searchTerm) // "searchTerm" matches name in Request from search form.
                                                // then it model binds to the Request.searchTerm value
                                                // but page refreshes will pass in a null, which is ok for string
        {
            

            Message = config["Message"];
            Employees = EmployeeData.GetEmployeeByName(SearchTerm);
        }
    }
}
