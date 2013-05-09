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

namespace WebSharing.DB4ODAL
{
    public class DB4ODALClient:IDisposable
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
            IObjectContainer.Ext().Store(obj,5);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Delete<T>(T obj)
        {
            IObjectContainer.Delete(obj);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<T> GetList<T>(Predicate<T> p)
        {
            var resultLinq = (from T d in IObjectContainer where p(d) select d);
            return resultLinq.ToList();
        }

        public void Commit()
        {
            IObjectContainer.Ext().Commit();
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
