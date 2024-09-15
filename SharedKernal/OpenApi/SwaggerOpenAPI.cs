using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SharedKernal.OpenApi
{
    public static class SwaggerOpenAPI
    {
        public static void ConfigureAppSwaggerGen(this IServiceCollection services, OpenApiInfo appApiInfo,
            bool userApiKeyAuthorizeFilter = false,
            bool useTokenAuthorizeFilter = false)
        {
            services.AddSwaggerGen(c =>
            {
                ConfigureSwagger(c, appApiInfo, userApiKeyAuthorizeFilter, useTokenAuthorizeFilter);
            });
        }

        private static void ConfigureSwagger(
            SwaggerGenOptions options,
            OpenApiInfo appApiInfo,
            bool userApiKeyAuthorizeFilter = false,
            bool useTokenAuthorizeFilter = false)
        {

            options.SwaggerDoc("v1", appApiInfo);

            if (userApiKeyAuthorizeFilter)
            {
                var securitySchemeApiKey = new OpenApiSecurityScheme
                {
                    Name = SharedConstants.CrossCutting.ApiKeyHeader,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "apiKey",  // Use "apiKey" as the scheme which is standard in OpenAPI for API keys
                    In = ParameterLocation.Header,  // Recommend only using Header due to security concerns with Query
                    Description = "API Key required for access to this endpoint"
                };

                var securityRequirementApiKey = new OpenApiSecurityRequirement
                {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "ApiKey"
                                },
                                Scheme = "apiKey",
                                Name = SharedConstants.CrossCutting.ApiKeyHeader,
                                In = ParameterLocation.Header
                            },
                            new string[] { }
                        }
                };

                options.AddSecurityDefinition("ApiKey", securitySchemeApiKey);
                options.AddSecurityRequirement(securityRequirementApiKey);
            }

            if (useTokenAuthorizeFilter)
            {
                var securitySchemeBearer = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Bearer token required for access to this endpoint"
                };

                var securityRequirementBearer = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "bearer",
                            Name = "Authorization",
                            In = ParameterLocation.Header
                        },
                        new string[] { }
                    }
                };

                options.AddSecurityDefinition("Bearer", securitySchemeBearer);
                options.AddSecurityRequirement(securityRequirementBearer);
            }
        }
    }
}
