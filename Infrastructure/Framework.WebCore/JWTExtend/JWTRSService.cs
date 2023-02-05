using Framework.WebCore.JWTExtend.RSA;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WebCore.JWTExtend
{

    public class JWTRSService : IJWTService
    {
        private static Dictionary<string, JWTUserModel> TokenCache = new Dictionary<string, JWTUserModel>();

        #region Option注入
        private readonly JWTTokenOptions _JWTTokenOptions;
        public JWTRSService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            _JWTTokenOptions = jwtTokenOptions.CurrentValue;
        }
        #endregion

        public string GetToken(JWTUserModel userModel)
        {
            return IssueToken(userModel);
        }

        /// <summary>
        /// 刷新token的有效期问题上端校验
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public string GetTokenByRefresh(string refreshToken)
        {
            if (TokenCache.ContainsKey(refreshToken))
            {
                string token = IssueToken(TokenCache[refreshToken], 60);
                return token;
            }
            else
            {
                return "";
            }
        }

        public Tuple<string, string> GetTokenWithRefresh(JWTUserModel userInfo)
        {
            string token = IssueToken(userInfo, 60);//1分钟
            string refreshToken = IssueToken(userInfo, 60 * 60 * 24);//24小时
            TokenCache.Add(refreshToken, userInfo);

            return Tuple.Create(token, refreshToken);
        }



        private string IssueToken(JWTUserModel userModel, int second = 600)
        {
            //string jtiCustom = Guid.NewGuid().ToString();//用来标识 Token
            var claims = new[]
            {
                   new Claim(ClaimTypes.Name, userModel.username),
                   new Claim("Id", userModel.id.ToString()),
            };

            string keyDir = Directory.GetCurrentDirectory();
            if (RSAHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams) == false)
            {
                keyParams = RSAHelper.GenerateAndSaveKey(keyDir);
            }
            var credentials = new SigningCredentials(new RsaSecurityKey(keyParams), SecurityAlgorithms.RsaSha256Signature);

            var token = new JwtSecurityToken(
               issuer: _JWTTokenOptions.Issuer,
               audience: _JWTTokenOptions.Audience,
               claims: claims,
               expires: DateTime.Now.AddSeconds(second),//默认10分钟有效期
               notBefore: DateTime.Now,//立即生效  DateTime.Now.AddMilliseconds(30),//30s后有效
               signingCredentials: credentials);
            var handler = new JwtSecurityTokenHandler();
            string tokenString = handler.WriteToken(token);
            return tokenString;
        }
    }
}
