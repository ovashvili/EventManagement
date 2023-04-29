using EventManagement.API.SwaggerSettings.SwaggerExtensions;
using EventManagement.Domain.Entities;
using EventManagement.Infrastructure.Extensions;
using EventManagement.Infrastructure.Helpers.JWT;
using EventManagement.Infrastructure.Middlewares;
using EventManagement.Infrastructure.Middlewares.RequestResponseLogger;
using EventManagement.Persistence.Context;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EventDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.InstallInfrastructure(builder.Configuration);

builder.Services.AddDefaultIdentity<User>()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<EventDbContext>();

builder.Services.AddTokenAuthentication(builder.Configuration.GetSection(nameof(JWTConfiguration)).GetSection(nameof(JWTConfiguration.Secret)).Value);
builder.Services.AddScoped<UserManager<User>>();

builder.Services.AddControllers();

builder.Services.AddCustomSwagger();
builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddHealthChecks().AddRedis(builder.Configuration.GetConnectionString("Redis"));

builder.Services.AddHealthChecksUI().AddInMemoryStorage();

builder.Logging.ClearProviders();

builder.Logging.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());

var app = builder.Build();

await app.AutomateMigrationAndSeeding().ConfigureAwait(false);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization("ka-GE", "en-US");

app.UseHttpsRedirection();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/healthcheck", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();

app.Run();
