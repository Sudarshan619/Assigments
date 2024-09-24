using Day14_Login.Interface;
using Day14_Login.Models;
using Day14_Login.Repositories;
using Day14_Login.Services;

namespace Day14_Login
{
    public class Program
    {
        public static void Main(string[] args)
        {
         
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddScoped<IUser<string,string,User>,UserRepositories>();
            builder.Services.AddScoped<IPizzaService, PizzaService>();//Service will be used in controller
            builder.Services.AddScoped<IRepository<int, PizzaImages>, PizzaImageRepository>();//Repo will be used in service
            builder.Services.AddScoped<IRepository<int, Pizza>, PizzaRepository>();//Repo will be used in servic


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}