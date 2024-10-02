using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=(.*[a-z]){3,})(?=(.*[A-Z]){2,})(?=(.*[0-9]){2,})(?=(.*[!@#$%^&*()\-__+.]){1,}).{8,}$")]
        public string Password { get; set; } = null!;
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not matched")]
        public string ConfirmPassword { get; set; } = null!;
       
        public bool isAgree { get; set; }
    }
}
