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
            // ViewData is a Dictionary Type Property (Introduced in ASP.NET Framework 3.0)
            // Helps to pass data from Controller [Action] to View
            ViewData["Message"] = "Hello ViweData";

            // ViewBag is a Dynamic Property (Introduced in ASP.NET Framework 4.0)
            // Helps to pass data from Controller [Action] to View

            ViewBag.Message = "Hello ViewBag";


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
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);

            var message = string.Empty;


            try
            {
                var CreatedEmployee = new CreatedEmployeeDTO()
                {
                    Name = employeeVM.Name,
                    Age = employeeVM.Age,
                    Email = employeeVM.Email,
                    Phone = employeeVM.Phone,
                    Address = employeeVM.Address,
                    Salary = employeeVM.Salary,
                    IsActive = employeeVM.IsActive,
                    HiringDate = employeeVM.HiringDate,
                    Gender = employeeVM.Gender,
                    EmployeeType = employeeVM.EmployeeType
                };
                var result = employeeService.CreateEmployee(CreatedEmployee);

                // 3. TempData : is a Property of type dictionary Object (Introduced in ASP.NET Framework 3.5)
                // Helps to pass data netweem 2 consecutive requests


                if (result > 0)
                {
                    TempData["Message"] = "Employee is added successfully";
                    TempData["Success"] = true;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Failed to add new employee";
                    TempData["Success"] = false;
                    message = "Failed to add new employee";
                    ModelState.AddModelError("", message);
                    return View(employeeVM);
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
            return View(employeeVM);
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
                    //Gender = (Gender) Enum.Parse(typeof(Gender) , employee.Gender),
                    //EmployeeType = (EmpType) Enum.Parse(typeof(EmpType) , employee.EmployeeType)

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
                    Gender = employeeUpdateVM.Gender,
                    EmployeeType = employeeUpdateVM.EmployeeType


                };

                var result = employeeService.UpdateEmployee(id, employee);

                if (result > 0)
                {
                    TempData["Message"] = "Employee is updated successfully";
                    TempData["Success"] = true;
                    return RedirectToAction(nameof(Index));
                }

                TempData["Message"] = "Failed to update employee";
                TempData["Success"] = false;
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
                {
                    TempData["Message"] = "Employee is deleted successfully";
                    TempData["Success"] = true;
                    return RedirectToAction(nameof(Index));


                }

                TempData["Message"] = "Failed to delete employee";
                TempData["Success"] = false;
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
