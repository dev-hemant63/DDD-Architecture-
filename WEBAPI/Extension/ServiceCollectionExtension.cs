using Domain.Interface;
using Domain.Service;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WEBAPI.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContext, Context>();
            services.AddAuthentication(option =>
            {
                option = new Microsoft.AspNetCore.Authentication.AuthenticationOptions
                {
                    DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme,
                    DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme,
                    DefaultScheme = JwtBearerDefaults.AuthenticationScheme
                };
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secretkey"]))
                };
            }).AddCookie("Cookies");
            services.AddAuthorization();
        }
    }
}
