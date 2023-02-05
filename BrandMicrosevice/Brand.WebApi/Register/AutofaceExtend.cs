using Autofac;
using Autofac.Extensions.DependencyInjection;
using BrandMicro.Model.Models;
using Framework.WebCore.JWTExtend;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BrandMicro.WebApi.Register
{
    /// <summary>
    /// 替换Autofac
    /// 注册抽象和服务
    /// </summary>
    public static class AutofaceExtend
    {
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="applicationBuilder"></param>
        public static void RegisterAutofac(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());//通过工厂替换，把Autofac整合进来
            applicationBuilder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
             {
                 #region 注册每个控制器和抽象之间的关系
                 {
                     var controllerBaseType = typeof(ControllerBase);
                     containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
                         .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType);
                 }
                 #endregion

                 #region 通过接口和实现类所在程序集注册 
                 {
                     Assembly interfaceAssembly = Assembly.Load("BrandMicro.Interface");
                     Assembly serviceAssembly = Assembly.Load("BrandMicro.Service");
                     containerBuilder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly).AsImplementedInterfaces();
                 }
                 #endregion

                 #region 注册生成Token
                 {
                     containerBuilder.RegisterType<JWTHSService>().As<IJWTService>();
                 }
                 #endregion
             });
        }
    }
}
