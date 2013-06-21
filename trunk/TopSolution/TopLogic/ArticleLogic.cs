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
        public IEnumerable<Article> GetRelatedArticleList(int id, string keywords, int num)
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

            if (result.StartsWith("http://") == false && result.StartsWith("https://") == false)
            {
                result = "http://" + url;
            }
            return result;
        }
    }
}
