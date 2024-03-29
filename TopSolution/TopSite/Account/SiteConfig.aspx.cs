/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/28 16:47:07
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
using TopEntity.Enum;

namespace TopSite.Account
{
    public partial class SiteConfig : System.Web.UI.Page
    {
        SiteLogic siteLogic = new SiteLogic();
        TopKeywordsLogic keywordsLogic = new TopKeywordsLogic();

        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowSiteConfig();
            }
        }

        private void ShowSiteConfig()
        {
            try
            {
                TopEntity.SiteConfig config = siteLogic.GetList().FirstOrDefault();
                if (config == null)
                {
                    btnSaveSiteConfig.CommandArgument = EditStateEnum.New.ToString(); ;
                }
                else
                {
                    btnSaveSiteConfig.CommandArgument = EditStateEnum.Edit.ToString();
                    this.SiteName.Text = config.SiteName;
                    this.TopKeywords.Text = config.TopKeywords;
                    this.KeyWords.Text = config.KeyWords;
                    this.Summary.Text = config.Summary;
                    this.CopyRight.Text = config.CopyRight;
                    this.TextBoxSiteUrl.Text = config.SiteUrl;
                }
            }
            catch (Exception)
            {
                btnSaveSiteConfig.CommandArgument = string.Empty;
            }
        }

        private TopEntity.SiteConfig GetSiteConfigForSave()
        {
            TopEntity.SiteConfig config = null;

            if (btnSaveSiteConfig.CommandArgument == EditStateEnum.New.ToString())
            {
                config = new TopEntity.SiteConfig();
                config.Id = 0;
            }
            else
            {
                config = siteLogic.GetList((TopEntity.SiteConfig p) => true).FirstOrDefault();
            }

            keywordsLogic.AnalyzeAndSave(TopKeywords.Text.Trim(), config.TopKeywords);

            config.KeyWords = KeyWords.Text;
            config.SiteName = SiteName.Text;
            config.Summary = Summary.Text;
            config.TopKeywords = TopKeywords.Text.Trim();
            config.CopyRight = CopyRight.Text;
            config.SiteUrl = this.TextBoxSiteUrl.Text;

            return config;
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            this.siteLogic.Dispose();
            this.keywordsLogic.Dispose();
        }

        protected void SaveSiteConfig_Click(object sender, EventArgs e)
        {
            try
            {
                TopEntity.SiteConfig config = GetSiteConfigForSave();
                siteLogic.Save(config);

                TopUtilityTool.TopUtility.UpdateConmmonJs(BasicCache.SiteConfig);
            }
            catch (Exception ex)
            {
                log.ErrorException("保存配置失败。", ex);
            }
        }
    }
}