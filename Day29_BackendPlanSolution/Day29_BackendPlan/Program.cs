using Day29_BackendPlan.Context;
using Microsoft.EntityFrameworkCore;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Repository;
using Day29_BackendPlan.Services;
using Day29_BackendPlan.Misc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace Day29_BackendPlan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            #region Contexts
            builder.Services.AddDbContext<InsuranceContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
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


            #region Filters
            builder.Services.AddScoped<CustomExceptionFilter>();
            #endregion

            #region OtherServices
            builder.Services.AddAutoMapper(typeof(ClaimReport));
            builder.Services.AddAutoMapper(typeof(Policy));
            builder.Services.AddAutoMapper(typeof(PolicyItem));
            builder.Services.AddAutoMapper(typeof(PolicyHolder));
            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, ClaimReport>, ClaimReportRepository>();
            builder.Services.AddScoped<IRepository<int,Policy>,PolicyRepository>();
            builder.Services.AddScoped<IRepository<int ,PolicyItem>, PolicyItemRepository>();
            builder.Services.AddScoped<IRepository<int,PolicyHolder>, PolicyHolderRepository>();
            builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IClaimReportService, ClaimReportService>();
            builder.Services.AddScoped<IPolicyService,PolicyService>();
            builder.Services.AddScoped<IPolicyItemService, PolicyItemService>();
            builder.Services.AddScoped<IPolicyHolderService, PolicyHolderService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
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
