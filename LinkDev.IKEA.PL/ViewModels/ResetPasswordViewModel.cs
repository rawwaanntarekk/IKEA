namespace LinkDev.IKEA.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
