using HR.Core;
using HR.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR.App.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeData EmployeeData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Employee Employee { get; set; }
        public IEnumerable<SelectListItem> EmployeeTypes { get; set; }

        public EditModel(IEmployeeData EmployeeData, IHtmlHelper htmlHelper)
        {
            this.EmployeeData = EmployeeData;
            this.htmlHelper = htmlHelper; // for EmployeeType option values.
        }
        public IActionResult OnGet(int? EmployeeId) // nullable due to Add New scenario
        {
            EmployeeTypes = htmlHelper.GetEnumSelectList<EmployeeType>();
            if (EmployeeId.HasValue)
            {
                Employee = EmployeeData.GetById(EmployeeId.Value);
            }
            else
            {
                Employee = new Employee();
                // could set some defaults here if needed.
            }

            if (Employee == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // asp.net core is stateless, so rebuild of EmployeeTypes is required when posting
                EmployeeTypes = htmlHelper.GetEnumSelectList<EmployeeType>();
                return Page();
            }

            if (Employee.Id > 0)
            {
                // assumes the id is the same that's on the form.
                EmployeeData.Update(Employee);

            }
            else
            {
                EmployeeData.Add(Employee);
            }

            EmployeeData.Commit();
            TempData["Message"] = "Employee saved.";
            return RedirectToPage("./Detail", new { EmployeeId = Employee.Id });

        }
    }
}
