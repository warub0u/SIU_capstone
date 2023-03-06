using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul; //install Ocelot.provider.consul nuget

namespace CSAPIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            IConfiguration config = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
            builder.Services.AddOcelot(config).AddConsul();

            // CORS //
            builder.Services.AddCors(options => options.AddPolicy("MyCors", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            var app = builder.Build();

            // ADD USECORS MIDDLEWARE //
            app.UseCors("MyCors");
            
            // add ocelot middleware
            app.UseOcelot();

            


            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}