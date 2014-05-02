/*********************************************************
 * copyright xinbohit.com 版权所有 
 * 开发人员：IvanYu
 * 创建时间：2014/1/14 12:40:02
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopUtilityTool;
using System.Web;
using System.Net;

namespace TopLogic
{
    public class HtmlFileGenerater
    {
        private Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// 生成站点首页Html页
        /// </summary>
        public void GenerateHomePage()
        {
            try
            {
                string rawUrl = HttpContext.Current.Request.RawUrl;

                string fromUrl = "~/default.aspx";
                string fullFromUrl = GetFullUrl(fromUrl);
                string outFile = HttpContext.Current.Server.MapPath("~/index.html");
                TopUtility.WriteHtmlToFile(fullFromUrl, encoding, outFile);
            }
            catch (Exception ex)
            {
                Loger.LogErr(ex);
            }
        }

        /// <summary>
        /// 生成文章内容Html页
        /// </summary>
        /// <param name="ids">要生成的文章Id列表，如果为null，会生成全站文章</param>
        public void GenerateArticlePage(IEnumerable<long> ids)
        {
            try
            {
                string fromUrlBuilder = GetFullUrl("~/ArticleShow.aspx?o=1&id=");
                string outFileBase = HttpContext.Current.Server.MapPath("~/articles/");
                int oriLength = fromUrlBuilder.Length;

                if (ids == null) 
                
                {
                    ids = new ArticleLogic().GetAllIds();
                }

                using (WebClient client = new WebClient())
                {
                    foreach (var id in ids)
                    {
                        string fromUrl = fromUrlBuilder + id;
                        string outFile = System.IO.Path.Combine(outFileBase, string.Format("{0}.html", id));

                        TopUtility.WriteHtmlToFile(fromUrl, encoding, outFile, client);
                    }
                }
            }
            catch (Exception ex)
            {
                Loger.LogErr(ex);
            }
        }

        #region 私有方法

        /// <summary>
        /// 将以~表示的绝对路径，映射成完整路径
        /// </summary>
        /// <param name="url">以~表示的绝对路径</param>
        /// <returns></returns>
        private string GetFullUrl(string url)
        {
            string baseurl = string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
            return url.Replace("~", baseurl);
        }

        /// <summary>
        /// 获取一个十进制整数有几位
        /// </summary>
        /// <param name="num">整数</param>
        /// <returns></returns>
        private static int GetNumLenth(long num)
        {
            int count = (int)(Math.Log10(num) + 1);
            return count;
        }

        #endregion 私有方法
    }
}
