using Microsoft.AspNetCore.Identity;

namespace LinkDev.IKEA.PL.Helpers
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddExternalAuth (this IServiceCollection services , IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            }).AddGoogle(options =>
            {
                configuration.GetSection("Authentication:Google").Bind(options);

                // Save the login information in the external cookie
                // Identity then reads the information from this external cookie
                // If the user is found in the database, it will sign in automatically => Add the application cookie scheme
                // Else it will create a new user in the database with the application cookie scheme

                options.SignInScheme = IdentityConstants.ExternalScheme;
            });


            return services;
        }
    }
}
