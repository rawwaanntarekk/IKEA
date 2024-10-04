using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Models.Departments;
using LinkDev.IKEA.DAL.Models.Identity;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositotries.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositotries.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using LinkDev.IKEA.PL.Helpers;
using LinkDev.IKEA.PL.Mapping;
using LinkDev.IKEA.PL.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LinkDev.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Register Services to the dependency injection container.
            // Add services to the container. 
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                       .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            builder.Services.AddTransient<IMailSettings, MailSettings>();


            // Adds the default identity system configuration for the specified User and Role types.
            // Register main 3 services : {userManager, signInManager, roleManager}
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);


            // Add Identity stores to the dependency injection container.
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            // Configure default application scheme
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Home/Error";
                // Security token lifetime
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.LogoutPath = "/Account/SignOut";
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Identity.Application";
                options.DefaultChallengeScheme = "Identity.Application";
            });

            builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));
            #endregion

            var app = builder.Build();

            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // check the path follows which controller and action in the application
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); 
            #endregion

            app.Run();
        }
    }
}
