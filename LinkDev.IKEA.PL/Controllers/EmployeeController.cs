using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController(IEmployeeService employeeService,
        ILogger<EmployeeController> logger,
        IWebHostEnvironment env) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string Search)
        {
            // ViewData is a Dictionary Type Property (Introduced in ASP.NET Framework 3.0)
            // Helps to pass data from Controller [Action] to View
            ViewData["Message"] = "Hello ViweData";

            // ViewBag is a Dynamic Property (Introduced in ASP.NET Framework 4.0)
            // Helps to pass data from Controller [Action] to View

            ViewBag.Message = "Hello ViewBag";


            var employees = await employeeService.GetEmployeesAsync(Search);
            return View(employees);
        }
        public async Task<IActionResult> Search(string Search)
        {
            var employees = await employeeService.GetEmployeesAsync(Search);
            return PartialView("Partials/EmployeeListPartial", employees);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM )
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
                    EmployeeType = employeeVM.EmployeeType,
                    DepartmentId = employeeVM.DepartmentId
                };
                var result = await employeeService.CreateEmployeeAsync(CreatedEmployee);

                // 3. TempData : is a Property of type dictionary Object (Introduced in ASP.NET Framework 3.5)
                // Helps to pass data netweem 2 consecutive requests


                if (result > 0)
                {
                    message = "Employee is added successfully";
                    TempData["Message"] = message;
                    TempData["Success"] = true;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Failed to add new employee";
                    TempData["Success"] = false;
                    message = "Failed to add new employee";
                    TempData["Message"] = message;
                    TempData["Success"] = false;
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
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = await employeeService.GetEmployeeAsync(id.Value);
            if (employee is { })
                return View(employee);
            return NotFound();
        } 
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int? id )
        {
            if (id is null)
                return BadRequest();



            var employee = await employeeService.GetEmployeeAsync(id.Value);

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
        public async Task<IActionResult> Update([FromRoute] int id, EmployeeViewModel employeeUpdateVM)
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
                    EmployeeType = employeeUpdateVM.EmployeeType,


                };

                var result = await employeeService.UpdateEmployeeAsync(id, employee);

                if (result > 0)
                {
                    message = "Department is updated successfully";
                    TempData["Message"] = message;
                    TempData["Success"] = true;
                    return RedirectToAction("Index");
                }

                TempData["Message"] = "Failed to update employee";
                TempData["Success"] = false;
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = await employeeService.GetEmployeeAsync(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = await employeeService.DeleteEmployeeAsync(id);

                if (deleted)
                {
                    message = "Employee is deleted successfully";
                    TempData["Message"] = message;
                    TempData["Success"] = true;
                    return RedirectToAction(nameof(Index));
                }




                TempData["Message"] = "Failed to delete employee";
                TempData["Success"] = false;
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
