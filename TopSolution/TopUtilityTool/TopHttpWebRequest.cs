﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.IO;

namespace TopUtilityTool
{
    /// <summary>
    /// 先说下流程
    ///1.使用httpwebrequest先进入你要登录的网站，获取cookie
    ///2.使用第一步获取的cookie到验证码的网页将验证码下载下来。
    ///3.使用Post数据 发送至网站。如果有cookie则继续保存。
    ///4.使用第三步的cookie登陆相关网页操作。
    /// </summary>
    public class TopHttpWebRequest
    {
        /// <summary>
        /// 通过get方式请求页面，传递一个实例化的cookieContainer
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static ArrayList GetHtmlData(string postUrl, CookieContainer cookie)
        {
            HttpWebRequest request;
            HttpWebResponse response;
            ArrayList list = new ArrayList();
            request = WebRequest.Create(postUrl) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0";
            request.CookieContainer = cookie;
            request.KeepAlive = true;

            request.CookieContainer = cookie;
            try
            {
                //获取服务器返回的资源
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default))
                    {
                        cookie.Add(response.Cookies);
                        //保存Cookies
                        list.Add(true);
                        list.Add(cookie);
                        list.Add(reader.ReadToEnd());
                        list.Add(Guid.NewGuid().ToString());//图片名
                    }
                }
            }
            catch (WebException ex)
            {
                list.Clear();
                list.Add(false);
                list.Add("发生异常/n/r");
                WebResponse wr = ex.Response;
                using (Stream st = wr.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(st, System.Text.Encoding.Default))
                    {
                        list.Add(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                list.Add(false);
                list.Add("5");
                list.Add("发生异常：" + ex.Message);
            }
            return list;
        }

        /// <summary>
        /// 下载验证码图片并保存到本地
        /// </summary>
        /// <param name="Url">验证码URL</param>
        /// <param name="cookCon">Cookies值</param>
        /// <param name="savePath">保存位置/文件名</param>
        public static bool DowloadCheckImg(string Url, CookieContainer cookCon, string savePath)
        {
            bool bol = true;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
            //属性配置
            webRequest.AllowWriteStreamBuffering = true;
            webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
            webRequest.MaximumResponseHeadersLength = -1;
            webRequest.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon; .NET CLR 1.1.4322)";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "GET";
            webRequest.Headers.Add("Accept-Language", "zh-cn");
            webRequest.Headers.Add("Accept-Encoding", "gzip,deflate");
            webRequest.KeepAlive = true;
            webRequest.CookieContainer = cookCon;
            try
            {
                //获取服务器返回的资源
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (Stream sream = webResponse.GetResponseStream())
                    {
                        List<byte> list = new List<byte>();
                        while (true)
                        {
                            int data = sream.ReadByte();
                            if (data == -1)
                                break;
                            list.Add((byte)data);
                        }
                        File.WriteAllBytes(savePath, list.ToArray());
                    }
                }
            }
            catch (WebException ex)
            {
                bol = false;
            }
            catch (Exception ex)
            {
                bol = false;
            }
            return bol;
        }

        /// <summary>
        /// 发送相关数据至页面
        /// 进行登录操作
        /// 并保存cookie
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="postUrl"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static ArrayList PostData(string postData, string postUrl, CookieContainer cookie)
        {
            ArrayList list = new ArrayList();
            HttpWebRequest request;
            HttpWebResponse response;
            ASCIIEncoding encoding = new ASCIIEncoding();
            request = WebRequest.Create(postUrl) as HttpWebRequest;
            byte[] b = encoding.GetBytes(postData);
            request.UserAgent = "Mozilla/4.0";
            request.Method = "POST";
            request.CookieContainer = cookie;
            request.ContentLength = b.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(b, 0, b.Length);
            }

            try
            {
                //获取服务器返回的资源
                using (response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        if (response.Cookies.Count > 0)
                        {
                            cookie.Add(response.Cookies);
                        }
                        list.Add(true);
                        list.Add(cookie);
                        list.Add(reader.ReadToEnd());
                    }
                }
            }
            catch (WebException wex)
            {
                list.Clear();
                list.Add(false);
                WebResponse wr = wex.Response;
                using (Stream st = wr.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(st, System.Text.Encoding.Default))
                    {
                        list.Add(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                list.Add(false);
                list.Add("发生异常/n/r" + ex.Message);
            }
            return list;
        }
    }
}
