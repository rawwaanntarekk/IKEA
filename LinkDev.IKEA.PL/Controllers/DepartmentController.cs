using AutoMapper;
using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    // DepartmentController is a controller [Inheritance]
    // DepartmentController has a IDepartmentService [Composition]
    [Authorize]
    public class DepartmentController(IDepartmentService departmentService,
        ILogger<DepartmentController> logger,
        IWebHostEnvironment env,
        IMapper _mapper) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var departments = await departmentService.GetDepartmentsAsync(search);
            return View(departments);
        }

        public async Task<IActionResult> Search(string search)
        {
            var departments = await departmentService.GetDepartmentsAsync(search);
            return PartialView("Partials/DepartmentListPartial" , departments);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
           if(!ModelState.IsValid)
                return View(departmentVM);

           var message = string.Empty;


            try
            {
                //var createdDepartment = new CreatedDepartmentDTO()
                //{
                  
                //    Code = departmentVM.Code,
                //    Name = departmentVM.Name,
                //    Description = departmentVM.Description,
                //    CreationDate = departmentVM.CreationDate,

                //};

                var createdDepartment = _mapper.Map<DepartmentViewModel, CreatedDepartmentDTO>(departmentVM);
                var result = await  departmentService.CreateDepartmentAsync(createdDepartment);
                if (result > 0)
                {
                    message = "Department is created successfully";
                    TempData["Message"] = message;
                    TempData["Success"] = true;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Failed to create department";
                    TempData["Success"] = false;
                    message = "Failed to create department";
                    TempData["Message"] = message;
                    TempData["Success"] = false;
                    ModelState.AddModelError("", message);
                    return View(departmentVM);
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
            return View(departmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await departmentService.GetDepartmentByIDAsync(id.Value);
            if (department is { })
                return View(department);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await  departmentService.GetDepartmentByIDAsync(id.Value);

            if(department is { })
                return View(new DepartmentViewModel()
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
        public async Task<IActionResult> Update([FromRoute] int id, DepartmentViewModel departmentVM)
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
                //var departmentToUpdate = _mapper.Map<DepartmentViewModel, UpdatedDepartmentDTO>(departmentVM);


                var result = await departmentService.UpdateDepartmentAsync(departmentToUpdate);

                if (result > 0)
                {

                    TempData["Message"] = "Department is updated successfully";
                    TempData["Success"] = true;
                    return RedirectToAction(nameof(Index));
                }

                TempData["Message"] = "Failed to update department";
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
            return View(departmentVM);




        }


        #region Delete Action
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = departmentService.GetDepartmentByIDAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = await departmentService.deleteDepartmentAsync(id);

                if (deleted)
                {
                    message = "Department is deleted successfully";
                    TempData["Message"] = message;
                    TempData["Success"] = true;
                    return RedirectToAction(nameof(Index));
                }

                message = "Failed to delete department";
                TempData["Message"] = message;
                TempData["Success"] = false;
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
