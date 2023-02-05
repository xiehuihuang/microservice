using Framework.Common.Models;
using Framework.Core;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicro.DTO;
using UserMicro.Interface;
using UserMicro.Model.Models;

namespace UserMicro.Service
{
    public class UserService : IUserService
    {
        private DbEntitys _db;
        //private readonly IMapper _IMapper;
        private RedisClusterHelper _cacheClientDB;
        
        public UserService(DbEntitys dbEntitys, RedisClusterHelper cacheClientDB)
        {
            _db = dbEntitys;
            _cacheClientDB = cacheClientDB;
            //_IMapper = iMapper;
        }

        private static readonly string KEY_PREFIX = "user:verify:code:";
        private static readonly object Redis_Lock = new object();

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public AjaxResult SendVerifyCode(string mobile)
        {
            Random random = new Random();
            string code = random.Next(100000, 999999).ToString();// 生成随机6位数字验证码

            string key = KEY_PREFIX + mobile;
            _cacheClientDB.Set(key, code, TimeSpan.FromMinutes(5));// 把验证码存储到redis中  5分钟有效,有则覆盖
            _cacheClientDB.Set(key + "1m1t", code, TimeSpan.FromMinutes(1));//一分钟只能发一次 

            ///调用接口 发送短信  付钱
            // 调用发送短信的方法
            return SMSTool.SendValidateCode(mobile, code);;
        }

        
        /// <summary>
        /// 1  数据库不存在
        /// 2  Redis注册频次
        /// 3  该号码一天多少次短信
        /// 4  该IP一天多少次短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public AjaxResult CheckPhoneNumberBeforeSend(string mobile)
        {
            var list = _db.User.Where(u => u.Mobile.Equals(mobile)).ToList();
            if (list.Count > 0)
            {
                return new AjaxResult()
                {
                    Result = false,
                    Message = "手机号码重复"
                };
            }

            string key = KEY_PREFIX + mobile;
            if (!string.IsNullOrWhiteSpace(_cacheClientDB.Get(key + "1m1t")))
            {
                return new AjaxResult()
                {
                    Result = false,
                    Message = "1分钟内只能发一次验证码"
                };
            }

            return new AjaxResult()
            {
                Result = true,
                Message = "发送成功"
            };
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        public void Register(RegisterModel user)
        {
            string key = KEY_PREFIX + user.Mobile;
            lock (Redis_Lock)//单线程，避免重复提交
            {
                string value = _cacheClientDB.Get(key);
                if (!user.VerifyCode.Equals(value))
                {
                    //验证码不匹配
                    throw new Exception("验证码不匹配");
                }
                _cacheClientDB.Remove(key);//把验证码从Redis中删除
            }

            user.Salt = MD5Helper.MD5EncodingOnly(user.Name);//不能改账号
            string md5Pwd = MD5Helper.MD5EncodingWithSalt(user.Password, user.Salt);
            user.Password = md5Pwd;
            _db.Add(user);
            int count = _db.SaveChanges();
            if (count != 1)
            {
                throw new Exception("用户注册失败");
            }
        }


    }
}
