using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TopLogic;
using TopEntity;

namespace TopSite.Controls
{
    public partial class ArticleList : System.Web.UI.UserControl
    {
        public long CatalogueId { get; set; }
        public string CatalogueTitle { get; set; }
        public ArticleLogic ArticleLogic { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        /// <summary>
        /// 是否计算页数，如果不计算PageCount将为0
        /// </summary>
        public bool CalculatePageCount { get; set; }
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

            if (PageIndex > PageCount)
            {
                PageIndex = PageCount;
            }


            IEnumerable<Article> articleList = null;
            if (CalculatePageCount)
            {
                int totalCount = 0;
                articleList = ArticleLogic.GetListByPage(out totalCount,
                                                        p => p.CatalogueId == CatalogueId,
                                                        p => p.Id,
                                                        TopEntity.Enum.OrderEnum.Descending,
                                                        PageSize,
                                                        PageIndex);
                PageCount = (int)Math.Ceiling((double)totalCount / PageSize);
            }
            else
            {
                articleList = ArticleLogic.GetListByPage(p => p.CatalogueId == CatalogueId,
                                                        p => p.Id,
                                                        TopEntity.Enum.OrderEnum.Descending,
                                                        PageSize,
                                                        PageIndex);
            }

            RepeaterArticleList.DataSource = articleList;
            RepeaterArticleList.DataBind();
        }
    }
}