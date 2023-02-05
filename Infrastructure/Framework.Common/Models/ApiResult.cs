using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Models
{
    public class ApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success
        {
            get
            {
                return string.IsNullOrWhiteSpace(Message);
            }
        }

        public ReponseCodeEnum Code { get; set; }

        /// <summary>
        /// 特指错误消息
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// 额外数据
        /// </summary>
        public object? Tag { get; set; }
    }

    public enum ReponseCodeEnum
    {
        NoAuthorize = 1
    }
}
