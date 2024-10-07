using LinkDev.IKEA.DAL.Models.Identity;
using LinkDev.IKEA.DAL.Models.Mails;
using LinkDev.IKEA.PL.Helpers;
using LinkDev.IKEA.PL.ViewModels;
using LinkDev.IKEA.PL.ViewModels.Accounts;
using LinkDev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{

	public class AccountController(UserManager<ApplicationUser> _userManager,
        SignInManager<ApplicationUser> _signInManager,
        IMailSettings settings) : Controller
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

        #region Sign In

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is { })
            {
                var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                if (flag)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                    if (result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "User is not allowed to sign in");

                    if (result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "User is locked out");



                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");




                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Email or Password");
            return View(model);


        } 
        #endregion

        public async new Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is { })
            {
                var Token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var url = Url.Action(nameof(ResetPassword), "Account", new
                {
                    model.Email,
                    Token
                }, Request.Scheme);

                var mail = new Email
                {
                    To = model.Email,
                    Subject = "Reset Password",
                    Body = $"Please reset your password by clicking <a href='{url}'>here</a>"
                };

                settings.SendEmail(mail);
            }
            return RedirectToAction(nameof(SignIn));

        }
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is { })
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                    return RedirectToAction(nameof(SignIn));
            }

            return View(model);

        } 
        #endregion



    }
}
