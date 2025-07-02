using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Office.BLL.Helpers;
using Office.BLL.Services;
using Office.BLL.Services.Interfaces;
using Office.DAL;
using Office.DAL.Repositories;
using Office.DAL.Repositories.Intefaces;
using Office.Web.Extensions;
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
        
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        
        services.AddScoped<IProjectTypeService, ProjectTypeService>();
        services.AddScoped<IProjectTypeRepository, ProjectTypeRepository>();
        
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        
        services.AddScoped<ISubdivisionService, SubdivisionService>();
        services.AddScoped<ISubdivisionRepository, SubdivisionRepository>();
        
        services.AddScoped<IAbsenceReasonService, AbsenceReasonService>();
        services.AddScoped<IAbsenceReasonRepository, AbsenceReasonRepository>();
        services.AddScoped<IApprovalRequestService, ApprovalRequestService>();
        services.AddScoped<IApprovalRequestRepository, ApprovalRequestRepository>();
        services.AddScoped<ILeaveRequestService, LeaveRequestService>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Office API", Version = "v1" });
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Bearer {token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        services.AddSwaggerGenNewtonsoftSupport();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Office API v1");
            });
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        
        app.UseMiddleware<ErrorHandlingMiddleware>(); 
        
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
