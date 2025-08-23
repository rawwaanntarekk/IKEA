using Microsoft.AspNetCore.Identity;


namespace LinkDev.IKEA.DAL.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsAgree { get; set; }
       
    }
}
