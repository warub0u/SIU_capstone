using Consul;
using CS_FavouritesAPI.Middlewares;
using CS_FavouritesAPI.Models;
using CS_FavouritesAPI.Repository;
using CS_FavouritesAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CS_FavouritesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var con = Environment.GetEnvironmentVariable("SQL_DB");
            if (con == null)
            {
                builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
            }
            else
            {
                builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(con));
            }
            builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new System.Uri(builder.Configuration["ConsulConfig:ConsulAddress"]);
                //this is where your consul is running.
            }));

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
            builder.Services.AddScoped<IBusBookmarkRepository, BusBookmarkRepository>();
            builder.Services.AddScoped<IBusBookmarkService, BusBookmarkService>();
            builder.Services.AddScoped<IFavouritesRepository, FavouritesRepository>();
            builder.Services.AddScoped<IFavouritesService, FavouritesService>();
            builder.Services.AddCors(options => options.AddPolicy("WeCors", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseConsul(builder.Configuration);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("WeCors");
            app.UseHandleExceptionMiddleware();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}