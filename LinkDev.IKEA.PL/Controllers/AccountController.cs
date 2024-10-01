using LinkDev.IKEA.DAL.Models.Identity;
using LinkDev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager,
        SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Sign Up

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is { })
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "User Name is already taken");
                return View(model);

            }

            user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                IsAgree = model.isAgree
            };

            // uses Identity stores to create a new user with the specified password.
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return RedirectToAction(nameof(SignIn));

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

                return View(model);

        }







        #endregion
    }
}
