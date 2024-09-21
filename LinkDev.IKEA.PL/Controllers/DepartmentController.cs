using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    // DepartmentController is a controller [Inheritance]
    // DepartmentController has a IDepartmentService [Composition]
    public class DepartmentController(IDepartmentService departmentService,
        ILogger<DepartmentController> logger,
        IWebHostEnvironment env) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var departments = departmentService.GetAllDepartments();
            return View(departments);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDTO department)
        {
           if(!ModelState.IsValid)
                return View(department);

           var message = string.Empty;


            try
            {
                var result = departmentService.CreateDepartment(department);
                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    message = "Failed to create department";
                    ModelState.AddModelError("", message);
                    return View(department);
                }
            }
            catch (Exception ex)
            {

                // 1. Log Exception
                logger.LogError(ex,ex.Message);

                // 2. Set Message

                if (env.IsDevelopment())
                {
                     message = ex.Message;
                    return View(department);

                }
                else
                {
                    message = "Department is not created :(";
                    return View("ErrorPage", message);

                }

               

            }
        }


        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = departmentService.GetDepartmentByID(id.Value);
            if (department is { })
                return View(department);
            return NotFound();
        }

    }
}
