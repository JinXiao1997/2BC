
using System;
using System.Collections.Generic;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
namespace Tools
{
   public class JwtTools
    {
        private static readonly string secret = "12312312321321321321321ewqeqw";
        public static string GetToken(string name)
        {
            double exp = (DateTime.UtcNow.AddSeconds(3600) - new DateTime(1970, 1, 1)).TotalSeconds;//过期时间
            var payload = new Dictionary<string, object>
            {
                {"UserName",name},
                { "exp",exp}//过期时间的key必须叫exp
            };
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serialer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serialer, urlEncoder);
            return encoder.Encode(payload, secret);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string DescTonken(string token)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                var json = decoder.Decode(token, secret, verify: true);
                return json.ToString();
            }
            catch (TokenExpiredException )
            {
               throw  new Exception("令牌已过期");
            }
            catch (SignatureVerificationException)
            {
                throw new Exception("签名验证失败，数据可能被篡改");
            }
        }
   
    }
}
