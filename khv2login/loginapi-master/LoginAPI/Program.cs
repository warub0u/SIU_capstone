using Consul;
using LoginAPI.Models;
using LoginAPI.Repo;
using LoginAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace LoginAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            // ADDSCOPED FOR SERVICES DATACONTEXT AND RESOLVE DEPENDENCIES //
            builder.Services.AddScoped<DataContext>();
            builder.Services.AddScoped<IGenerator, Generator>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();

            // ADDSCOPED FOR MIDDLEWARES FOR CONNECTING TO GATEWAY//
            //this is where your consul is running. })); //this one below app.UseConsul(); //this replaces app.UseOcelot()
            builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new System.Uri(builder.Configuration["ConsulConfig:ConsulAddress"]);
                //this is where your consul is running.
            }));


            // ADDSCOPED FOR TESTING
            //builder.Services.AddScoped<DataContext>(provider =>
            //{
            //    var connectionString = "mongodb://localhost:27017";
            //    var databaseName = "testdb";
            //    return new DataContext(connectionString, databaseName);
            //});

            // ADD HTTP CLIENT
            builder.Services.AddHttpClient();

            // ADD CONFIGURATION
            IConfiguration configuration = builder.Configuration;
            builder.Services.AddSingleton(configuration);


            // CORS //
            builder.Services.AddCors(options => options.AddPolicy("MyCors", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            // JWT AUTHENTICATION SERVICES //
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_my_secret_key_for_user"));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,

                ValidateIssuer = true,
                ValidIssuer = "authapi",

                ValidateAudience = true,
                ValidAudience = "userapi"           
            });

            // TO AVOID MultiPartBodyLength ERROR FOR UPLOADING OF PICTURE, ADD THE FOLLOWING SERVICES //
            builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();            

            // build the app including the addtional services above
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
            // ADD USECORS MIDDLEWARE //
            app.UseCors("MyCors");

            // ADD MIDDLEWARE TO MAKE STATIC FILES INSIDE ASSETS FOLDER SERVABLE
            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Assets")),
            //    RequestPath = new PathString("/Assets")
            //});

            //ADDED UseAuthentication() //
            app.UseAuthentication();
            
            app.UseAuthorization();


            app.MapControllers();

          

            app.Run();
        }
    }
}