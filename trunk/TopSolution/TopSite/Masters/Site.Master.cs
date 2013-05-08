using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TopSite
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public string PageKeywords { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindkeywords();
            }
        }

        private void Bindkeywords()
        {
            if (PageKeywords == null)
            {
                this.TopList.KeyWords = "";
            }
            else
            {
                this.TopList.KeyWords = PageKeywords;
            }
        }
    }
}
