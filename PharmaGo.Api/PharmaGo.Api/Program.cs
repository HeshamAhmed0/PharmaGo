
using System.Reflection.Metadata;
using Domain.Contracts;
using Domain.Moduls;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Reposatories;
using Services;
using Services_Abstraction;

namespace PharmaGo.Api
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                   .AddEntityFrameworkStores<StoreIdentityDbContext>()
                   .AddDefaultTokenProviders();
            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });


            var app = builder.Build();

            #region Scop For DbInitializer
              
            var Scope =app.Services.CreateScope();
            var DbInitializer =Scope.ServiceProvider.GetService<IDbInitializer>();
             DbInitializer.InitializeAsync();

            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
