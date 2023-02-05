using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using System;
using System.Text;
using Framework.Common.Models;

namespace Framework.Core
{
    /// <summary>
    /// 1、需要导入aliyun-net-sdk-core依赖
    /// 2、在阿里云配置签名+模板
    /// https://dysms.console.aliyun.com/dysms.htm?spm=5176.b60019767.ProductAndService--ali--widget-home-product-recent.dre0.525216d03rcBLN#/domestic/text/template
    /// </summary>
    public class SMSTool
    {
        /// <summary>
        /// 提供手机号---验证码
        /// 相关配置写死了
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static AjaxResult SendValidateCode(string phoneNumber, string code)
        {
            /*
                regionId:cn-hangzhou
                accessKeyId: LTAI4GB4y***************
                accessKeySecret: b5JVF50XjGU3tmxc**************
             */
            try
            {
                IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", "LTAI5tHKBS86************", "TbvbiEcnINGelaPt8w************");
                DefaultAcsClient client = new DefaultAcsClient(profile);
                CommonRequest request = new CommonRequest();
                request.Method = MethodType.POST;
                request.Domain = "dysmsapi.aliyuncs.com";
                request.Version = "2017-05-25";
                request.Action = "SendSms";
                // request.Protocol = ProtocolType.HTTP;
                request.AddQueryParameters("PhoneNumbers", phoneNumber); // 电话号码
                request.AddQueryParameters("SignName", "需使用签名模板"); // 签名模板的名称
                request.AddQueryParameters("TemplateCode", "SMS_210070060"); // 验证码模板ID
                request.AddQueryParameters("TemplateParam", "{\"code\":\"" + code + "\"}"); // 验证码

                CommonResponse response = client.GetCommonResponse(request);
                Console.WriteLine(Encoding.Default.GetString(response.HttpResponse.Content));
                if (response.HttpStatus == 200)
                {
                    return new AjaxResult()
                    {
                        StatusCode = response.HttpStatus,
                        Result = true,
                        Message = "短信已发送"
                    };
                }
                else
                {
                    return new AjaxResult()
                    {
                        StatusCode = response.HttpStatus,
                        Result = false,
                        Message = "短信发送异常"
                    };
                }
            }
            catch (ServerException e)
            {
                Console.WriteLine(e);
                return new AjaxResult()
                {
                    Result = false,
                    Message = $"短信发送失败，服务器错误：{e.Message}"
                };
            }
            catch (ClientException e)
            {
                Console.WriteLine(e);
                return new AjaxResult()
                {
                    Result = false,
                    Message = $"短信发送失败，客户端错误：{e.Message}"
                };
            }
        }


        ///// <summary>
        ///// ToDo  号码筛选
        ///// </summary>
        ///// <param name="phoneNumber"></param>
        ///// <returns></returns>
        //public static AjaxResult CheckPhoneNumber(string phoneNumber)
        //{
        //    //1  数据库不存在
        //    //2  Redis注册频次
        //    //3  该号码一天多少次短信
        //    //4  该IP一天多少次短信
        //}
    }
}
