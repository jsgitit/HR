using HR.Data;
using Microsoft.EntityFrameworkCore;

namespace HR.App;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContextPool<HRDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("HRDBContainer"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    // Enable retry on failure for transient errors
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,  // The maximum number of retries
                        maxRetryDelay: TimeSpan.FromSeconds(5),  // Delay between retries
                        errorNumbersToAdd: null);  // You can specify additional SQL error numbers to retry on
                });
        });


        // InMemory Database implementation for DEV
        // services.AddSingleton<IEmployeeData, InMemoryEmployeeData>();

        // A Scoped SQL Server Database Implementation, per http request, for Production
        services.AddScoped<IEmployeeData, SqlEmployeeData>();

        services.AddRazorPages();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
    }
}