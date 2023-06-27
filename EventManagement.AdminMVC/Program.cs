using System.Text;
using EventManagement.Domain.Entities;
using EventManagement.Infrastructure.Extensions;
using EventManagement.Infrastructure.Helpers;
using EventManagement.Infrastructure.Middlewares;
using EventManagement.Infrastructure.Middlewares.JwtTokenvalidator;
using EventManagement.Infrastructure.Middlewares.RequestResponseLogger;
using EventManagement.Persistence.Context;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<EventDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.InstallInfrastructure(builder.Configuration);

builder.Services.AddDefaultIdentity<User>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<EventDbContext>();

builder.Services.AddScoped<UserManager<User>>();

builder.Logging.ClearProviders();

builder.Logging.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "Issuer",
        ValidAudience = "Audience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfiguration:Secret"]))
    };
});
builder.Services.AddHealthChecksUI().AddInMemoryStorage();
builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddHealthChecks().AddRedis(builder.Configuration.GetConnectionString("Redis"));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseMiddleware<JwtTokenValidatorMiddleware>();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminEvent}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapHealthChecks("/healthcheck", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();
app.Run();
