using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TopEntity;

namespace TopSite.WebService
{
    /// <summary>
    /// TopItemService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class TopItemService : System.Web.Services.WebService
    {
        /// <summary>
        /// 保存传入的TopItem到数据库
        /// </summary>
        /// <param name="items"></param>
        [WebMethod]
        public void SaveTopItemList(List<TopItem> items)
        {
            using (TopLogic.TopLogic logic = new TopLogic.TopLogic())
            {
                foreach (var item in items)
                {
                    logic.Save(item);
                }
            }
        }
    }
}
