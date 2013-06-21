/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/28 16:47:41
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
using TopLogic;
using TopEntity;
using NLog;

namespace TopSite.Account
{
    public partial class ArticleList : System.Web.UI.Page
    {
        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        ArticleLogic articleLogic = new ArticleLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { ShowList(1); }
        }

        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnDel = sender as LinkButton;
                if (lbtnDel != null)
                {
                    string strId = lbtnDel.CommandArgument;
                    int id = 0;
                    if (int.TryParse(strId, out id))
                    {
                        Article article = articleLogic.GetList(p => p.Id == id).FirstOrDefault();
                        if (article != null)
                        {
                            int oldPageIndex = GridViewArticleList.PageIndex;
                            articleLogic.Delete(article);
                            ShowList(oldPageIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorException("删除文章失败", ex);
            }
        }

        private void ShowList(int pageIndex = 0)
        { 
            if (GridViewArticleList.PageCount >= pageIndex)
            {
                GridViewArticleList.PageIndex = pageIndex;
            }
            else
            {
                GridViewArticleList.PageIndex = GridViewArticleList.PageCount;
            }
            List<Article> articleList = articleLogic.GetList().OrderBy(p => p.CreateDate).ToList();
            this.GridViewArticleList.DataSource = articleList;
            this.GridViewArticleList.DataBind();
        }

        protected void GridViewArticleList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageIndex = e.NewPageIndex;
            ShowList(pageIndex);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            articleLogic.Dispose();
        }
    }
}