/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/27 13:34:39
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

namespace TopSite.Masters
{
    public partial class ArticleListMaster : System.Web.UI.MasterPage
    {
        private string _PageKeywords;

        public string PageKeywords
        {
            get { return _PageKeywords; }
            set { _PageKeywords = value; }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}