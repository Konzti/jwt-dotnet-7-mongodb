using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using JwtDotNet7.Services;
using JwtDotNet7.Services.Interfaces;
using JwtDotNet7.Settings.Jwt;
using JwtDotNet7.Settings.MongoDB;

using MongoDB.Driver;

namespace JwtDotNet7.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, ConfigurationManager config)
        {
            services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));
            services.Configure<MongoDBSettings>(config.GetSection(MongoDBSettings.SectionName));
            services.AddSingleton<MongoClientBase>(s => new MongoClient(config.GetValue<string>("MongoDBConnectionSettings:ConnectionString")));

            return services;
        }

        public static IServiceCollection AddAppDependencies(this IServiceCollection services)
        {

            services.AddSingleton<IMongoDBSettings>(s => s.GetRequiredService<IOptions<MongoDBSettings>>().Value);
            services.AddSingleton<IJwtSettings>(s => s.GetRequiredService<IOptions<JwtSettings>>().Value);
            services.AddSingleton<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;

        }

        public static IServiceCollection AddJwt(this IServiceCollection services, ConfigurationManager config)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddCookie(x =>
            {
                x.Cookie.Name = "token";
                x.Cookie.HttpOnly = true;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = config.GetValue<string>("JwtSettings:Issuer"),
                    ValidAudience = config.GetValue<string>("JwtSettings:Audience"),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("JwtSettings:JwtSecret")!))
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Cookies["token"];
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

    }
}