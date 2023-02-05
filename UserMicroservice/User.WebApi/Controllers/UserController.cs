using Framework.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UserMicro.DTO;
using UserMicro.Interface;

namespace UserMicro.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService iUserService)
        {
            _logger = logger;
            _userService = iUserService;
        }

        #region 注册

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult SendVerifyCode(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return new JsonResult(new ApiResult()
                {
                    Message = "请输入手机号"
                });
            }
            if (new Regex("^1[3458][0-9]{9}$").IsMatch(mobile) == false)
            {
                return new JsonResult(new ApiResult()
                {
                    Message = "手机号不合法"
                });
            }
            AjaxResult ajaxResult = _userService.CheckPhoneNumberBeforeSend(mobile);
            if (!ajaxResult.Result)//校验失败
            {
                return new JsonResult(ajaxResult);
            }
            else
            {
                var result = _userService.SendVerifyCode(mobile);
                return new JsonResult(result);
            }
        }


        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Register(RegisterModel registerModel)
        {
            _userService.Register(registerModel);
            return new JsonResult(new AjaxResult()
            {
                Result = true,
                Message = "注册成功"
            });
        }

        #endregion
    }
}
