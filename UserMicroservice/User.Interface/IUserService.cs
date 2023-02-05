using Framework.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicro.DTO;
using UserMicro.Model.Models;

namespace UserMicro.Interface
{
    public interface IUserService
    {
        /**
        * 发送验证码
        * @param mobile
        */
        AjaxResult SendVerifyCode(string mobile);

        /// <summary>
        /// 发送验证码前验证
        /// </summary>
        /// <param name="mobile"></param>
        AjaxResult CheckPhoneNumberBeforeSend(string mobile);

        /**
         * 用户注册
         * @param user
         * @param code
         */
        void Register(RegisterModel user);

    }
}
