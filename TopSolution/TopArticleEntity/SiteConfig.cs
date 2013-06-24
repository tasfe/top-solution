/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/28 11:59:35
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopEntity
{
    public class SiteConfig : EntityBase
    {
        private string _SiteName;

        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }

        private string _TopKeywords;

        public string TopKeywords
        {
            get { return _TopKeywords; }
            set { _TopKeywords = value; }
        }


        private string _KeyWords;

        public string KeyWords
        {
            get { return _KeyWords; }
            set { _KeyWords = value; }
        }

        private string _Summary;

        public string Summary
        {
            get { return _Summary; }
            set { _Summary = value; }
        }



        private string _CopyRight;

        public string CopyRight
        {
            get { return _CopyRight; }
            set { _CopyRight = value; }
        }

        private string _SiteUrl;

        public string SiteUrl
        {
            get { return _SiteUrl; }
            set { _SiteUrl = value; }
        }

    }
}
