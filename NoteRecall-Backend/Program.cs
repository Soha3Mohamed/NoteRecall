using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoteRecall_Application;
using NoteRecall_Application.Mapping;
using NoteRecall_Core.Common;
using NoteRecall_Infrastructure.Contexts;
using NoteRecall_Infrastructure.Extensions;
using NoteRecall_Infrastructure.Services.QuestionsGenration;
using Serilog;
using System.Text;
//using Microsoft.OpenApi.Models;


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
//builder.Services.AddOpenApi();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddScoped<SentenceSplitter>();
builder.Services.AddScoped<ImportantSentencePicker>();
builder.Services.AddScoped<IQuestionGenerator, FakeQuestionGenerator>();
builder.Services.AddScoped<SpacedRepetitionService>();

#region add authentication schema 
//add authentication schema 
builder.Services
                .AddAuthentication(op => op.DefaultAuthenticateScheme = "MySchema")
                .AddJwtBearer("MySchema", option =>
                {

                    string _jwtKey = "secretKeyforjwtauthenticationforPayWise";
                    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtKey));

                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
#endregion

#region adding authentication to swagger 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Note-Recall Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter: Bearer <your JWT token>"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
        });
});

#endregion

//builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<UserProfile>(); // optional, can scan assemblies instead
}, typeof(UserProfile).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseSerilogRequestLogging(); // logs HTTP requests

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//// expose endpoint
//app.MapHealthChecks("/health");

app.Run();