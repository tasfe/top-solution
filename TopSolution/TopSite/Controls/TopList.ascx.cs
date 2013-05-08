using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TopSite.Controls
{
    public partial class TopList : System.Web.UI.UserControl
    {
        /// <summary>
        /// 要搜索的关键词
        /// </summary>        
        public string KeyWords { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}