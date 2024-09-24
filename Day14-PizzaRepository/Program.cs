using Day14_PizzaRepository.Interface;
using Day14_PizzaRepository.Models;
using Day14_PizzaRepository.Repositories;
using Day14_PizzaRepository.Services;

namespace Day14_PizzaRepository
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IPizzaService, PizzaService>();//Service will be used in controller
            builder.Services.AddScoped<IRepository<int, PizzaImages>, PizzaImageRepository>();//Repo will be used in service
            builder.Services.AddScoped<IRepository<int, Pizza>, PizzaRepository>();//Repo will be used in service


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}