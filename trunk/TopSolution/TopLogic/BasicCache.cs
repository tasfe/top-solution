/*********************************************************
 * copyright learnren.com 版权所有
 * 开发人员：minyuan
 * 创建时间：2013/5/10 19:55:14
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopArticleEntity;

namespace TopLogic
{
    public static class BasicCache
    {
        private static SiteConfig _SiteConfig;

        public static SiteConfig SiteConfig
        {
            get
            {
                if (_SiteConfig != null)
                {
                    return _SiteConfig;
                }
                else
                {
                    return new SiteConfig { SiteName = "乐人淘宝客系统", KeyWords = "乐人淘宝客", Summary = "乐人淘宝客系统" };
                }
            }
            set { _SiteConfig = value; }
        }

        public static void Initialize()
        {
            try
            {

                SiteLogic siteLogic = new SiteLogic();
                SiteConfig siteConfig = siteLogic.GetList(p => true).FirstOrDefault();
                BasicCache._SiteConfig = siteConfig;

            }
            catch (Exception ex)
            {
                NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
                logger.ErrorException("初始化缓存失败", ex);
            }
        }
    }
}
