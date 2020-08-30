using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace libloaderapi.Domain.Extensions.Configuration
{
    public static class SwaggerConfigurationEx
    {
        public static IServiceCollection AddCustomSwaggerConfig (this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<EnumSchemaFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static IApplicationBuilder UseCustomSwaggerConfig(this IApplicationBuilder app, string apiName)
        {
            return app
                .UseSwagger()
                .UseSwaggerUI(c 
                    => c.SwaggerEndpoint("/swagger/v1/swagger.json", apiName));
        }
    }
}
