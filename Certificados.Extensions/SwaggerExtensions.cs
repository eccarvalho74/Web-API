using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

public static class SwaggerExtensions
{
    public static void SwaggerGenExtension(this IServiceCollection builder, IConfiguration configuration)
    {


        var swaggerBaseUrl = configuration["SwaggerBaseUrl"];

        builder.AddSwaggerGen(option =>
        {
            option.SwaggerDoc($"v1", new OpenApiInfo
            {
                Version = $"v1",
                Title = configuration["SwaggerApiName"],
                Description = configuration["SwaggerApiDescription"],

            });

            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Insira o Token JWT com o campo Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            option.AddSecurityRequirement(new OpenApiSecurityRequirement {
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


            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            try
            {
                option.IncludeXmlComments(xmlPath);
            }
            catch        {            }
           

            option.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

        });
    }
}