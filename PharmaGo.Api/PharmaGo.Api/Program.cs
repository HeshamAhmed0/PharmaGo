
using System.Reflection.Metadata;
using Domain.Contracts;
using Domain.Moduls;
using Domain.Moduls.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Presistance;
using Presistance.Reposatories;
using Services;
using Services_Abstraction;
using Shared.MedulesDto.AuthModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Encodings;
using System.Text;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
namespace PharmaGo.Api
{
    public class Program
    {
        public static  async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region JWT
            // 1. Bind JwtSettings ?? ??? appsettings.json
            var jwtSettingsSection = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
           
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,


                    ValidIssuer = jwtSettingsSection.Issuer,
                    ValidAudience = jwtSettingsSection.Audiences,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettingsSection.SecurityKey)),
                    RoleClaimType = ClaimTypes.Role
                };
            });
            #endregion

            #region Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // ?? ????? Security Scheme ? JWT (Http bearer)
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",     // lowercase ???
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token only (no 'Bearer ' prefix required)."
                });

                // ?? ??? ?? ??? Endpoints ???? Security Requirement
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });

            #endregion

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(AssemblyForAutoMapper).Assembly);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IUnitofwork, UnitOfWork>();
            builder.Services.AddScoped<IProductService,ProductServices>();
            builder.Services.AddScoped<IServiceManager,ServiceManager>();
            builder.Services.Configure<JwtOptions>(
                builder.Configuration.GetSection("JWTOptions")
                );
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbContext>() //
            .AddDefaultTokenProviders();
            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });



            var app = builder.Build();

            #region Scop For DbInitializer

            using (var Scope = app.Services.CreateScope())
            {
                var DbInitializer = Scope.ServiceProvider.GetService<IDbInitializer>();
               await DbInitializer.InitializeAsync();
               await DbInitializer.InitializeIdentityAsync();
            }
              
             

            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();


            app.Run();
        }
    }
}
