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
using NLog;

namespace TopLogic
{

    public abstract class LogicBase<T> : IDisposable
    {
        protected string mainConn = null;
        protected string adConn = null;

        protected string dbBackupDir = null;
        protected Logger logger = null;

        protected DB4ODALClient GetDbClient(string conn)
        {
            return DB4ODALServerHelper.GetIDALClient(conn);
        }
        /// <summary>
        /// 主数据库管理类
        /// </summary>
        protected DB4ODALClient mainClient = null;
        public LogicBase()
        {
            mainConn = System.Configuration.ConfigurationManager.ConnectionStrings["mainBase"].ConnectionString;
            adConn = System.Configuration.ConfigurationManager.ConnectionStrings["adBase"].ConnectionString;
            dbBackupDir = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/");
            logger = LogManager.GetCurrentClassLogger();

            mainClient = GetDbClient(mainConn);
        }


        public virtual void Save(T obj)
        {

            mainClient.Save(obj);
        }

        public virtual void Delete(T obj)
        {
            mainClient.Delete(obj);
        }

        /// <summary>
        /// 根据条件筛选
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual List<T> GetList(Predicate<T> p)
        {
            return mainClient.GetList<T>(p);
        }

        /// <summary>
        /// 获取全部对象
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            return mainClient.GetList<T>();
        }

        /// <summary>
        /// 获取新的自增Id
        /// </summary>
        /// <returns></returns>
        public int GetNewIdentity()
        {
            Type type = typeof(T);
            return IdentityHelper.GetNewIdentity(mainClient, type);
        }

        public virtual void Dispose()
        {
            if (mainClient != null)
            {
                mainClient.Dispose();
                mainClient = null;
            }
        }
    }
}
