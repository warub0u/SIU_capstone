using Consul;
using OneMapAPI.Repository;
using OneMapAPI.Services;

namespace OneMapAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IOneMapRepository, OneMapRepository>();
            builder.Services.AddScoped<IOneMapService, OneMapService>();
            builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new System.Uri(builder.Configuration["ConsulConfig:ConsulAddress"]);
                //this is where your consul is running.
            }));
            builder.Services.AddControllers();

            //CORS
            builder.Services.AddCors(options => options.AddPolicy("OurCors", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

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
            app.UseCors("OurCors");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}