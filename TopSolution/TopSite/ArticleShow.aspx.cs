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
using TopUtilityTool;

namespace TopSite
{
    public partial class ArticleShow : System.Web.UI.Page
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        protected Article CurArticle = null;
        protected ArticleLogic CurArticleLogic = new ArticleLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowArticle();
                BindTopKeywords();
                BindRelatedArticle();
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
                        CurArticle.ClickNum += 1;
                        CurArticleLogic.Save(CurArticle);
                        this.Title = string.Format("{0}-{1}", CurArticle.Title, BasicCache.SiteConfig.SiteName);
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx");
                        logger.Error("显示文章失败，不存在id为" + strId + "的文章");
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                    logger.Error("显示文章失败，id" + strId + "不合法");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
                logger.Error("显示文章失败，缺少关键参数id");
            }
        }

        private void BindTopKeywords()
        {
            if (CurArticle != null)
            {
                SiteMaster sitemaster = this.Master.Master as SiteMaster;
                if (sitemaster != null)
                {
                    sitemaster.PageKeywords = TopUtility.GetRandomKeyword(CurArticle.TopKeywords);
                }
            }
        }

        private void BindRelatedArticle()
        {
            this.RelatedArticleList1.TargetId = CurArticle.Id;
            this.RelatedArticleList1.CurArticleLogic = CurArticleLogic;
            this.RelatedArticleList1.CurArticleKeywords = CurArticle.KeyWords;
            this.RelatedArticleList1.ShowNums = 30;
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            CurArticleLogic.Dispose();
        }
    }
}