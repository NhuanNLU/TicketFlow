using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TicketFlow.Service.JwtService;

namespace TicketFlow.API.Extensions;

public static class JwtExtension
{
       public const string CustomerPolicy = "CustomerPolicy";

       public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
       {
              services.AddAuthentication(options =>
              {
                     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
              }).AddJwtBearer(o =>
              {
                     JwtOption jwtOption = new JwtOption();
                     configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);
                     var key = Encoding.UTF8.GetBytes(jwtOption.SecretKey);
                     o.SaveToken = true; // Lưu token vào AutheticationProperties
    
                     o.TokenValidationParameters = new TokenValidationParameters
                     {
                            ValidateIssuer =  true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtOption.Issuer,
                            ValidAudience = jwtOption.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ClockSkew = TimeSpan.Zero
                     };
                     o.Events = new JwtBearerEvents()
                     {
                            OnAuthenticationFailed = context =>
                            {
                                   if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                   {
                                          context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                                   }

                                   return Task.CompletedTask;
                            }
                     };
              });
              services.AddAuthorization();
       }
}