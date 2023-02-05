using Framework.WebCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Common.Models;
using Framework.Core;

namespace Framework.WebCore.FilterExtend
{
    public class CustomAction2CommitFilterAttribute : ActionFilterAttribute
    {
        #region Identity
        private readonly ILogger<CustomAction2CommitFilterAttribute> _logger;
        private readonly RedisClusterHelper _cacheClientDB;
        private static string KeyPrefix = "2CommitFilter";

        public CustomAction2CommitFilterAttribute(ILogger<CustomAction2CommitFilterAttribute> logger, RedisClusterHelper cacheClientDB)
        {
            _logger = logger;
            _cacheClientDB = cacheClientDB;
        }
        #endregion

        /// <summary>
        /// 防重复提交周期  单位秒
        /// </summary>
        public int TimeOut = 3;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            UserInfo user = context.HttpContext.GetCurrentUserInfo();
            string url = context.HttpContext.Request.Path.Value;
            string argument = JsonConvert.SerializeObject(context.ActionArguments);
            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            string agent = context.HttpContext.Request.Headers["User-Agent"];
            string sInfo = $"{user.id}+{user.username}-{url}-{argument}-{ip}-{agent}";
            // 生成唯一标识
            string summary = MD5Helper.MD5EncodingOnly(sInfo);

            string totalKey = $"{KeyPrefix}-{summary}";

            // 获取key对应的结果
            string result = _cacheClientDB.Get(totalKey);
            if (string.IsNullOrEmpty(result))
            {
                _cacheClientDB.Set(totalKey, "1", TimeSpan.FromSeconds(TimeOut));//3秒有效期
                _logger.LogInformation($"CustomAction2CommitFilterAttribute:{sInfo}");
            }
            else
            {
                //已存在
                _logger.LogWarning($"CustomAction2CommitFilterAttribute重复请求:{sInfo}");
                context.Result = new JsonResult(new AjaxResult()
                {
                    Result = false,
                    Message = $"请勿重复提交，{TimeOut}s之后重试"
                });
            }

            //CurrentUser currentUser = context.HttpContext.GetCurrentUserBySession();
            //if (currentUser == null)
            //{
            //    //if (this.IsAjaxRequest(context.HttpContext.Request))
            //    //{ }
            //    context.Result = new RedirectResult("~/Fourth/Login");
            //}
            //else
            //{
            //    this._logger.LogDebug($"{currentUser.Name} 访问系统");
            //}
        }
        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
