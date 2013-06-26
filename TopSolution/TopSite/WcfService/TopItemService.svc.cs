using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TopEntity;
using TopLogic;

namespace TopSite.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“TopItemService”。
    public class TopItemService : ITopItemService
    {
        public void SaveTopItemList(List<TopItem> items)
        {
            using (TopLogic.TopLogic logic = new TopLogic.TopLogic())
            {
                logic.Save(items);
            }
        }


        public void DeleteTopItems(string keyword)
        {
            using (TopLogic.TopLogic logic = new TopLogic.TopLogic())
            {
                List<TopItem> items = logic.GetList(p => p.Keywords == keyword);

                foreach (var item in items)
                {
                    logic.Delete(item);
                }
            }
        }


        public List<TopKeywords> GetAllKeywords()
        {
            using (var logic = new TopKeywordsLogic())
            {
                return logic.GetList(p => p.LastGetTime.AddHours(24) < DateTime.Now);
            }
        }
    }
}
