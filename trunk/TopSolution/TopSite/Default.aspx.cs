using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Top.Api.Response;

namespace TopSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetTopKeItemList();
        }

        private void GetTopKeItemList()
        {
            TopLogic.TopLogic logic = new TopLogic.TopLogic();
            TaobaokeItemsCouponGetResponse response = logic.GetTaobaokeItemsCoupon("wenwenxing", "site", "条纹", 0, "volume_desc");
            this.RepeaterTaoBaoKeItems.DataSource = response.TaobaokeItems;
            this.RepeaterTaoBaoKeItems.DataBind();
        }
    }
}
