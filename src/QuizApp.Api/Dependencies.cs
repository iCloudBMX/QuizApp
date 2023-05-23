using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using QuizApp.Domain.Enums;

namespace QuizApp.Api;

public static class Dependencies
{
    public static IServiceCollection AddApis(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("UserPolicy", options =>
            {
                options.RequireRole(
                    UserRole.Admin.ToString(),
                    UserRole.Tester.ToString());
            });
        });
        services.AddHttpContextAccessor();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters=new TokenValidationParameters
            {
                ValidateIssuer=true,
                ValidateAudience=true,
                ValidateLifetime=true,
                ValidateIssuerSigningKey=true,
                ValidIssuer=configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey=new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                ClockSkew=TimeSpan.Zero
            };
        });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title="QuizApp.Api", Version="v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name="Authorization",
                Description=
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In=ParameterLocation.Header,
                Type=SecuritySchemeType.ApiKey
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
                    new string[] { }
                }
            });
        });
        return services;
    }
}