using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController(IEmployeeService employeeService,
        ILogger<EmployeeController> logger,
        IWebHostEnvironment env) : Controller
    {
        [HttpGet]
        public IActionResult Index(string Search)
        {
            // ViewData is a Dictionary Type Property (Introduced in ASP.NET Framework 3.0)
            // Helps to pass data from Controller [Action] to View
            ViewData["Message"] = "Hello ViweData";

            // ViewBag is a Dynamic Property (Introduced in ASP.NET Framework 4.0)
            // Helps to pass data from Controller [Action] to View

            ViewBag.Message = "Hello ViewBag";


            var employees = employeeService.GetEmployees(Search);
            return View(employees);
        }
        public IActionResult Search(string Search)
        {
            var employees = employeeService.GetEmployees(Search);
            return PartialView("Partials/EmployeeListPartial", employees);
        }

        #region Create
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
                {
                    message = "Employee is added successfully";
                    TempData["Message"] = message;
                    TempData["Success"] = true;
                    return RedirectToAction("Index");
                }
                else
                {
                    message = "Failed to add new employee";
                    TempData["Message"] = message;
                    TempData["Success"] = false;
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
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = employeeService.GetEmployee(id.Value);
            if (employee is { })
                return View(employee);
            return NotFound();
        } 
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = employeeService.GetEmployee(id.Value);

            if (employee is { })
                return View(new EmployeeViewModel
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    HiringDate = employee.HiringDate,
                }
               );

            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int id, EmployeeViewModel employeeUpdateVM)
        {
            if (!ModelState.IsValid)
                return View(employeeUpdateVM);

            var message = string.Empty;

            try
            {
                var employee = new UpdatedEmployeeDTO()
                {
                    Id = id,
                    Name = employeeUpdateVM.Name,
                    Age = employeeUpdateVM.Age,
                    Email = employeeUpdateVM.Email,
                    Phone = employeeUpdateVM.Phone,
                    Address = employeeUpdateVM.Address,
                    Salary = employeeUpdateVM.Salary,
                    IsActive = employeeUpdateVM.IsActive,
                    HiringDate = employeeUpdateVM.HiringDate,


                };

                var result = employeeService.UpdateEmployee(id, employee);

                if (result > 0)
                {
                    message = "Department is updated successfully";
                    TempData["Message"] = message;
                    TempData["Success"] = true;
                    return RedirectToAction("Index");

                }

                message = "Failed to update department";
                TempData["Message"] = message;
                TempData["Success"] = false;
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = env.IsDevelopment() ? ex.Message : "Department is not updated :(";

            }

            ModelState.AddModelError("", message);
            return View(employeeUpdateVM);




        } 
        #endregion

        #region Delete Action
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = employeeService.GetEmployee(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = employeeService.DeleteEmployee(id);

                if (deleted)
                {
                    message = "Employee is deleted successfully";
                    TempData["Message"] = message;
                    TempData["Success"] = true;
                    return RedirectToAction(nameof(Index));
                }

                message = "an error has occured during deleting the employee :(";
                TempData["Message"] = message;
                TempData["Success"] = false;
            }
            catch (Exception ex)
            {

                // 1. Log Exception
                logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = env.IsDevelopment() ? ex.Message : "Employee is not created :(";

            }

            //ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));








        }
        #endregion

    }
}
