using HeroesProject_ASP.NET.Data;
using HeroesProject_ASP.NET.Models;
using HeroesProject_ASP.NET.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
    
namespace HeroesProject_ASP.NET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Creating the web application - that wiil manage services configuration and sets-up middlewears
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container //

            // Database connection
            builder.Services.AddDbContext<HeroesContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("HeroesDB")));

            // Identity connection - for authentication
            builder.Services.AddIdentity<TrainerModel, IdentityRole>()
                .AddEntityFrameworkStores<HeroesContext>()
                .AddDefaultTokenProviders();

            // Configure JWT authentication settings
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false; // enanble accessing JWT metadata even from http (not secured)
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"]
,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

            /* Possible way to add password validations
                builder.Services.Configure<IdentityOptions>(opt =>
                {
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireNonAlphanumeric = true;
                    opt.Password.RequiredLength = 8;
                });
            */

            // JSON Serialization Settings - prevent infinite loops
            builder.Services.AddControllers().AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Registering Repositories (Dependency Injection) - defining services scopes
            builder.Services.AddTransient<IAccountRepository, AccountRepository>();
            builder.Services.AddTransient<IHeroesRepository, HeroesRepository>();
            builder.Services.AddTransient<ITrainersRepository, TrainersRepository>();

            // Swagger settings 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // CORS settings - about how can outside sources can access and call to this API web server
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    policy => policy.WithOrigins("http://localhost:4200")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline //


            // Enable swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); // Redirecting all requests to secure connection

            app.UseRouting(); // Enables endpoint routing
            app.UseCors("AllowAngularApp");
            app.UseAuthentication(); // Activates JWT authentication
            app.UseAuthorization();

            app.MapControllers(); // Connect (map) between controller action method to corresponding route and request type

            app.Run();
        }
    }
}
