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
using System.Linq.Expressions;
using TopEntity.Enum;

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
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">要查询的数据类型</typeparam>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="totalCount">输出总条数</param>
        /// <param name="searchCondition">检索条件</param>
        /// <param name="orderKeySelector">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <param name="pageSize">页面大小默认为10</param>
        /// <param name="pageIndex">页面索引</param>
        /// <returns></returns>
        public virtual List<T> GetListByPage<TKey>(out int totalCount,
                                                    Predicate<T> searchCondition = null,
                                                    Expression<Func<T, TKey>> orderKeySelector = null,
                                                    OrderEnum order = OrderEnum.Ascending,
                                                    int pageSize = 10,
                                                    int pageIndex = 1)
        {
            TopDal.Enum.OrderEnum baseOrder = TopDal.Enum.OrderEnum.Ascending;

            switch (order)
            {
                case OrderEnum.Ascending:
                    baseOrder = TopDal.Enum.OrderEnum.Ascending;
                    break;
                case OrderEnum.Descending:
                    baseOrder = TopDal.Enum.OrderEnum.Descending;
                    break;
                default:
                    break;
            }

            return mainClient.GetListByPage<T, TKey>(out totalCount,
                                                        searchCondition,
                                                        orderKeySelector,
                                                        baseOrder,
                                                        pageSize,
                                                        pageIndex);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">要查询的数据类型</typeparam>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="searchCondition">检索条件</param>
        /// <param name="orderKeySelector">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <param name="pageSize">页面大小默认为10</param>
        /// <param name="pageIndex">页面索引</param>
        /// <returns></returns>
        public virtual List<T> GetListByPage<TKey>(Predicate<T> searchCondition = null,
                                                    Expression<Func<T, TKey>> orderKeySelector = null,
                                                    OrderEnum order = OrderEnum.Ascending,
                                                    int pageSize = 10,
                                                    int pageIndex = 1)
        {
            TopDal.Enum.OrderEnum baseOrder = TopDal.Enum.OrderEnum.Ascending;

            switch (order)
            {
                case OrderEnum.Ascending:
                    baseOrder = TopDal.Enum.OrderEnum.Ascending;
                    break;
                case OrderEnum.Descending:
                    baseOrder = TopDal.Enum.OrderEnum.Descending;
                    break;
                default:
                    break;
            }

            return mainClient.GetListByPage<T, TKey>(searchCondition,
                                                     orderKeySelector,
                                                     baseOrder,
                                                     pageSize,
                                                     pageIndex);
        }

        /// <summary>
        /// 获取新的自增Id
        /// </summary>
        /// <returns></returns>
        public long GetNewIdentity()
        {
            Type type = typeof(T);
            return IdentityHelper.GetNewIdentity(mainClient, type);
        }

        public virtual void Commit()
        {
            if (mainClient != null)
            {
                mainClient.Commit();
            }
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
