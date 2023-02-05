using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BrandMicro.WebApi.Register
{
    /// <summary>
    /// Swagger文件上传过滤配置封装
    /// </summary>
    public class SwaggerFileUploadFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            const string FileUploadContentType = "multipart/form-data";
            if (operation.RequestBody == null ||
                !operation.RequestBody.Content.Any(x =>
                x.Key.Equals(FileUploadContentType, StringComparison.InvariantCultureIgnoreCase)))
            {
                return;
            }

            if (context.ApiDescription.ParameterDescriptions[0].Type == typeof(HttpRequest))
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Description = "My IO",
                    Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        FileUploadContentType, new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Required = new HashSet<string>{ "file" },
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    {
                                        "file", new OpenApiSchema()
                                        {
                                            Type = "string",
                                            Format = "binary"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                };
            }
        }
    }
}
