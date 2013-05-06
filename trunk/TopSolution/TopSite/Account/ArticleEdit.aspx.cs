/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/28 16:48:46
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
using TopArticleEntity.Enum;
using NLog;
using TopLogic;

namespace TopSite.Account
{
    public partial class ArticleEdit : System.Web.UI.Page
    {
        ArticleLogic articleLogic = new ArticleLogic();
        CatalogueLogic catalogueLogc = new CatalogueLogic();

        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EditStateEnum nowAction = GetAction();
                InitPageCtrl(nowAction);
            }
        }

        private void InitPageCtrl(EditStateEnum nowAction)
        {
            BindDropdownlist();
            switch (nowAction)
            {
                case EditStateEnum.New:
                    this.txtTitle.Text = string.Empty;
                    this.txtContent.Text = string.Empty;
                    this.OrignSource.Text = "本站";
                    this.KeyWords.Text = string.Empty;
                    this.Summary.Text = string.Empty;

                    break;
                case EditStateEnum.Edit:
                    string strId=Request.QueryString["id"];
                    

                    break;
                default:
                    break;
            }
        }

        private void BindDropdownlist()
        {
            this.DropDownListCatalogue.DataSource = catalogueLogc.GetList(p => true).OrderBy(p => p.Order);
            this.DropDownListCatalogue.DataBind();
        }

        /// <summary>
        /// 保存文章
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveArticle_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 获取当前界面的操作类型
        /// </summary>
        /// <returns></returns>
        private EditStateEnum GetAction()
        {
            string strAction = Request.QueryString[""];
            if (string.IsNullOrEmpty(strAction))
            {
                return EditStateEnum.New;
            }
            else
            {
                try
                {
                    return (EditStateEnum)Enum.Parse(typeof(EditStateEnum), strAction, true);
                }
                catch (Exception ex)
                {
                    log.ErrorException("获取当前操作类型失败。", ex);
                    return EditStateEnum.New;
                }
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            articleLogic.Dispose();
        }
    }
}