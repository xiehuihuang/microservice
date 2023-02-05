using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Common.Models
{
    /// <summary>
    /// 通用数据返回类型
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 状态码  无明确意义  前端用Result
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// 常规结果都在这里
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 特殊传值
        /// </summary>
        public object OtherValue { get; set; }
        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// true 成功
        /// flase 失败
        /// </summary>
        public bool Result { get; set; }
    }


    public class AjaxResult<T> : AjaxResult
    {
        /// <summary>
        /// 泛型数据
        /// </summary>
        public T TValue { get; set; }
    }
}
