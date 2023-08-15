namespace sunstealer.mvc.odata.Swagger;

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class OperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription != null && context.ApiDescription?.HttpMethod == "GET" && context.ApiDescription.RelativePath == "odata/Edm")
        {
            operation.Parameters.Add(new OpenApiParameter()
            {
                Description = "$expand",
                In = ParameterLocation.Query,
                Name = "$expand",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                }
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Description = "$filter",
                In = ParameterLocation.Query,
                Name = "$filter",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                }
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Description = "$orderby",
                In = ParameterLocation.Query,
                Name = "$orderby",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                }
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Description = "$select",
                In = ParameterLocation.Query,
                Name = "$select",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                }
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Description = "$skip",
                In = ParameterLocation.Query,
                Name = "$skip",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Description = "$top",
                In = ParameterLocation.Query,
                Name = "$top",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                }
            });
        }
    }
}
