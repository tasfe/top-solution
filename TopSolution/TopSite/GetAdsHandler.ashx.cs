/*********************************************************
 * copyright xinbohit.com 版权所有 
 * 开发人员：IvanYu
 * 创建时间：2014/1/8 12:50:49
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopSite.Controls;
using System.Web.UI;
using System.Web.SessionState;

namespace TopSite
{
    /// <summary>
    /// 用于获取广告的Handler
    /// 
    /// TODO:加验证机制，防止被别人利用
    /// </summary>
    public class GetAdsHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string keyword = context.Request.Form["keyword"];

            if (string.IsNullOrEmpty(keyword))
            {
                keyword = "减肥";
            }

            UserControl con = new UserControl();
            TopList topList = con.LoadControl("~/Controls/TopList.ascx") as TopList;
            topList.KeyWords = keyword;
            HtmlTextWriter w = new HtmlTextWriter(context.Response.Output);
            topList.RenderControl(w);
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