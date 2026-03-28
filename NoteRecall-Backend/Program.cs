using Microsoft.EntityFrameworkCore;
using NoteRecall_Infrastructure.Contexts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Adding Serilog
//adding Serilog configuration
Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Information()
             .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
             .Enrich.FromLogContext()
             .WriteTo.Console()
             .WriteTo.File(new Serilog.Formatting.Json.JsonFormatter(), "logs/log.json", rollingInterval: RollingInterval.Day)
             .WriteTo.Seq("http://localhost:5341") // Example Seq server URL
             .CreateLogger();

builder.Host.UseSerilog(); //instead of the default logger
#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging(); // logs HTTP requests

app.UseAuthorization();

app.MapControllers();

app.Run();
