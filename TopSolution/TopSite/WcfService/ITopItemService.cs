using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TopEntity;

namespace TopSite.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ITopItemService”。
    [ServiceContract]
    public interface ITopItemService
    {
        /// <summary>
        /// 向数据库中添加广告项目
        /// </summary>
        /// <param name="items"></param>
        [OperationContract]
        void SaveTopItemList(List<TopItem> items);
        
        /// <summary>
        /// 根据关键词删除广告项目
        /// </summary>
        /// <param name="keyword"></param>
        [OperationContract]
        void DeleteTopItems(string keyword);

        /// <summary>
        /// 获取系统中所有的Keywords
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<string> GetAllKeywords();
    }
}
