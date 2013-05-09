/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/5/2 13:06:52
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSharing.DB4ODAL;

namespace TopDal.IdentiyHelper
{
    public class IdentityHelper
    {
        private IdentityHelper()
        {
            _IdentityContainer = new Dictionary<Type, int>();
        }

        private Dictionary<Type, int> _IdentityContainer;

        /// <summary>
        /// 获取新的自增长字符
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="type">要获取的数据类型</param>
        /// <returns></returns>
        public static int GetNewIdentity(DB4ODALClient client, Type type)
        {
            int result = 0;

            IdentityHelper helper = client.GetList<IdentityHelper>(p => true).FirstOrDefault();
            if (helper == null)
            {
                helper = new IdentityHelper();
            }

            if (helper._IdentityContainer.ContainsKey(type) == false)
            {
                helper._IdentityContainer.Add(type, 0);
            }

            helper._IdentityContainer[type] += 1;

            client.Save(helper);

            client.Commit();

            result = helper._IdentityContainer[type];

            return result;
        }
    }
}
