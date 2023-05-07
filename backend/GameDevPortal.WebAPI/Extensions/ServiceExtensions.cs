using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Models;
using GameDevPortal.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GameDevPortal.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentityCore<User>(o => { 
            o.Password.RequireDigit = true; 
            o.Password.RequireLowercase = false; 
            o.Password.RequireUppercase = false; 
            o.Password.RequireNonAlphanumeric = false; 
            o.Password.RequiredLength = 10; 
            o.User.RequireUniqueEmail = true; 
        }); 
        
        builder = new IdentityBuilder(builder.UserType, typeof(UserRole), builder.Services); 
        builder.AddEntityFrameworkStores<ProjectContext>().AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration) 
    { 
        var jwtSettings = configuration.GetSection("JwtSettings"); 
        var secretKey = Environment.GetEnvironmentVariable("JwtSignKey"); 
        services.AddAuthentication(opt => { 
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
        }).AddJwtBearer(options => { 
            options.TokenValidationParameters = new TokenValidationParameters { 
                ValidateIssuer = true, 
                ValidateAudience = true, 
                ValidateLifetime = true, 
                ValidateIssuerSigningKey = true, 
                ValidIssuer = jwtSettings.GetSection("validIssuer").Value, 
                ValidAudience = jwtSettings.GetSection("validAudience").Value, 
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)) 
            }; 
        }); 
    }
}