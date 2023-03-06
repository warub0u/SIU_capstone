using Consul;

public static class ServiceRegistryExtension
{
    // Creating middleware for registering service on Consul
    // Name of the middle ware is UseConsul
    public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configurationSetting)
    //"this" keyword is used for creating extension methods; aka trying to add new features within the existing class. the first parameter muststart with the "this" keyword. the servicereg class is not mentioned anywhere but bc it is extending IApplicationBuilder, you're able to call UseConsul() in Program.cs
    {
        //retrieving Consul client
        var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
        //setting up logger
        var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtension");
        var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        // This is registration configuration that I am reading from appsettings.json file
        var registration = new AgentServiceRegistration()
        {
            ID = configurationSetting["ConsulConfig:ServiceName"],
            Name = configurationSetting["ConsulConfig:ServiceName"],
            Address = configurationSetting["ConsulConfig:ServiceHost"],
            Port = int.Parse(configurationSetting["ConsulConfig:ServicePort"])
        };

        logger.LogInformation("Registering with consul");
        //Here I am registering the service with service ID
        consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
        consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

        lifetime.ApplicationStopped.Register(() =>
        {
            logger.LogInformation("Unregistering service from consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(!true);
        });
        return app;
    }


}