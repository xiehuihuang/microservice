using Framework.Core.ConsulExtend.DispatcherExtend;
using Framework.Core.ConsulExtend.DistributedExtend;
using Framework.Core.ConsulExtend.ServerExtend.Register;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.ConsulExtend
{
    /// <summary>
    /// 提供个扩展，完成注册
    /// </summary>
    public static class ConsulExtend
    {
        /// <summary>
        /// 完成注册
        /// </summary>
        /// <param name="services"></param>
        public static void AddConsulRegister(this IServiceCollection services)
        {
            services.AddTransient<IConsulRegister, ConsulRegister>();//完成IOC注册
        }

        /// <summary>
        /// 注册Consul调度策略
        /// </summary>
        /// <param name="services"></param>
        /// <param name="consulDispatcherType"></param>
        public static void AddConsulDispatcher(this IServiceCollection services, ConsulDispatcherType consulDispatcherType)
        {
            switch (consulDispatcherType)
            {
                case ConsulDispatcherType.Average:
                    services.AddTransient<AbstractConsulDispatcher, AverageDispatcher>();
                    break;
                case ConsulDispatcherType.Polling:
                    services.AddTransient<AbstractConsulDispatcher, PollingDispatcher>();
                    break;
                case ConsulDispatcherType.Weight:
                    services.AddTransient<AbstractConsulDispatcher, WeightDispatcher>();
                    break;
                default:
                    break;
            }
        }

        public static void AddConsulDistributed(this IServiceCollection services)
        {
            services.AddTransient<IConsulDistributed, ConsulDistributed>();//完成IOC注册
        }
    }

    public enum ConsulDispatcherType
    {
        Average = 0,
        Polling = 1,
        Weight = 2
    }
}
