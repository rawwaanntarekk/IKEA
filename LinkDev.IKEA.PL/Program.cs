using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositotries.Departments;
using Microsoft.EntityFrameworkCore;

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

            //builder.Services.AddScoped<ApplicationDbContext>();
            //IServiceCollection serviceCollection = builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>((ServiceProvider) =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            //    var options = optionsBuilder.Options;

            //    return options;
            //});

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                
            });

            builder.Services.AddScoped<IDepartmentRepository, IDepartmentRepository>();


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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); 
            #endregion

            app.Run();
        }
    }
}
