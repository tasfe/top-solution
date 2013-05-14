/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/28 16:54:49
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TopArticleEntity;
using TopLogic;

namespace TopSite
{
    public partial class ArticleShow : System.Web.UI.Page
    {
        protected Article CurArticle = null;
        protected ArticleLogic CurArticleLogic = new ArticleLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowArticle();
                BindTopKeywords();
            }
        }

        private void ShowArticle()
        {
            string strId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(strId) == false)
            {
                int id = 0;
                if (int.TryParse(strId, out id))
                {
                    CurArticle = CurArticleLogic.GetList(p => p.Id == id).FirstOrDefault();
                    if (CurArticle != null)
                    {

                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx");

                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");

                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        private void BindTopKeywords()
        {
            if (CurArticle != null)
            {
                SiteMaster sitemaster = this.Master.Master as SiteMaster;
                if (sitemaster != null)
                {
                    sitemaster.PageKeywords = CurArticle.TopKeywords;
                }
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            CurArticleLogic.Dispose();
        }
    }
}