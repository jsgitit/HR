using HR.Data;
using Microsoft.AspNetCore.Mvc;

namespace HR.App.ViewComponents
{
    public class EmployeeCountViewComponent : ViewComponent
    {
        private readonly IEmployeeData EmployeeData;

        public EmployeeCountViewComponent(IEmployeeData EmployeeData)
        {
            this.EmployeeData = EmployeeData;
        }
        public IViewComponentResult Invoke()
        {
            var count = EmployeeData.GetEmployeeCount();
            return View(count);  // View() with no specific view name param, will render the "Default.cshtml"
        }
    }
}
