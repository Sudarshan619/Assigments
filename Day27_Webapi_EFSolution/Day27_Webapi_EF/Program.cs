using Day27_Webapi_EF.Context;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Misc;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Repositories;
using Day27_Webapi_EF.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Day27_Webapi_EF
{
    public class Program
    {

        
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Filters
            builder.Services.AddScoped<CustomExceptionFilter>();
            #endregion

            #region Contexts
            builder.Services.AddDbContext<ShoppingContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });
                #endregion

                #region OtherServices
                builder.Services.AddAutoMapper(typeof(Customer));
                builder.Services.AddAutoMapper(typeof(Product));
                #endregion

                #region Repositories
                 builder.Services.AddScoped<IRepository<int, Customer>, CustomerRepository>();
                 builder.Services.AddScoped<IRepository<int, Product>, ProductRepository>();
                builder.Services.AddScoped<IRepository<int, Order>, OrderRepository>();
                builder.Services.AddScoped<IRepository<int, OrderDetail>, OrderDetailRepository>();
                builder.Services.AddScoped<IRepository<int, Cart>, CartRepository>();
                builder.Services.AddScoped<IRepository<int, CartItem>, CartItemRepository>();
                builder.Services.AddScoped<IRepository<int, ImageItem>, ImageItemRepository>();
            builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<ICustomerBasicService, CustomerBasicService>();
                builder.Services.AddScoped<IProductService,ProductService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICartItemService, CartItemService>();
            builder.Services.AddScoped<ImageItemService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            #endregion

            #region Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                    };
                });
            #endregion

            builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseAuthentication();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
        

    }
}
