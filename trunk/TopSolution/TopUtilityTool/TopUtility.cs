using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TopEntity;
using System.Text.RegularExpressions;
using System.Net;

namespace TopUtilityTool
{
    public static class TopUtility
    {
        /// <summary>
        /// 获取随机淘宝客关键字
        /// </summary>
        /// <param name="keywords">原始关键字字符串</param>
        /// <param name="spliter">分隔符</param>
        /// <returns></returns>
        public static string GetRandomKeyword(string keywords, params string[] spliter)
        {
            if (string.IsNullOrEmpty(keywords))
            {
                return "减肥";
            }

            string s = keywords;

            if (spliter == null || spliter.Length == 0)
            {
                spliter = new string[] { ",", "，" };
            }

            string[] keywordsArray = s.Split(spliter, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();

            int index = r.Next(keywordsArray.Length);

            return keywordsArray[index];
        }

        /// <summary>
        /// 获取强调关键词的内容。
        /// </summary>
        /// <param name="content">原内容</param>
        /// <param name="newKeywords">新关键词</param>
        /// <returns></returns>
        public static string GetStressedContent(string content, string newKeywords)
        {
            string result = content;
            char[] spliter = new char[] { ',', '，' };
            string[] newKeywordsArray = newKeywords.Split(spliter);

            for (int i = 0; i < newKeywordsArray.Length; i++)
            {
                result = result.Replace(newKeywordsArray[i], string.Format("<strong>{0}</strong>", newKeywordsArray[i]));
            }

            return result;
        }

        /// <summary>
        /// 将已经加强过的内容转换回没有加强过的内容
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="oldKeywords">旧的关键字</param>
        /// <returns></returns>
        public static string GetUnstressedContent(string content, string oldKeywords)
        {
            string result = content;
            char[] spliter = new char[] { ',', '，' };
            string[] oldKeywordsArray = oldKeywords.Split(spliter);

            for (int i = 0; i < oldKeywordsArray.Length; i++)
            {
                result = result.Replace(string.Format("<strong>{0}</strong>", oldKeywordsArray[i]), oldKeywordsArray[i]);
            }

            return result;
        }

        /// <summary>
        /// 使用正则表达式提取字符串
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        /// <param name="oriStr">要从中提取的字符串</param>
        /// <returns></returns>
        public static string GetStringByReg(string pattern, string oriStr)
        {
            if (Regex.IsMatch(oriStr, pattern))
            {
                return Regex.Match(oriStr, pattern, RegexOptions.Singleline).Value;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 更新js模板文件夹中的所有通用js文件
        /// </summary>
        /// <param name="siteConfig"></param>
        public static void UpdateConmmonJs(SiteConfig siteConfig)
        {
            try
            {
                string sourceDir = System.Web.Hosting.HostingEnvironment.MapPath("~/ScriptsTemplates/");
                string targetDir = System.Web.Hosting.HostingEnvironment.MapPath("~/Scripts/");

                if (Directory.Exists(sourceDir))
                {
                    foreach (string file in Directory.GetFiles(sourceDir, "*.js"))
                    {
                        string pathTarget = Path.Combine(targetDir, Path.GetFileName(file));
                        string content = File.ReadAllText(file);
                        string contentNew = content.Replace("{sitename}", siteConfig.SiteName);
                        contentNew = contentNew.Replace("{siteurl}", siteConfig.SiteUrl);
                        File.WriteAllText(pathTarget, contentNew);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 将指定url的网页内容写入文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <param name="fileName"></param>
        /// <param name="webClient"></param>
        public static void WriteHtmlToFile(string url, Encoding encoding, string fileName, WebClient webClient = null)
        {
            bool needClear = false;
            try
            {
                string dir = Path.GetDirectoryName(fileName);
                if (Directory.Exists(dir) == false)
                {
                    Directory.CreateDirectory(dir);
                }

                if (webClient == null)
                {
                    webClient = new WebClient();
                    needClear = true;
                }
                webClient.Encoding = encoding;

                string content = webClient.DownloadString(url);
                if (string.IsNullOrEmpty(content) == false)
                {
                    File.WriteAllText(content, fileName);
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (needClear && webClient != null)
                {
                    webClient.Dispose();
                    webClient = null;
                }
            }
        }
    }
}
