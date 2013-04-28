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

namespace WebSharing.DB4ODAL
{
    public class DB4ODALClient
    {
        private IObjectContainer _IObjectContainer;

        public IObjectContainer IObjectContainer
        {
            get { return _IObjectContainer; }
            set { _IObjectContainer = value; }
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
