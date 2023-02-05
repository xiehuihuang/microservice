using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Consul;
using Framework.Core.ConsulExtend;
using Microsoft.Extensions.Options;

namespace Framework.Core.ConsulExtend.DispatcherExtend
{
    /// <summary>
    /// 平均
    /// </summary>
    public class AverageDispatcher : AbstractConsulDispatcher
    {
        #region Identity
        private static int _iTotalCount = 0;
        private static int iTotalCount
        {
            get
            {
                return _iTotalCount;
            }
            set
            {
                _iTotalCount = value >= int.MaxValue ? 0 : value;
            }
        }

        private ConsulClientOptions _ConsulClientOption = null;

        public AverageDispatcher(IOptionsMonitor<ConsulClientOptions> consulClientOption) : base(consulClientOption)
        {
        }
        #endregion

        /// <summary>
        /// 平均
        /// </summary>
        /// <returns></returns>
        protected override int GetIndex()
        {
            return new Random(iTotalCount++).Next(0, _CurrentAgentServiceDictionary.Length);
        }
    }
}
