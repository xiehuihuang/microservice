using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace BrandMicro.WebApi.Register
{
    /// <summary>
    /// Swagger配置封装
    /// </summary>
    public static class SwaggerExtend
    {
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(option =>
            {
                option.OperationFilter<SwaggerFileUploadFilter>();

                //添加安全定义
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入token,格式为 Bearer xxxxxxxx（注意中间必须有空格）",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                //添加安全要求
                option.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme{
                Reference =new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id ="Bearer"
                }
            },new string[]{ }
        }
    });
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BrandMicrosevice",
                    Version = "WebApi：V1.0",
                    Description = "Jack NET6.0 WebApi 品牌微服务"
                });
                var file = Path.Combine(AppContext.BaseDirectory, "BrandMicro.WebApi.xml");  // xml文档绝对路径 
                option.IncludeXmlComments(file, true); // true : 显示控制器层注释
                //option.OrderActionsBy(o => o.RelativePath); // 对action的名称进行排序，如果有多个，就可以看见效果了。 
            });
        }
    }
}
