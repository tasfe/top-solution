/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/28 17:39:14
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

namespace TopLogic
{

    public abstract class LogicBase<T>
    {
        protected static readonly string conn = "datasource=.;";
        protected DB4ODALClient GetDbClient()
        {
            return DB4ODALServerHelper.GetIDALClient(conn);
        }

        public virtual void Save(T obj)
        {
            using (DB4ODALClient client = GetDbClient())
            {
                client.Save(obj);
            }
        }

        public virtual void Delete(T obj)
        {
            using (DB4ODALClient client = GetDbClient())
            {
                client.Save(obj);
            }
        }

        public virtual List<T> GetList(Predicate<T> p)
        {
            using (DB4ODALClient client = GetDbClient())
            {
                return client.GetList<T>(p);
            }
        }
    }
}
