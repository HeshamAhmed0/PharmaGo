
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Reposatories;

namespace PharmaGo.Api
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           
            
            #region Infrastructure
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();

            #region StoreDbContext Options

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            #endregion

            
            #endregion



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
