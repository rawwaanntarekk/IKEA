using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Models.Common.Enums;
using LinkDev.IKEA.PL.ViewModels.Departments;
using LinkDev.IKEA.PL.ViewModels.Employees;
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
        [ValidateAntiForgeryToken]
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

            var employee = employeeService.GetEmployee(id.Value);
            if (employee is { })
                return View(employee);
            return NotFound();
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = employeeService.GetEmployee(id.Value);

            if (employee is { })
                return View(new EmployeeUpdateVM
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
        public IActionResult Update([FromRoute] int id, EmployeeUpdateVM employeeUpdateVM)
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
                    return RedirectToAction(nameof(Index));

                message = "Failed to update department";
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
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = employeeService.DeleteEmployee(id);

                if (deleted)
                    return RedirectToAction(nameof(Index));

                message = "an error has occured during deleting the employee :(";
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

            #endregion







        }
    }
}
