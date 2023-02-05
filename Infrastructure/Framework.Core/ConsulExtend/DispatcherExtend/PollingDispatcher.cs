﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Consul;
using Framework.Core.ConsulExtend;
using Microsoft.Extensions.Options;

namespace Framework.Core.ConsulExtend.DispatcherExtend
{
    /// <summary>
    /// 轮询
    /// </summary>
    public class PollingDispatcher : AbstractConsulDispatcher
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

        public PollingDispatcher(IOptionsMonitor<ConsulClientOptions> consulClientOption) : base(consulClientOption)
        {
        }
        #endregion

        /// <summary>
        /// 轮询
        /// </summary>
        /// <param name="serviceCount"></param>
        /// <returns></returns>
        protected override int GetIndex()
        {
            return iTotalCount++ % _CurrentAgentServiceDictionary.Length;
        }
    }
}