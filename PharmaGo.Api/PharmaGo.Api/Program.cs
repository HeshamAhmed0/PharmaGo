
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
namespace PharmaGo.Api
{
    public class Program
    {
        public static  async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region JWT
            // 1. Bind JwtSettings ?? ??? appsettings.json
            var jwtSettingsSection = builder.Configuration.GetSection(nameof(JwtOptions));
            builder.Services.Configure<JwtOptions>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtOptions>()
                ?? throw new InvalidOperationException("JWT settings are not configured properly.");

            // 2. Add Authentication & Authorization
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,


                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audiences,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey)),
                    ClockSkew = TimeSpan.Zero,
                };
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
