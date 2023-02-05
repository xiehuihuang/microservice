using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Framework.WebCore.JWTExtend;

namespace BrandMicro.WebApi.Register
{
    /// <summary>
    /// JWT授权封装
    /// </summary>
    public static class AuthorizationExtend
    {
        /// <summary>
        /// 支持授权的扩展方法
        /// </summary>
        /// <param name="builder"></param>
        public static void AuthorizationExt(this WebApplicationBuilder builder)
        {
            JWTTokenOptions tokenOptions = new JWTTokenOptions();
            builder.Configuration.Bind("JWTTokenOptions", tokenOptions);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
            .AddJwtBearer(options =>  //这里是配置的鉴权的逻辑
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //JWT有一些默认的属性，就是给鉴权时就可以筛选了
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = tokenOptions.Audience,//
                    ValidIssuer = tokenOptions.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                    AudienceValidator = (m, n, z) =>  //可以添加一些自定义的动作
                    {
                        return true;
                    },
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                    {
                        return true;
                    }
                };
            });

        }
    }
}
