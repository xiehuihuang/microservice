using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace BrandMicro.WebApi.Register
{
    /// <summary>
    /// 跨域策略配置封装
    /// </summary>
    public static class CorsExtend
    {
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="applicationBuilder"></param>
        public static void RegisterCors(this WebApplicationBuilder applicationBuilder, string policyName)
        {
            applicationBuilder.Services.AddCors(options => options.AddPolicy(policyName, cors =>
            {
                //cors.WithOrigins(new string[] { "http://localhost:8080" }); 
                {
                    cors.WithOrigins(applicationBuilder.Configuration["AllowedHosts"]);
                    cors.AllowAnyOrigin();
                    cors.AllowAnyMethod();
                    cors.AllowAnyHeader();
                    //cors.WithMethods("POST"); //配置化
                }
                //cors.WithHeaders("User-Agent");
                //cors.WithExposedHeaders("User-Agent");
                //cors.WithMethods("GetCurrentUserInfo"); 

                //cors.AllowCredentials();
                //cors.DisallowCredentials();

                //cors.AllowAnyOrigin();
                //cors.AllowAnyMethod();
                //cors.AllowAnyHeader();

                //cors.SetPreflightMaxAge(TimeSpan.Zero); 
                //根据条件判断
                //cors.SetIsOriginAllowed(c =>
                //{
                //    return c.Equals("http://localhost:8080");
                //});
                //cors.SetIsOriginAllowedToAllowWildcardSubdomains();

            }));
        }
    }
}
