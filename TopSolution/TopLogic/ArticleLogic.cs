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
using TopArticleEntity;
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
        public IEnumerable<Article> GetRelatedArticleList(string keywords, int num)
        {
            List<Article> articleList = GetList(p => true);

            string parten = string.Format("[{0}]", keywords);

            return (from d in articleList orderby Regex.Matches(d.Content, parten).Count select d).Take(num);
        }
    }
}
