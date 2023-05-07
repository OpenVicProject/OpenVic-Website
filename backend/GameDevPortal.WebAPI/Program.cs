using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces;
using GameDevPortal.Core.Interfaces.Authentication;
using GameDevPortal.Core.Interfaces.Notifications;
using GameDevPortal.Core.Interfaces.Repositories;
using GameDevPortal.Core.Services.DomainServices;
using GameDevPortal.Infrastructure.Data;
using GameDevPortal.Infrastructure.Files;
using GameDevPortal.Infrastructure.Notifications.Configuration;
using GameDevPortal.Infrastructure.Notifications.Services;
using GameDevPortal.WebAPI.Configuration;
using GameDevPortal.WebAPI.Extensions;
using GameDevPortal.WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() } });
}
);

// Setup DB
builder.Services.AddDbContext<ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectDBConnectionString"), x =>
    {
        x.MigrationsAssembly("GameDevPortal.Infrastructure");
        x.EnableRetryOnFailure();
    }));

// Setup Authentication
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

// Setup DI
builder.Services.AddSingleton<ILogger, Logger<Entity>>();
builder.Services.AddScoped<IRepository, EfCoreRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Domain Services
builder.Services.AddScoped<IProjectDomainService, ProjectDomainService>();
builder.Services.AddScoped<IProgressReportDomainService, ProgressReportDomainService>();
builder.Services.AddScoped<ICategoryDomainService, CategoryDomainService>();
builder.Services.AddScoped<IFaqDomainService, FaqDomainService>();

// Notification Services
builder.Services.AddScoped<ITemplateProvider, LocalFileTemplateProvider>();
builder.Services.AddScoped<INotificationService, EmailNotificationService>();

// Authentication Services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();

// Setup Config
builder.Services.Configure<SendGridConfiguration>(builder.Configuration.GetSection(SendGridConfiguration.SectionName));
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(JwtConfiguration.SectionName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();