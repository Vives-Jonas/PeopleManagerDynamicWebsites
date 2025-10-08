using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PeopleManager.Api.Services;
using PeopleManager.Api.Settings;

namespace PeopleManager.Api.Installers
{
    public static class AuthenticationInstaller
    {
        public static WebApplicationBuilder InstallAuthentication(this WebApplicationBuilder builder)
        {

            //configuratie om token te valideren

            var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

            if (jwtSettings is null || string.IsNullOrWhiteSpace(jwtSettings.Secret))
            {
                throw new InvalidOperationException("JwtSettings configuration is missing or incomplete");
            }

            builder.Services.AddSingleton(jwtSettings);

            var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.Secret);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });


            //Generates the JWT tokens
            builder.Services.AddScoped<AuthenticationManager>();

            return builder;
        }
    }
}
