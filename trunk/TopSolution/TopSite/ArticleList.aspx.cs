/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/28 16:54:19
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

namespace TopSite
{
    public partial class ArticleList : System.Web.UI.Page
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        CatalogueLogic catalogueLogic = new CatalogueLogic();
        ArticleLogic articleLogic = new ArticleLogic();
        public int PageCount { get; set; }
        protected int PageIndex = 1;
        protected int CataId = 0;
        protected Catalogue curCatalogue;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitList();
                InitKeywords(curCatalogue);
            }
        }

        private void InitList()
        {
            string strCataID = Request.QueryString["id"];

            if (int.TryParse(strCataID, out CataId))
            {
                Catalogue catalogue = catalogueLogic.GetList(p => p.Id == CataId).FirstOrDefault();
                if (catalogue != null)
                {
                    curCatalogue = catalogue;
                    string strPageIndex = Request.QueryString["page"];
                    if (int.TryParse(strPageIndex, out PageIndex))
                    {
                        this.ArticleList1.PageIndex = PageIndex;
                    }
                    this.ArticleList1.articleLogic = articleLogic;
                    this.ArticleList1.CatalogueId = CataId;
                    this.ArticleList1.CatalogueTitle = catalogue.Title;
                    this.Title =string.Format("{0}-{1}", catalogue.Title,BasicCache.SiteConfig.SiteName);                    
                }
                else
                {
                    logger.Error("不存在id为" + strCataID + "的目录。");
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        private void InitKeywords(Catalogue catalogue)
        {
            if (catalogue != null)
            {
                SiteMaster master = this.Master.Master as SiteMaster;
                if (master != null)
                {
                    master.PageKeywords = catalogue.TopKeywords;
                }
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            catalogueLogic.Dispose();
            articleLogic.Dispose();
        }
    }
}