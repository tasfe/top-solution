/*********************************************************
 * copyright learnren.com 版权所有
 * 开发人员：minyuan
 * 创建时间：2013/2/5 21:08:03
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using System.Linq.Expressions;
using TopDal.Enum;

namespace WebSharing.DB4ODAL
{
    public class DB4ODALClient : IDisposable
    {
        private IObjectContainer _IObjectContainer;

        public IObjectContainer IObjectContainer
        {
            get { return _IObjectContainer; }
            set { _IObjectContainer = value; }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Save<T>(T obj)
        {
            this.IObjectContainer.Ext().Store(obj, 5);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Delete<T>(T obj)
        {
            this.IObjectContainer.Delete(obj);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<T> GetList<T>(Predicate<T> p)
        {
            var resultLinq = (from T d in this.IObjectContainer where p(d) select d);
            return resultLinq.ToList();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetList<T>()
        {
            return (from T d in this.IObjectContainer select d).ToList();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TSource">要查询的数据类型</typeparam>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="searchCondition">检索条件</param>
        /// <param name="orderKeySelector">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <param name="pageSize">页面大小默认为10</param>
        /// <param name="pageIndex">页面索引</param>
        /// <returns></returns>
        public List<TSource> GetListByPage<TSource, TKey>(Predicate<TSource> searchCondition = null,
                                                          Expression<Func<TSource, TKey>> orderKeySelector = null,
                                                          OrderEnum order = OrderEnum.Ascending,
                                                          int pageSize = 10,
                                                          int pageIndex = 1)
        {
            IDb4oLinqQuery<TSource> linqResult = null;

            if (searchCondition == null)
            {
                linqResult = from TSource d in this.IObjectContainer select d;
            }
            else
            {
                linqResult = from TSource d in this.IObjectContainer where searchCondition(d) select d;
            }

            if (order == OrderEnum.Ascending)
            {
                linqResult = linqResult.OrderBy(orderKeySelector);
            }
            else
            {
                linqResult = linqResult.OrderByDescending(orderKeySelector);
            }

            // 获取合理页数
            pageIndex = pageIndex > 0 ? pageIndex : 1;

            // 获取要跳过的数量
            int skipCount = pageSize * (pageIndex - 1);

            // 获取合理的页大小
            pageSize = pageSize > 0 ? pageSize : 1;

            var linqPagedResult = linqResult.Skip(pageIndex).Take(pageSize);

            return linqPagedResult.ToList();
        }

        /// <summary>
        /// 提交修改
        /// </summary>
        public void Commit()
        {
            this.IObjectContainer.Ext().Commit();
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="path">备份地址</param>
        public void BackupDb(string path)
        {
            this.IObjectContainer.Ext().Backup(path);
        }

        public void Dispose()
        {
            if (_IObjectContainer != null)
            {
                _IObjectContainer.Close();
                _IObjectContainer.Dispose();
            }
        }
    }
}
