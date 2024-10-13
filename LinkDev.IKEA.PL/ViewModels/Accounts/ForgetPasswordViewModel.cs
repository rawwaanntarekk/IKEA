using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Accounts
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage ="Email is required for recovering your account")]
        [EmailAddress(ErrorMessage ="Please enter a valid email address")]
        public string Email { get; set; } = null!;
    }
}
