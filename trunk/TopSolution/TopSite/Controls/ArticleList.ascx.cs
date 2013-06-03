using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TopLogic;
using TopArticleEntity;

namespace TopSite.Controls
{
    public partial class ArticleList : System.Web.UI.UserControl
    {
        public int CatalogueId { get; set; }
        public string CatalogueTitle { get; set; }
        public ArticleLogic ArticleLogic { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initpage();
            }
        }

        private void Initpage()
        {
            if (PageSize == 0)
            {
                PageSize = 10;
            }
            if (PageIndex < 1)
            {
                PageIndex = 1;
            }

            List<Article> allList = ArticleLogic.GetList(p => p.CatalogueId == CatalogueId).OrderByDescending(p=>p.CreateDate).ToList();
            PageCount = (int)Math.Ceiling((double)allList.Count / PageSize);
            if (PageIndex > PageCount)
            {
                PageIndex = PageCount;
            }

            IEnumerable<Article> articleList = allList.OrderByDescending(p => p.CreateDate).Skip(PageSize * (PageIndex - 1)).Take(PageSize);
            RepeaterArticleList.DataSource = articleList;
            RepeaterArticleList.DataBind();
        }
    }
}