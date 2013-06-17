﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TopEntity;

namespace TopSite.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“TopItemService”。
    public class TopItemService : ITopItemService
    {
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