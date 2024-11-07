using Day28_EventBooking.Context;
using Day28_EventBooking.Interface;
using Day28_EventBooking.Model;
using Microsoft.EntityFrameworkCore;
using Day28_EventBooking.Repository;
using Day28_EventBooking.Services;


namespace Day28_EventBooking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Contexts
            builder.Services.AddDbContext<EventBookingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Repository
            builder.Services.AddScoped<IRepository<int, Employee>, EmployeeRepository>();
            builder.Services.AddScoped<IRepository<int, Event>, EventRepository>();
            builder.Services.AddScoped<IRepository<int, Booking>, BookingRepository>();
            builder.Services.AddScoped<IRepository<int, EventBooking>, EventBookingRepository>();
            builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<EventService>();
            builder.Services.AddScoped<BookingService>();
            builder.Services.AddScoped<EventBookingService>();
            builder.Services.AddScoped<UserService>();
            #endregion

            #region Other service
            builder.Services.AddAutoMapper(typeof(Employee));
            builder.Services.AddAutoMapper(typeof(Event));
            builder.Services.AddAutoMapper(typeof(EventBooking));
            builder.Services.AddAutoMapper(typeof(Booking));
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
