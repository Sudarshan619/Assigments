using Day15_PatientLogin.Service;
using Day15_PatientLogin.Interface;
using Day15_PatientLogin.Repositories;
using Day15_PatientLogin.Models;


namespace Day15_PatientLogin
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
            builder.Services.AddScoped<ILogin<string, string, Patient>, LoginRepository>();
            builder.Services.AddScoped<AppointmentRepository>();
            builder.Services.AddScoped<AppointmentService>();
            builder.Services.AddScoped<PatientRepository>();


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