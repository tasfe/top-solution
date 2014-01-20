/*********************************************************
 * copyright xinbohit.com 版权所有 
 * 开发人员：IvanYu
 * 创建时间：2014/1/19 3:28:21
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopLogic;
using TopEntity;

namespace TopSite
{
    /// <summary>
    /// 获取文章当前点击数，并自加一，存库。
    /// </summary>
    public class GetClickNumHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string idStr = context.Request.QueryString["id"];
            int num = 0;

            if (string.IsNullOrEmpty(idStr) == false)
            {
                long id = 0;
                if (long.TryParse(idStr, out id))
                {
                    using (ArticleLogic logic = new ArticleLogic())
                    {
                        Article article = logic.GetById(id);
                        num = article.ClickNum++;
                        logic.Save(article);                        
                    }
                }
            }

            context.Response.Write(num.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}