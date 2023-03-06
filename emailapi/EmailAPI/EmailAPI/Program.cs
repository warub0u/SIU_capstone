using Consul;
using EmailAPI.Models;
using EmailAPI.Services;

namespace EmailAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ADD SERVICES DEPENDENCIES RESOLUTION FOR EMAIL SERVICE, AND ALSO EMAIL CONFIGS
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddSingleton(builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            builder.Services.AddCors(options => options.AddPolicy("MyCorsPolicy", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            // ADDSCOPED FOR MIDDLEWARES FOR CONNECTING TO GATEWAY//
            //this is where your consul is running. })); //this one below app.UseConsul(); //this replaces app.UseOcelot()
            builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new System.Uri(builder.Configuration["ConsulConfig:ConsulAddress"]);
                //this is where your consul is running.
            }));

            var app = builder.Build();

            // UseConsul for API Gateway
            app.UseConsul(builder.Configuration);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("MyCorsPolicy");


            app.MapControllers();

            app.Run();
        }
    }
}