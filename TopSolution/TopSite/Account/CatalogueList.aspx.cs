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
using TopEntity;
using TopEntity.Enum;

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
            IEnumerable<Catalogue> list = CatalogueLogic.GetList(d => true).OrderBy(d => d.Order);
            this.GridViewCatalogue.DataSource = list;
            this.GridViewCatalogue.DataBind();
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            LinkButton linkbtn = sender as LinkButton;
            if (linkbtn != null)
            {
                string strId = linkbtn.CommandArgument;
                int id = 0;
                if (int.TryParse(strId, out id))
                {
                    btnSaveCatalogue.CommandArgument = EditStateEnum.Edit.ToString();
                    Catalogue catalogue = CatalogueLogic.GetList(p => p.Id == id).FirstOrDefault();
                    this.txtTitle.Text = catalogue.Title;
                    this.Order.Text = catalogue.Order.ToString();
                    this.KeyWords.Text = catalogue.KeyWords;
                    this.Summary.Text = catalogue.Summary;
                    this.TopKeywords.Text = catalogue.TopKeywords;
                    this.txtId.Value = catalogue.Id.ToString();
                }
            }
        }

        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            LinkButton linkbtn = sender as LinkButton;
            if (linkbtn != null)
            {
                string strId = linkbtn.CommandArgument;
                int id = 0;
                if (int.TryParse(strId, out id))
                {
                    Catalogue catalogue = CatalogueLogic.GetList(p => p.Id == id).FirstOrDefault();
                    if (catalogue != null)
                    {
                        CatalogueLogic.Delete(catalogue);
                        ShowCatalogues();
                    }
                }
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            CatalogueLogic.Dispose();
            CatalogueLogic = null;
        }

        private Catalogue GetCatalogueFrSave()
        {
            Catalogue result = null;

            string str = btnSaveCatalogue.CommandArgument;
            if (str == EditStateEnum.Edit.ToString())
            {
                int id = 0;
                if (int.TryParse(this.txtId.Value, out id))
                {
                    result = CatalogueLogic.GetList(p => p.Id == id).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                result = new Catalogue();
                result.Id = CatalogueLogic.GetNewIdentity();
            }

            result.Title = txtTitle.Text;
            result.KeyWords = KeyWords.Text;
            result.Summary = Summary.Text;
            result.Order = int.Parse(Order.Text);
            result.TopKeywords = TopKeywords.Text;

            return result;
        }

        protected void btnSaveCatalogue_Click(object sender, EventArgs e)
        {
            Catalogue catalogue = GetCatalogueFrSave();
            CatalogueLogic.Save(catalogue);
            ShowCatalogues();
        }
    }
}