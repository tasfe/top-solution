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
            get { return _SiteConfig; }
            set { _SiteConfig = value; }
        }
    }
}
