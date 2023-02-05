using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.ConsulExtend.DistributedExtend
{
    public interface IConsulDistributed : IDisposable
    {
        /// <summary>
        /// 非API封装，仅为展示
        /// </summary>
        Task KVShow();
        Task<IDistributedLock> AcquireLock(string key);
        Task ExecuteLocked(string key, Action action);
    }
}
