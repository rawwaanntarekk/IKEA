using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Models.Department;
using LinkDev.IKEA.PL.ViewModels.Departments;
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
        [ValidateAntiForgeryToken]
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
                message = env.IsDevelopment() ? ex.Message : "Department is not created :(";


            }
            ModelState.AddModelError("", message);
            return View(department);
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

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = departmentService.GetDepartmentByID(id.Value);

            if(department is { })
                return View(new DepartmentUpdateViewModel()
                {
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate
                });

            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int id, DepartmentUpdateViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var message = string.Empty;

            try
            {
                var departmentToUpdate = new UpdatedDepartmentDTO()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,
                   
                };

                var result = departmentService.UpdateDepartment(departmentToUpdate);

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
            return View(departmentVM);




        }


        #region Delete Action
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = departmentService.GetDepartmentByID(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = departmentService.deleteDepartment(id);

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
