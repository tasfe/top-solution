/*********************************************************
 * copyright learnren.com 版权所有
 * 开发人员：minyuan
 * 创建时间：2013/6/24 21:20:58
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopEntity
{
    public abstract class EntityBase
    {
        private long _Id;
        /// <summary>
        /// Id
        /// </summary>
        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }

    }
}
