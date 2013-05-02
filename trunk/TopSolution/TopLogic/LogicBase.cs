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
using TopDal.IdentiyHelper;

namespace TopLogic
{

    public abstract class LogicBase<T> : IDisposable
    {
        protected static readonly string conn = "datasource=.;";
        protected DB4ODALClient GetDbClient()
        {
            return DB4ODALServerHelper.GetIDALClient(conn);
        }

        private DB4ODALClient client = null;
        public LogicBase()
        {
            client = GetDbClient();
        }


        public virtual void Save(T obj)
        {

            client.Save(obj);
        }

        public virtual void Delete(T obj)
        {
            client.Save(obj);
        }

        public virtual List<T> GetList(Predicate<T> p)
        {

            return client.GetList<T>(p);
        }

        /// <summary>
        /// 获取新的自增Id
        /// </summary>
        /// <returns></returns>
        public int GetNewIdentity()
        {
            Type type = typeof(T);
            return IdentityHelper.GetNewIdentity(client, type);
        }

        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
                client = null;
            }
        }
    }
}
