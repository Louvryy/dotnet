using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Louvryy.Core.Tests.MinimalApi.Configurations;

/// <summary>
/// SwaggerConfiguration
/// </summary>
public static class SwaggerConfiguration
{
    /// <summary>
    /// ConfigureSwagger
    /// </summary>
    /// <param name="services"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, string name)
    {
        services.AddSwaggerGen(cfg => {
            var xmlFilename = $"{name}.xml";

            cfg.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            cfg.SchemaFilter<EnumSchemaFilter>();

            cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = @"JWT Authorization header using the Bearer scheme.
                                Enter 'Bearer' [space] and then your token in the text input below.
                                Example: 'Bearer 12345abcdef'",
            });
            cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new string[] {}
                }
            });

            cfg.UseAllOfForInheritance();
        });

        return services;
    }
}

/// <summary>
/// EnumSchemaFilter
/// </summary>
public class EnumSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// Apply
    /// </summary>
    /// <param name="model"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            model.Enum.Clear();
            Enum.GetNames(context.Type)
                .ToList()
                .ForEach(name => model.Enum.Add(new OpenApiString($"{Convert.ToInt64(Enum.Parse(context.Type, name))} = {name}")));
        }
    }
}