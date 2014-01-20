/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/28 11:56:14
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopEntity;
using WebSharing.DB4ODAL;
using TopDal;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace TopLogic
{
    public class ArticleLogic : LogicBase<Article>
    {
        /// <summary>
        /// 获取相关文档
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="num">要取的数量</param>
        /// <returns></returns>
        public IEnumerable<Article> GetRelatedArticleList(long id, string keywords, int num)
        {
            List<Article> articleList = GetList();

            string parten = string.Format("{0}", string.Join("|", keywords.Split(',', '，', ' ')));

            int c = Regex.Matches(articleList[0].Content, parten).Count;

            return (from d in articleList where d.Id != id orderby Regex.Matches(d.Content, parten).Count descending select d).Take(num);
        }

        /// <summary>
        /// 自动获取正确的来源标题
        /// </summary>
        /// <param name="orgn">原始的来源，用户输入的信息</param>
        /// <param name="url">用户输入的来源网址</param>
        /// <returns></returns>
        public string GetArticleOrignSourceTitle(string orgn, string url)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(orgn) && orgn != "本站")
            {
                result = orgn;
            }
            else
            {
                if (string.IsNullOrEmpty(url) == false && url.ToLower().Contains(BasicCache.SiteConfig.SiteUrl.ToLower().Replace("http://", "")))
                {
                    result = "本站";
                }
                else
                {
                    result = "网络转载";
                }
            }
            return result;
        }

        /// <summary>
        /// 获取文章来源url
        /// </summary>
        /// <param name="url">用户输入的来源地址</param>
        /// <returns></returns>
        public string GetArticleOrignSourceUrl(string url)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(url))
            {
                result = BasicCache.SiteConfig.SiteUrl;
            }
            else
            {
                result = url;
            }

            if (string.IsNullOrEmpty(result))
            {
                result = string.Empty;
            }
            else if (result.StartsWith("http://") == false && result.StartsWith("https://") == false)
            {
                result = "http://" + url;
            }

            return result;
        }

        /// <summary>
        /// 处理指定文章的内容，上传文章内容中的图片。处理之后的文章内容做相应的变化。
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public bool AutoUploadImage(string content, out string result)
        {
            try
            {
                StringBuilder resultBuilder = new StringBuilder(content);

                string imgDir = System.Web.Hosting.HostingEnvironment.MapPath("~/userfiles/images/");
                WebClient client = new WebClient();

                Regex pa = new Regex("<img[^>]+src\\s*=\\s*['\"]([^'\"]+)['\"][^>]*>");

                MatchCollection matches = pa.Matches(content);
                foreach (Match match in matches)
                {
                    // 保存文件
                    string src = match.Groups[1].Value;

                    // 不是远程文件跳过
                    if (!src.StartsWith("http://") && !src.StartsWith("https://"))
                    {
                        continue;
                    }

                    string ext = Path.GetExtension(src);

                    if (string.IsNullOrEmpty(ext.TrimStart('.')))
                    {
                        // 如果是类似mvc形式得不到真正的后缀名，则默认设置为jpg格式
                        ext = ".jpg";
                    }

                    string autoFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ext;
                    string fileName = Path.Combine(imgDir, autoFileName);
                    client.DownloadFile(src, fileName);
                    // 替换地址
                    resultBuilder = resultBuilder.Replace(src, "/userfiles/images/" + autoFileName);
                }
                result = resultBuilder.ToString();
                return true;
            }
            catch (Exception ex)
            {
                logger.ErrorException("自动上传图片失败。", ex);
                result = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// 查询所有文章的Id集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<long> GetAllIds()
        {
            return this.GetList().Select(p => p.Id);
        }

        /// <summary>
        /// 根据Id获取文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Article GetById(long id)
        {
            return GetList(p => p.Id == id).FirstOrDefault();
        }
    }
}
