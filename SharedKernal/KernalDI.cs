using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharedKernal.Correlation;
using SharedKernal.Exceptions.CrossCutting;
using SharedKernal.MediatR;
using SharedKernal.OpenApi;
using SharedKernal.Policies;
using SharedKernal.Settings;
using SharedKernal.Syncronization.Cancelation;
using SharedKernal.UnitOfWork;
using SharedKernal.Validation;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedKernal
{
    public static class KernalDI
    {
        public static IMvcBuilder AddAppControllers(this IServiceCollection services)
        {
            return services
                  .AddControllers(options =>
                  {
                      options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                  })
                  .AddJsonOptions(options =>
                  {
                      options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                  })
                  .ConfigureApiBehaviorOptions(options =>
                  {
                      options.InvalidModelStateResponseFactory = context =>
                      {
                          var errors = context.ModelState.ToFluentValidationFailures();

                          throw new ValidationException(errors);
                      };
                  });
        }
        public static void AddAppData(this IServiceCollection services)
        {
            services.AddScoped<IUOF, UOF>();
        }
        private static void AddOpenApi(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.ConfigureAppSwaggerGen(new OpenApiInfo
            {
                Title = "FitToFit APis v1",
                Description = "FitToFit Apis v1"
            }, useTokenAuthorizeFilter: true);
        }
        private static void AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp => configuration.GetSection($"{nameof(AuthSetting)}s").Get<AuthSetting>());
        }
        private static void AddCROS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(SharedConstants.CrossCutting.CROSPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

        }

        public static void AddKernalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppSettings(configuration);

            services.AddOpenApi();

            services.AddTransient(sp => TypeAdapterConfig.GlobalSettings);
            services.AddHttpContextAccessor();
            services.AddScoped<ICancelationTokenFactory, CancelationTokenFactory>();

            var authSetting = services.BuildServiceProvider().GetRequiredService<AuthSetting>();

            services.AddAppAuth(authSetting);

            services.AddExceptionHandler<AppExceptionHandler>();
            services.AddTransient<AppCorrelationIdMiddleware>();

            services.AddCROS();

            services.AddAppValidators();

            services.AddAppMediaR();

            services.AddAppData();
        }
        private static void AddAppAuth(this IServiceCollection services, AuthSetting authSetting)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var keyBytes = new byte[64];
                Encoding.UTF8.GetBytes(authSetting.Secret).CopyTo(keyBytes, 0);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authSetting.Issuer,
                    ValidAudience = authSetting.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                };
            });

            services.AddAuthorization(options => options.AddSystemPolicies());
        }
    }
}
