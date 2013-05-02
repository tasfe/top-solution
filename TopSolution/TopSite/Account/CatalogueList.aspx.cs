/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/28 16:49:27
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
    public partial class CatalogueList : System.Web.UI.Page
    {
        CatalogueLogic CatalogueLogic = new CatalogueLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowCatalogues();
            }
        }

        private void ShowCatalogues()
        {
            IEnumerable<Catalogue> list = CatalogueLogic.GetList(d => true).OrderBy(d=>d.Order);
            this.GridViewCatalogue.DataSource = list;
            this.GridViewCatalogue.DataBind();
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            LinkButton linkbtn = sender as LinkButton;
            if (linkbtn != null)
            { 
            
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            CatalogueLogic.Dispose();
            CatalogueLogic = null;
        }
    }
}