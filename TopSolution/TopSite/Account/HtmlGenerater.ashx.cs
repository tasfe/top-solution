/*********************************************************
 * copyright xinbohit.com 版权所有 
 * 开发人员：IvanYu
 * 创建时间：2014/1/19 2:07:52
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
using TopUtilityTool;

namespace TopSite.Account
{
    /// <summary>
    /// 生成Html页。
    /// 
    /// 参数：
    /// type:生成类型。0：首页；1：全部内容页；2：指定内容页。
    /// 
    /// ids:要生成的内容Id集合。逗号间隔的id列表。
    /// </summary>
    public class HtmlGenerater : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strType = context.Request["type"];

            try
            {

                int type = 0;
                if (int.TryParse(strType, out type))
                {
                    HtmlFileGenerater h = new HtmlFileGenerater();
                    switch (type)
                    {
                        case 0:
                            h.GenerateHomePage();
                            context.Response.Write("生成成功。");
                            break;
                        case 1:
                            h.GenerateArticlePage(null);
                            context.Response.Write("生成成功。");
                            break;
                        case 2:
                            string ids = context.Request["ids"];
                            if (string.IsNullOrEmpty(ids)==false)
                            {
                                string[] idsArray = ids.Split(',');
                                List<long> idlist = new List<long>();
                                foreach (var idstr in idsArray)
                                {
                                    long temp = 0;
                                    if (long.TryParse(idstr, out temp))
                                    {
                                        idlist.Add(temp);
                                    }
                                }
                                h.GenerateArticlePage(idlist);
                                context.Response.Write("生成成功。");
                            }
                            else
                            {
                                context.Response.Write("缺少ids参数。");
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Loger.LogErr(ex);
                context.Response.Write("生成操作内部错误。");
            }
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