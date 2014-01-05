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
using TopEntity.Enum;
using NLog;
using TopLogic;
using TopEntity;
using TopUtilityTool;

namespace TopSite.Account
{
    public partial class ArticleEdit : System.Web.UI.Page
    {
        ArticleLogic articleLogic = new ArticleLogic();
        CatalogueLogic catalogueLogc = new CatalogueLogic();
        TopKeywordsLogic topKeywordsLogic = new TopKeywordsLogic();

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
                    this.OrignSourceUrl.Text = BasicCache.SiteConfig.SiteUrl;
                    this.KeyWords.Text = string.Empty;
                    this.Summary.Text = string.Empty;
                    this.btnSaveArticle.CommandArgument = EditStateEnum.New.ToString();

                    break;
                case EditStateEnum.Edit:
                    string strId = Request.QueryString["id"];
                    int id = 0;
                    if (int.TryParse(strId, out id))
                    {
                        Article article = articleLogic.GetList(p => p.Id == id).FirstOrDefault();
                        if (article != null)
                        {
                            this.txtTitle.Text = article.Title;
                            this.txtContent.Text = TopUtility.GetUnstressedContent(article.Content, article.KeyWords);
                            this.OrignSource.Text = article.OrignSource;
                            this.OrignSourceUrl.Text = article.OrignSourceUrl;
                            this.KeyWords.Text = article.KeyWords;
                            this.Summary.Text = article.Summary;
                            this.TopKeywords.Text = article.TopKeywords;
                            this.DropDownListCatalogue.SelectedValue = article.CatalogueId.ToString();
                            this.btnSaveArticle.CommandArgument = EditStateEnum.Edit.ToString();
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private void BindDropdownlist()
        {
            this.DropDownListCatalogue.DataSource = catalogueLogc.GetList().OrderBy(p => p.Order);
            this.DropDownListCatalogue.DataBind();
        }

        /// <summary>
        /// 保存文章
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveArticle_Click(object sender, EventArgs e)
        {
            try
            {
                Article article = GetEditingArticleAndProcessKeywords();
                articleLogic.Save(article);
            }
            catch (Exception ex)
            {
                log.ErrorException("保存文章失败", ex);
            }
            Response.Redirect(Request.Url.ToString());
        }

        /// <summary>
        /// 获取正在编辑的文章对象，并同步数据库中关键字情况
        /// </summary>
        /// <returns></returns>
        private Article GetEditingArticleAndProcessKeywords()
        {
            Article article = null;

            EditStateEnum editEnum = GetAction();
            switch (editEnum)
            {
                case EditStateEnum.New:
                    article = new Article();
                    article.Id = articleLogic.GetNewIdentity();
                    article.CreateTime = DateTime.Now;
                    article.ClickNum = 0;

                    break;
                case EditStateEnum.Edit:
                    string strId = Request.QueryString["id"];
                    int id = 0;
                    if (int.TryParse(strId, out id))
                    {
                        article = articleLogic.GetList(p => p.Id == id).FirstOrDefault();
                    }

                    break;
                default:
                    break;
            }

            // 处理关键字
            topKeywordsLogic.AnalyzeAndSave(this.TopKeywords.Text.Trim(), article.TopKeywords);

            // 处理自动上传图片
            string content = string.Empty;
            bool uploadResult = articleLogic.AutoUploadImage(this.txtContent.Text, out content);
            if (uploadResult)
            {
                article.Content = content;
            }
            else
            {
                article.Content = this.txtContent.Text;
            }

            // 为属性赋新值
            article.Title = this.txtTitle.Text;
            article.CatalogueId = int.Parse(DropDownListCatalogue.SelectedValue);
            article.Content = TopUtility.GetStressedContent(article.Content, this.KeyWords.Text);
            article.KeyWords = this.KeyWords.Text;
            article.OrignSourceUrl = articleLogic.GetArticleOrignSourceUrl(this.OrignSourceUrl.Text);
            article.OrignSource = articleLogic.GetArticleOrignSourceTitle(this.OrignSource.Text, this.OrignSourceUrl.Text);
            article.Summary = this.Summary.Text;
            article.TopKeywords = TopKeywords.Text.Trim();

            return article;
        }

        /// <summary>
        /// 获取当前界面的操作类型
        /// </summary>
        /// <returns></returns>
        private EditStateEnum GetAction()
        {
            string strAction = Request.QueryString["action"];
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
            catalogueLogc.Dispose();
            topKeywordsLogic.Dispose();
        }
    }
}