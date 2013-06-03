using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Top.Api.Response;
using TopLogic;
using TopArticleEntity;
using TopUtilityTool;

namespace TopSite
{
    public partial class _Default : System.Web.UI.Page
    {
        TopLogic.ArticleLogic articleLogic = new TopLogic.ArticleLogic();
        TopLogic.CatalogueLogic catalogueLogic = new TopLogic.CatalogueLogic();
        SiteLogic siteLogic = new SiteLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = string.Format("{0}-{1}", BasicCache.SiteConfig.SiteName, BasicCache.SiteConfig.KeyWords);
                ShowArticleList();
                BindKeywords();
            }
        }

        private void BindKeywords()
        {
            SiteMaster master = this.Master as SiteMaster;
            if (master != null)
            {
                SiteConfig siteConfig = BasicCache.SiteConfig;
                if (siteConfig == null || string.IsNullOrEmpty(siteConfig.KeyWords))
                {
                    master.PageKeywords = "减肥";
                }
                else
                {
                    master.PageKeywords =TopUtility.GetRandomKeyword(siteConfig.TopKeywords);
                }
            }
        }

        private void ShowArticleList()
        {

            List<TopArticleEntity.Catalogue> catalogueList = catalogueLogic.GetList(p => true);
            foreach (var item in catalogueList)
            {
                TopSite.Controls.ArticleList list = (Controls.ArticleList)Page.LoadControl("~/Controls/ArticleList.ascx");
                list.PageSize = 22;
                list.CatalogueId = item.Id;
                list.CatalogueTitle = item.Title;
                list.ArticleLogic = articleLogic;

                placeHolder.Controls.Add(list);
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            articleLogic.Dispose();
            catalogueLogic.Dispose();
            siteLogic.Dispose();
        }
    }
}
