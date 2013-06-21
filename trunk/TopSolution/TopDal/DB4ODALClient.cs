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
            this.IObjectContainer.Ext().Store(obj,5);
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
