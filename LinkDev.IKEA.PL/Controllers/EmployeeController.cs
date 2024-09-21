using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController(IEmployeeService employeeService,
        ILogger<EmployeeController> logger,
        IWebHostEnvironment env) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var employees = employeeService.GetAllEmployees();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDTO employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var message = string.Empty;


            try
            {
                var result = employeeService.CreateEmployee(employee);
                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    message = "Failed to add new employee";
                    ModelState.AddModelError("", message);
                    return View(employee);
                }
            }
            catch (Exception ex)
            {

                // 1. Log Exception
                logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = env.IsDevelopment() ? ex.Message : "Employee is not added :(";


            }
            ModelState.AddModelError("", message);
            return View(employee);
        }

        

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = employeeService.GetEmployee(id.Value);
            if (department is { })
                return View(department);
            return NotFound();
        }

        //[HttpGet]
        //public IActionResult Update(int? id)
        //{
        //    if (id is null)
        //        return BadRequest();

        //    var department = employeeService.GetEmployee(id.Value);

        //    if (department is { })
        //        return View(new DepartmentUpdateViewModel()
        //        {
                   
        //        });

        //    return NotFound();

        //}

        //[HttpPost]
        //public IActionResult Update([FromRoute] int id, DepartmentUpdateViewModel departmentVM)
        //{
        //    if (!ModelState.IsValid)
        //        return View(departmentVM);

        //    var message = string.Empty;

        //    try
        //    {
        //        var employee = new UpdatedEmployeeDTO()
        //        {
                  

        //        };

        //        var result = employeeService.UpdateEmployee(employee);

        //        if (result > 0)
        //            return RedirectToAction(nameof(Index));

        //        message = "Failed to update department";
        //    }
        //    catch (Exception ex)
        //    {
        //        // 1. Log Exception
        //        logger.LogError(ex, ex.Message);

        //        // 2. Set Message
        //        message = env.IsDevelopment() ? ex.Message : "Department is not updated :(";

        //    }

        //    ModelState.AddModelError("", message);
        //    return View(departmentVM);




        //}


        #region Delete Action
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = employeeService.GetEmployee(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = employeeService.DeleteEmployee(id);

                if (deleted)
                    return RedirectToAction(nameof(Index));

                message = "an error has occured during deleting the department :(";
            }
            catch (Exception ex)
            {

                // 1. Log Exception
                logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = env.IsDevelopment() ? ex.Message : "Department is not created :(";

            }

            //ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

            #endregion







        }
    }
}
