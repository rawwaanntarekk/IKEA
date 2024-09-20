using LinkDev.IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    // DepartmentController is a controller [Inheritance]
    // DepartmentController has a IDepartmentService [Composition]
    public class DepartmentController(IDepartmentService departmentService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
