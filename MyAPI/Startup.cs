using MyAPI.Services;

namespace MyAPI;

public class Startup
{
    public IWebHostEnvironment HostEnvironment { get; }

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        HostEnvironment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.ModelValidatorProviders.Clear();
            options.AllowEmptyInputInBodyModelBinding = true;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<IMyService, MyService>();
        services.AddSingleton<ICalculator, Calculator>();
        services.AddSingleton<IStorage, Storage>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IServiceProvider services)
    {
        if (HostEnvironment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.EnableTryItOutByDefault();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Deployment API v1");
            });
        }

        if (!this.HostEnvironment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseHttpLogging();


        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }
}