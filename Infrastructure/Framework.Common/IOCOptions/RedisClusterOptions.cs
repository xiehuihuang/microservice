using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.IOCOptions
{
    /// <summary>
    /// Redis集群配置类
    /// </summary>
    public class RedisClusterOptions
    {
        /// <summary>
        /// 包括IP:端口,IP:端口格式
        /// </summary>
        public List<string> Hosts { get; set; }
    }


}
