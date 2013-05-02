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
using TopArticleEntity;

namespace TopSite.Account
{
    public partial class ArticleList : System.Web.UI.Page
    {
        ArticleLogic articleLogic = new ArticleLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowList();
            }
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

            }
        }

        private void ShowList(int pageIndex = 0)
        {
            IEnumerable<Article> articleList = articleLogic.GetList(p => true).OrderBy(p => p.CreateDate);
            this.GridViewArticleList.DataSource = articleList;
            this.GridViewArticleList.DataBind();
            if (GridViewArticleList.PageCount >= pageIndex)
            {
                GridViewArticleList.PageIndex = pageIndex;
            }
            else
            {
                GridViewArticleList.PageIndex = GridViewArticleList.PageCount;
            }
        }

        protected void GridViewArticleList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageIndex = e.NewPageIndex;
            ShowList(pageIndex);
        }


    }
}