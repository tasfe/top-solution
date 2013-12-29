using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Net;

namespace QQBuySdk
{
    public class OpenApiOauth
    {
        private String appOAuthID;
        private String appOAuthkey;
        private long? uin;
        private string accessToken;
        private const String FORMAT_DEFAULT = FORMAT_XML;
        private const String FORMAT_XML = "xml";
        private const String FORMAT_JSON = "json";

        private const String CHARSET = "utf-8";
        private const string SIGN = "sign";
        // 请求方法
        private String method = "post";

        private WebUtils webUtils;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appOAuthID"></param>
        /// <param name="appOAuthkey"></param>
        /// <param name="uin">卖家qq号</param>
        /// <param name="accessToken">卖家授权码</param>
        public OpenApiOauth(String appOAuthID, String appOAuthkey, long? uin, string accessToken)
        {
            this.appOAuthID = appOAuthID;
            this.appOAuthkey = appOAuthkey;
            this.uin = uin;
            this.accessToken = accessToken;
            webUtils = new WebUtils();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverUrl">接口请求url</param>
        /// <param name="txtParams">文本参数</param>
        /// <param name="fileParams">文件参数</param>
        /// <returns></returns>
        public string InvokeOpenApi(string serverUrl, IDictionary<string, string> txtParams, IDictionary<string, FileItem> fileParams)
        {
            // 添加协议级请求参数
            if (!txtParams.ContainsKey("timeStamp"))
            {
                txtParams.Add("timeStamp", GetTime());
            }
            if (!txtParams.ContainsKey("randomValue"))
            {
                txtParams.Add("randomValue", GetRandomValue().ToString());
            }
            if (!txtParams.ContainsKey("uin") && this.uin != null)
            {
                txtParams.Add("uin", this.uin.ToString());
            }
            if (!txtParams.ContainsKey("accessToken") && !string.IsNullOrEmpty(this.accessToken))
            {
                txtParams.Add("accessToken", this.accessToken);
            }
            if (!txtParams.ContainsKey("appOAuthID"))
            {
                txtParams.Add("appOAuthID", this.appOAuthID);
            }

            if (!txtParams.ContainsKey("charset"))
            {
                txtParams.Add("charset", CHARSET);
            }
            if (!txtParams.ContainsKey("format"))
            {
                txtParams.Add("format", FORMAT_DEFAULT);
            }
            if (FORMAT_JSON.Equals(txtParams["format"]))
            {
                txtParams.Add("pureData", "1");
            }
                    
            if (serverUrl.Contains("?"))
            {
                serverUrl = serverUrl.Substring(0, serverUrl.IndexOf('?') + 1);
            }

            // 计算签名
            Uri uri = new Uri(serverUrl);
            String apiName = uri.AbsolutePath;

            //sign,自动生成
            String encoding = CHARSET;
            txtParams.TryGetValue("charset", out encoding);
            String sign = GetSign(method, apiName, txtParams, appOAuthkey + "&", encoding);

            // 添加签名参数
            txtParams.Add(SIGN, sign);

            // 是否需要上传文件
            string body = string.Empty;
            if (fileParams != null && fileParams.Count > 0)
            {
                body = webUtils.DoPost(serverUrl, txtParams, fileParams);
            }
            else
            {
                 body = webUtils.DoPost(serverUrl, txtParams);
                //body = webUtils.DoGet(serverUrl, txtParams);
            }
            body = body.Replace("<![CDATA[", "").Replace("]]>", "").Trim();

            return body;
        }


        private string GetTime()
        {
            Int64 retval = 0;
            DateTime st = new DateTime(1970, 1, 1);
            TimeSpan t = (DateTime.Now.ToUniversalTime() - st);
            retval = (Int64)(t.TotalMilliseconds + 0.5);
            return retval.ToString();
        }
        private int GetRandomValue()
        {
            Random radom = new Random();
            return radom.Next(1, int.MaxValue);
        }


        /* 生成签名
         * @param method HTTP请求方法 "get" / "post"
         * @param url_path CGI名字, 
         * @param params URL请求参数
         * @param secret 密钥
         * @return 签名值
         * @throws OpensnsException 不支持指定编码以及不支持指定的加密方法时抛出异常。
         */
        private String GetSign(String method, String url_path, IDictionary<String, String> parameters, String secret, String encoding)
        {
            String tmp = GetSource(method, url_path, parameters, encoding);

            System.IO.File.WriteAllText(@"D:\addSourceString.txt", tmp);

            byte[] signatureKey = Encoding.Default.GetBytes(secret);
            HMACSHA1 hmacsha1 = new HMACSHA1(signatureKey);
            hmacsha1.ComputeHash(Encoding.GetEncoding(encoding).GetBytes(tmp));
            //hmacsha1.ComputeHash(tmp.get);

            byte[] hash = hmacsha1.Hash;
            string signature = Convert.ToBase64String(hash);
            return signature;
        }

        /* 
         * URL编码 (符合FRC1738规范)
         * @param input 待编码的字符串
         * @return 编码后的字符串
         * @throws OpenApiException 不支持指定编码时抛出异常。
         */
        private String EncodeUrl(String input, String encoding)
        {
            return webUtils.UrlEncode(input, Encoding.GetEncoding(encoding));
        }

        /* 生成签名所需源串
         * @param method HTTP请求方法 "get" / "post"
         * @param url_path CGI名字, 
         * @param params URL请求参数
         * @return 签名所需源串
         */
        private String GetSource(String method, String url_path, IDictionary<String, String> parameters, String encoding)
        {

            StringBuilder buffer = new StringBuilder();
            buffer.Append(method.ToUpper()).Append("&").Append(EncodeUrl(url_path, encoding)).Append("&");


            //把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();
            StringBuilder buffer2 = new StringBuilder();
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    buffer2.Append(key).Append("=").Append(value);//new String(params.get(keys[i]).getBytes(),charset));
                    buffer2.Append("&");

                }
            }
            if (buffer2.Length > 1)//移除最后一个&
                buffer2.Remove(buffer2.Length - 1, 1);

            buffer.Append(EncodeUrl(buffer2.ToString(), encoding));

	    //string sign = buffer.ToString();
	    //if(sign.Length > 1024){
		//sign = sign.SubString(0,1024);
	    //}

        //    return sign;
		
		return buffer.ToString();
        }


        /// <summary>
        /// 清除字典中值为空的项。
        /// </summary>
        /// <param name="dict">待清除的字典</param>
        /// <returns>清除后的字典</returns>
        private IDictionary<string, T> CleanupDictionary<T>(IDictionary<string, T> dict)
        {
            IDictionary<string, T> newDict = new Dictionary<string, T>(dict.Count);
            IEnumerator<KeyValuePair<string, T>> dem = dict.GetEnumerator();

            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                T value = dem.Current.Value;
                if (value != null)
                {
                    newDict.Add(name, value);
                }
            }

            return newDict;
        }

    }

}
