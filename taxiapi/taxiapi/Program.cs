using Consul;
using taxiapi;
using taxiapi.Repository;
using taxiapi.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //add httpclient
        builder.Services.AddHttpClient();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options => options.AddPolicy("Cors", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

        builder.Services.AddScoped<IOneMapRepository, OneMapRepository>();
        builder.Services.AddScoped<IFareService, FareService>();
        builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {
            consulConfig.Address = new System.Uri(builder.Configuration["ConsulConfig:ConsulAddress"]);
            //this is where your consul is running.
        }));

        var app = builder.Build();
        app.UseConsul(builder.Configuration);
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("Cors");
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}