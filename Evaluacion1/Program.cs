using Evaluacion1.Data;
using Evaluacion1.Services;
using Evaluacion1.Services.EventLoggers;
using Evaluacion1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar IApplicationDbContext (Principio de Inversión de Dependencias)
builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar servicios (Dependency Injection)
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();

// Registrar servicio de validación (Principio de Responsabilidad Única)
builder.Services.AddScoped<IValidationService, ValidationService>();

// Registrar loggers de eventos
builder.Services.AddSingleton<ConsoleEventLogger>();
builder.Services.AddSingleton<FileEventLogger>();
builder.Services.AddScoped<DbEventLogger>();
builder.Services.AddScoped<IEventLogger, CompositeEventLogger>(sp => {
    var consoleLogger = sp.GetRequiredService<ConsoleEventLogger>();
    var fileLogger = sp.GetRequiredService<FileEventLogger>();
    var dbLogger = sp.GetRequiredService<DbEventLogger>();
    return new CompositeEventLogger([consoleLogger, fileLogger, dbLogger]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();