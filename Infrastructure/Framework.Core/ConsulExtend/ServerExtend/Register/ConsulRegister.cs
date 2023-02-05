using Consul;
using Framework.Core.ConsulExtend;
using Framework.Core.ConsulExtend.ServerExtend;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.ConsulExtend.ServerExtend.Register
{
    public class ConsulRegister : IConsulRegister
    {
        private readonly ConsulRegisterOptions _consulRegisterOptions;
        private readonly ConsulClientOptions _consulClientOptions;
        public ConsulRegister(IOptionsMonitor<ConsulRegisterOptions> consulRegisterOptions, IOptionsMonitor<ConsulClientOptions> consulClientOptions)
        {
            _consulRegisterOptions = consulRegisterOptions.CurrentValue;
            _consulClientOptions = consulClientOptions.CurrentValue;
        }

        public async Task UseConsulRegist()
        {
            using (ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri($"http://{_consulClientOptions.IP}:{_consulClientOptions.Port}/");
                c.Datacenter = _consulClientOptions.Datacenter;
            }))
            {
                await client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = $"{_consulRegisterOptions.GroupName}-{_consulRegisterOptions.IP}-{_consulRegisterOptions.Port}",//唯一Id
                    Name = _consulRegisterOptions.GroupName,//组名称-Group
                    Address = _consulRegisterOptions.IP,
                    Port = _consulRegisterOptions.Port,
                    Tags = new string[] { _consulRegisterOptions.Tag ?? "Tags is null" },
                    Check = new AgentServiceCheck()
                    {
                        Interval = TimeSpan.FromSeconds(_consulRegisterOptions.Interval),
                        HTTP = _consulRegisterOptions.HealthCheckUrl,
                        Timeout = TimeSpan.FromSeconds(_consulRegisterOptions.Timeout),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(_consulRegisterOptions.DeregisterCriticalServiceAfter),
                    }
                });
                Console.WriteLine($"{JsonConvert.SerializeObject(_consulRegisterOptions)} 完成注册");
            }
        }

        public async Task UseConsulRegistgRPC()
        {
            using (ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri($"http://{_consulClientOptions.IP}:{_consulClientOptions.Port}/");
                c.Datacenter = _consulClientOptions.Datacenter;
            }))
            {
                await client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = $"{_consulRegisterOptions.IP}-{_consulRegisterOptions.Port}-{_consulRegisterOptions.GroupName}",//唯一Id
                    Name = _consulRegisterOptions.GroupName,//组名称-Group
                    Address = _consulRegisterOptions.IP,
                    Port = _consulRegisterOptions.Port,
                    Tags = new string[] { _consulRegisterOptions.Tag ?? "Tags is null" },
                    Check = new AgentServiceCheck()
                    {
                        Interval = TimeSpan.FromSeconds(_consulRegisterOptions.Interval),
                        //HTTP = this._consulRegisterOption.HealthCheckUrl,
                        GRPC = _consulRegisterOptions.HealthCheckUrl,//gRPC特有
                        GRPCUseTLS = false,//支持http
                        Timeout = TimeSpan.FromSeconds(_consulRegisterOptions.Timeout),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(_consulRegisterOptions.DeregisterCriticalServiceAfter),

                    }
                });
                Console.WriteLine($"{JsonConvert.SerializeObject(_consulRegisterOptions)} 完成注册");
            }
        }
    }
}
