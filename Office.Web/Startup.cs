using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Office.BLL.Helpers;
using Office.BLL.Services;
using Office.BLL.Services.Interfaces;
using Office.DAL;
using Office.DAL.Repositories;
using Office.DAL.Repositories.Intefaces;
using Office.Web.Helpers;

namespace Office.Web;
public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddJwtAuth();
        services.AddScoped<TokenHelper>();
        
        services.AddControllers().AddNewtonsoftJson(opt => 
            opt.SerializerSettings.Converters.Add(new StringEnumConverter()));
        var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING") ?? Configuration.GetConnectionString("ConnectionString");

        services.AddDbContext<OfficeDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddAutoMapper(typeof(Startup));

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();

        services.AddScoped<IManagerService, ManagerService>();

        services.AddScoped<IAuthService, AuthService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        app.UseRouting();
        app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
    }
}